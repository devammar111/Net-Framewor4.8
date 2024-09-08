using Probase.GridHelper;
using Probase.GridHelper.Export;
using PBASE.Service;
using System;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using PBASE.WebAPI.Controllers;
using System.IO;
using System.Globalization;
using PBASE.Entity;
using System.Collections.Generic;
using OfficeOpenXml;
using PBASE.WebAPI.Helpers;
using PBASE.Entity.Enum;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PBASE.WebAPI.ViewModels;

namespace PBASE.WebAPI.Controllers
{
    public class ExcelExportController : BaseController
    {
        #region Initialization

        private readonly ILookupService lookupService;
        private readonly IUserService userService;

        public ExcelExportController(ILookupService lookupService, IUserService userService
        )
        {
            this.lookupService = lookupService;
            this.userService = userService;
        }
        #endregion Initialization

        [Route("api/ExcelExport")]
        [HttpGet]
        public async Task<IHttpActionResult> ExcelExport(string exportGridId, [ModelBinder(typeof(APIGridModelBinder))]GridSetting gridSetting, string exportColumnIds, string exportColumnLabels, string savedFilterId, string gridLabel, string filteRules, string current_date)
        {
            if (exportGridId.Contains("%2F"))
            {
                exportGridId = exportGridId.Split("%2F")[1];
            }
            List<string> exportColumns = new List<string>();
            List<string> exportColumnsLabels = new List<string>();
            if (!string.IsNullOrWhiteSpace(exportColumnIds))
            {
                exportColumns = JsonConvert.DeserializeObject<List<string>>(exportColumnIds);
            }
            if (!string.IsNullOrWhiteSpace(exportColumnLabels))
            {
                exportColumnsLabels = JsonConvert.DeserializeObject<List<string>>(Uri.UnescapeDataString(exportColumnLabels));
            }

            byte[] fileContents = null;
            byte[] binaryData = ReadFully(new MemoryStream());
            var serviceName = "PBASE.Repository";
            Type type = null;
            dynamic gridData = null;
            try
            {
                var referenceAssemblyNames = Assembly.GetExecutingAssembly().GetReferencedAssemblies();
                var reportsitoryAssemblyName = referenceAssemblyNames.FirstOrDefault(x => x.Name.Equals(serviceName));

                if (reportsitoryAssemblyName != null)
                {
                    var reportsitoryAssembly = Assembly.Load(reportsitoryAssemblyName);
                    var fullName = serviceName + "." + exportGridId + "Repository";

                    type = reportsitoryAssembly.GetType(fullName);
                }

                if (exportGridId == "vw_AspNetUserLogsGrid")
                {
                    DateTime? startDate = null;
                    DateTime? endDate = null;
                    if (gridSetting.Where.rules.Length > 0)
                    {
                        gridSetting.IsExcelExport = true;
                        foreach (var rule in gridSetting.Where.rules)
                        {
                            if (rule.field.ToLower() == "startdate")
                            {
                                startDate = DateTime.ParseExact(rule.data, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            }
                            if (rule.field.ToLower() == "enddate")
                            {
                                endDate = DateTime.ParseExact(rule.data, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            }
                        }
                        endDate = endDate.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
                        gridSetting.Where.rules = gridSetting.Where.rules.Where(x => x.field.ToLower() != "startdate" && x.field.ToLower() != "enddate").ToArray();
                        var userLogs = userService.Selectvw_AspNetUserLogsGridsByGridSetting(gridSetting);
                        var userIds = userLogs.Select(x => x.Id).ToList().Distinct();
                        IEnumerable<vw_AspNetUserAccountLogs> logs = userService.SelectMany_vw_AspNetUserAccountLogs(x => userIds.Contains(x.AspNetUserKey) && x.CreatedDate >= startDate.Value && x.CreatedDate <= endDate.Value);
                        gridData = from x in userLogs
                                   select new vw_AspNetUserLogsGrid
                                   {
                                       Id = x.Id,
                                       FullName = x.FullName,
                                       username = x.username,
                                       TotalLogin = logs.Where(l => l.AspNetUserKey == x.Id && l.RequestType == Enum.GetName(typeof(RequestType), 0)).Count(),
                                       LoginSuccess = logs.Where(l => l.AspNetUserKey == x.Id && l.RequestType == Enum.GetName(typeof(RequestType), 0) && l.IsStatus == true).Count(),
                                       LoginFailure = logs.Where(l => l.AspNetUserKey == x.Id && l.RequestType == Enum.GetName(typeof(RequestType), 0) && l.IsStatus == false).Count(),
                                       PasswordResetTotal = logs.Where(l => l.AspNetUserKey == x.Id && l.RequestType == Enum.GetName(typeof(RequestType), 1)).Count(),
                                       PasswordResetSuccess = logs.Where(l => l.AspNetUserKey == x.Id && l.RequestType == Enum.GetName(typeof(RequestType), 1) && l.IsStatus == true).Count(),
                                       PasswordResetFailure = logs.Where(l => l.AspNetUserKey == x.Id && l.RequestType == Enum.GetName(typeof(RequestType), 1) && l.IsStatus == false).Count(),
                                       ChangePasswordSuccess = logs.Where(l => l.AspNetUserKey == x.Id && l.RequestType == Enum.GetName(typeof(RequestType), 2) && l.IsStatus == true).Count(),
                                       ChangePasswordFailure = logs.Where(l => l.AspNetUserKey == x.Id && l.RequestType == Enum.GetName(typeof(RequestType), 2) && l.IsStatus == false).Count(),
                                       SuccessCreatedDate = x.SuccessCreatedDate,
                                       FailedCreatedDate = x.FailedCreatedDate,
                                       IPAddress = x.IPAddress,
                                       Location = x.Location,

                                   };
                    }
                    else
                    {
                        gridData = new List<vw_AspNetUserLogsGrid>();
                    }

                }
                else if (exportGridId == "loginAnalysis")
                {
                    DateTime? startDate = null;
                    DateTime? endDate = null;
                    int userId = int.MinValue;
                    if (gridSetting.Where.rules.Length > 0)
                    {
                        gridSetting.IsExcelExport = true;
                        List<Rule> rules = new List<Rule>();
                        foreach (var rule in gridSetting.Where.rules)
                        {
                            if (rule.field.ToLower() == "userid")
                            {
                                int.TryParse(rule.data, out userId);
                            }
                            if (rule.field.ToLower() == "startdate")
                            {
                                startDate = DateTime.ParseExact(rule.data, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            }
                            if (rule.field.ToLower() == "enddate")
                            {
                                endDate = DateTime.ParseExact(rule.data, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            }
                        }
                        rules = gridSetting.Where.rules.Where(x => x.field.ToLower() != "startdate" && x.field.ToLower() != "enddate" && x.field.ToLower() != "userid").ToList();
                        rules.Add(new Rule { field = "CreatedDate", data = startDate.ToString(), op = "ge" });
                        rules.Add(new Rule { field = "AspNetUserKey", data = userId.ToString(), op = "eq" });
                        rules.Add(new Rule { field = "CreatedDate", data = endDate.Value.AddDays(1).ToString(), op = "le" });
                        gridSetting.Where.rules = rules.ToArray();
                        gridData = await userService.Selectvw_AspNetUserAccountLogssByGridSettingAsync(gridSetting);
                    }
                    else
                    {
                        gridData = new List<vw_AspNetUserAccountLogs>();
                    }
                }
                else
                {
                    if (type != null)
                    {
                        gridSetting.IsExcelExport = true;
                        object[] parametersArray = new object[] { gridSetting };

                        object classInstance = GlobalConfiguration.Configuration.DependencyResolver.GetService(type);
                        gridData = classInstance.InvokeMethod("SelectByGridSetting", parametersArray);
                    }
                }
                if (gridData != null)
                {
                    var export = new ExcelExport();
                    var headerExportColumnsIds = exportColumnIds;
                    var headerExportColumnsLabels = exportColumnLabels;
                    exportColumnIds = exportColumnIds.CompressString();
                    exportColumnLabels = Uri.UnescapeDataString(exportColumnLabels).CompressString();
                    fileContents = export.GetFileContents(exportColumnIds, exportColumnLabels, gridData);
                    Stream stream = new MemoryStream(fileContents);
                    Stream memoryStream = new MemoryStream();
                    ExcelPackage excelPackage = new ExcelPackage(memoryStream, stream);

                    ExcelWorksheet worksheetNew = excelPackage.Workbook.Worksheets.Add("Export Header");
                    excelPackage.Workbook.Protection.LockRevision = false;
                    excelPackage.Workbook.Protection.LockStructure = false;
                    worksheetNew = await GenerateExcelExportHeader(worksheetNew, savedFilterId, exportGridId, headerExportColumnsIds, headerExportColumnsLabels, gridLabel, filteRules, current_date);

                    excelPackage.Save();
                    memoryStream.Position = 0;
                    binaryData = ReadFully(memoryStream);
                    memoryStream.Close();
                    memoryStream.Dispose();
                }
                return Ok(Base64EncodedFile(binaryData));
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        public static string decode(string text)
        {
            byte[] mybyte = System.Convert.FromBase64String(text);
            string returntext = System.Text.Encoding.UTF8.GetString(mybyte);
            return returntext;
        }

        private async Task<ExcelWorksheet> GenerateExcelExportHeader(ExcelWorksheet worksheetNew, string savedFilterId, string exportGridId, string exportColumnIds, string exportColumnLabels, string gridLabel, string filteRules, string current_date)
        {
            List<Rule> gridRules = new List<Rule>();
            List<string> exportColumns = new List<string>();
            List<string> exportColumnsLabels = new List<string>();
            var stateName = string.Empty;
            string filterInfo = string.Empty;
            var currentDate = DateTime.Now;

            var gridName = gridLabel != "undefined" ? Uri.UnescapeDataString(gridLabel) : "";
            var currentDateTime = currentDate.ToString("yyyy-MMM-dd hh:mm:ss");
            var currentUserId = GetUserId();
            var user = lookupService.SelectSingle_vw_UserGrid(x => x.Id == currentUserId);
            var filterId = 0;
            if (savedFilterId != null && savedFilterId != "undefined")
            {
                filterId = Convert.ToInt32(savedFilterId);
            }

            if (!string.IsNullOrWhiteSpace(exportColumnIds))
            {
                exportColumns = JsonConvert.DeserializeObject<List<string>>(exportColumnIds);
            }
            if (!string.IsNullOrWhiteSpace(exportColumnLabels))
            {
                exportColumnsLabels = JsonConvert.DeserializeObject<List<string>>(System.Uri.UnescapeDataString(exportColumnLabels));
            }
            if (!string.IsNullOrWhiteSpace(filteRules))
            {
                var equality = string.Empty;
                gridRules = JsonConvert.DeserializeObject<List<Rule>>(Uri.UnescapeDataString(filteRules));
                for (var m = 0; m < gridRules.Count(); m++)
                {

                    var columnIndex = exportColumns.FindIndex(s => s.Equals(gridRules[m].field));
                    if (gridRules[m].op == "eq")
                    {
                        equality = "EQUALS";
                    }
                    else if (gridRules[m].op == "cn")
                    {
                        equality = "CONTAINS";
                    }
                    else if (gridRules[m].op == "ge")
                    {
                        equality = "GREATER THAN";
                    }
                    else if (gridRules[m].op == "le")
                    {
                        equality = "LESS THAN";
                    }
                    else
                    {
                        equality = "NOT EQUALS";
                    }
                    if (!String.IsNullOrWhiteSpace(filterInfo) && columnIndex > -1)
                    {
                        filterInfo = filterInfo + ", " + exportColumnsLabels[columnIndex] + " " + equality + " '" + gridRules[m].data + "'";
                    }
                    else if (columnIndex > -1)
                    {
                        filterInfo = exportColumnsLabels[columnIndex] + " " + equality + " '" + gridRules[m].data + "'";
                    }
                    else
                    {
                        filterInfo = !String.IsNullOrWhiteSpace(filterInfo) ? filterInfo + ", " + gridRules[m].field + " " + equality + " '" + gridRules[m].data + "'" : gridRules[m].field + " " + equality + " '" + gridRules[m].data + "'";
                    }
                }
            }

            InternalGridSetting internalGridSettings = await lookupService.SelectSingle_InternalGridSettingAsync(x => x.InternalGridSettingId == filterId);
            if (internalGridSettings != null)
            {
                stateName = internalGridSettings.StateName;
            }


            string fileName = gridName + "_GRID_" + current_date;
            try
            {
                UserExportLog userExportLog = new UserExportLog();
                userExportLog.UserExportLogId = 0;
                userExportLog.ApplicationOption = gridName;
                userExportLog.DataSource = exportGridId;
                userExportLog.SavedFilterName = stateName;
                userExportLog.Filter = filterInfo;
                userExportLog.ExportFileName = fileName;
                userExportLog.IsArchived = false;

                await lookupService.SaveUserExportLogFormAsync(userExportLog);

            }
            catch (Exception ex)
            {
            }

            worksheetNew.Protection.IsProtected = false;
            worksheetNew.Column(1).Width = 20;
            worksheetNew.Column(2).Width = 25;
            worksheetNew.Cells[1, 1].Value = "Grid Name";
            worksheetNew.Cells[1, 1].Style.Font.Bold = true;
            worksheetNew.Cells[1, 2].Value = gridName ?? "";
            worksheetNew.Cells[2, 1].Value = "Data Source";
            worksheetNew.Cells[2, 1].Style.Font.Bold = true;
            worksheetNew.Cells[2, 2].Value = exportGridId ?? "";
            worksheetNew.Cells[3, 1].Value = "User";
            worksheetNew.Cells[3, 1].Style.Font.Bold = true;
            worksheetNew.Cells[3, 2].Value = user.Email ?? "";
            worksheetNew.Cells[4, 1].Value = "Date Time";
            worksheetNew.Cells[4, 1].Style.Font.Bold = true;
            worksheetNew.Cells[4, 2].Value = currentDateTime ?? "";
            worksheetNew.Cells[5, 1].Value = "Saved Filter Name";
            worksheetNew.Cells[5, 1].Style.Font.Bold = true;
            worksheetNew.Cells[5, 2].Value = stateName ?? "";
            worksheetNew.Cells[6, 1].Value = "Filters";
            worksheetNew.Cells[6, 1].Style.Font.Bold = true;
            worksheetNew.Cells[6, 2].Value = filterInfo ?? "";
            return worksheetNew;
        }
    }
}
