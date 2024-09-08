using PBASE.Entity;
using PBASE.Entity.Enum;
using PBASE.Service;
using PBASE.WebAPI.Helpers;
using PBASE.WebAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;
using System.Linq;
using System.Web.Configuration;
using System.Text;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using static PBASE.Entity.Lookup;
using OfficeOpenXml;

namespace PBASE.WebAPI.Controllers
{

    [Authorize]
    public abstract class BaseController : ApiController
    {

        #region Model Copier

        public void CopyEntityToViewModel(object from, object to)
        {
            ModelCopier.CopyModel(from, to);
            //
            // Default form mode is always Edit.
        }

        public void CopyViewModelToEntity(object from, object to)
        {
            ModelCopier.CopyModel(from, to);
        }

        public void CopyEntityToEntity(object from, object to)
        {
            ModelCopier.CopyModel(from, to);
        }

        #endregion

        #region Attachment

        /// <summary>
        /// Method will provide Stream from given url.
        /// </summary>
        /// <param name="url">Remote address of file containing binary data.</param>
        /// <returns>Byte array containing binary data.</returns>
        protected Stream StreamFromUrl(string url)
        {
            int readSize; // bytes read from response stream
            const int BUFFERSIZE = 5120;
            byte[] buffer = new byte[BUFFERSIZE]; // 5MB buffer to read from response and write to memory stream
            MemoryStream memoryStream;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;

            WebRequest webRequest = WebRequest.Create(url);

            using (WebResponse serverResponse = webRequest.GetResponse())
            {
                using (Stream responseStream = serverResponse.GetResponseStream())
                {
                    memoryStream = new MemoryStream();
                    while ((readSize = responseStream.Read(buffer, 0, buffer.Length)) > 0)
                        memoryStream.Write(buffer, 0, readSize);
                }
            }

            memoryStream.Position = 0L;
            return memoryStream;
        }
        /// <summary>
        /// Method will provide Stream from given File Handle.
        /// </summary>
        /// <param name="fileHandle">File Handle</param>
        /// <returns>Byte array containing binary data.</returns>
        //protected Stream StreamFromFileHandle(string fileHandle)
        //{
        //    return StreamFromUrl("https://www.filepicker.io/api/file/" + fileHandle);
        //}
        /// <summary>
        /// Method will download binary data from given url and return a byte array containing that data.
        /// </summary>
        /// <param name="url">Remote address of file containing binary data.</param>
        /// <returns>Byte array containing binary data.</returns>
        //protected byte[] ByteArrayFromUrl(string url)
        //{
        //    return ((MemoryStream)StreamFromUrl(url)).ToArray();
        //}
        /// <summary>
        /// Method will download binary data from Ink File Picker cloud and return a byte array containing that data.
        /// </summary>
        /// <param name="fileHandle">File handle of file containing binary data.</param>
        /// <returns>Byte array containing binary data.</returns>
        //protected byte[] ByteArrayFromFileHandle(string fileHandle)
        //{
        //    return ByteArrayFromUrl("https://www.filepicker.io/api/file/" + fileHandle); // Make url from file handle and pass to UrlToByteArray, return the result.
        //}
        /// <summary>
        /// Upload the given file to Server via InkFilePicker
        /// </summary>
        /// <param name="fileToUpload">File to be uploaded</param>
        //protected string UploadFile(Stream fileToUpload)
        //{
        //    fileToUpload.Position = 0;
        //    string URL = "https://www.filepicker.io/api/store/azure" + "?key=" + WebConfigurationManager.AppSettings["UploadPluginAPIKey"].ToString();

        //    HttpWebRequest req = (HttpWebRequest)WebRequest.Create(URL);

        //    req.Method = "POST";
        //    req.ContentLength = fileToUpload.Length;
        //    req.AllowWriteStreamBuffering = true;
        //    Stream reqStream = req.GetRequestStream();
        //    byte[] inData = fileToUpload.ReadAllBytes();

        //    // put data into request stream
        //    reqStream.Write(inData, 0, (int)fileToUpload.Length);
        //    fileToUpload.Close();

        //    HttpWebResponse response = (HttpWebResponse)req.GetResponse();
        //    Stream receiveStream = response.GetResponseStream();

        //    StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
        //    string requestResult = readStream.ReadToEnd();
        //    reqStream.Close();
        //    receiveStream.Close();
        //    readStream.Close();
        //    Newtonsoft.Json.Linq.JObject jsonResponse = Newtonsoft.Json.Linq.JObject.Parse(requestResult);
        //    string thumbnailUrl = (string)jsonResponse.SelectToken("url");
        //    return thumbnailUrl.Substring(thumbnailUrl.LastIndexOf("/") + 1);
        //}

        //protected string UploadFile(Stream fileToUpload, string filename)
        //{
        //    fileToUpload.Position = 0;
        //    string URL = "https://www.filepicker.io/api/store/azure" + "?key=" + WebConfigurationManager.AppSettings["UploadPluginAPIKey"].ToString() + "&filename=" + filename + "";

        //    HttpWebRequest req = (HttpWebRequest)WebRequest.Create(URL);

        //    req.Method = "POST";
        //    req.ContentLength = fileToUpload.Length;
        //    req.AllowWriteStreamBuffering = true;
        //    Stream reqStream = req.GetRequestStream();
        //    byte[] inData = fileToUpload.ReadAllBytes();

        //    // put data into request stream
        //    reqStream.Write(inData, 0, (int)fileToUpload.Length);
        //    fileToUpload.Close();

        //    HttpWebResponse response = (HttpWebResponse)req.GetResponse();
        //    Stream receiveStream = response.GetResponseStream();

        //    StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
        //    string requestResult = readStream.ReadToEnd();
        //    reqStream.Close();
        //    receiveStream.Close();
        //    readStream.Close();
        //    Newtonsoft.Json.Linq.JObject jsonResponse = Newtonsoft.Json.Linq.JObject.Parse(requestResult);
        //    string thumbnailUrl = (string)jsonResponse.SelectToken("url");
        //    return thumbnailUrl.Substring(thumbnailUrl.LastIndexOf("/") + 1);
        //}
        /// <summary>
        /// Will handle Attachment and return the AttachmentId of saved Attachment
        /// </summary>
        /// <param name="lookupService">Service for Attachment</param>
        /// <param name="attachmentId">AttachmentId</param>
        /// <param name="fileHandle">fileHandle</param>
        /// <param name="unsavedFileHandle">unSavedFileHandle</param>
        /// <returns></returns>

            
        

        //public int? ProcessAttchment(ILookupService lookupService, int? attachmentId, string fileHandle, string unsavedFileHandle)
        //{
        //    if (attachmentId.HasValue) // Update or Remove
        //    {
        //        if (!string.IsNullOrEmpty(fileHandle) && (fileHandle == unsavedFileHandle)) return attachmentId;

        //        Attachment attachment = lookupService.SelectByAttachmentId(attachmentId.Value);
        //        attachment.AttachmentFileHandle = fileHandle;
        //        //attachment.AttachmentFileName = "";
        //        lookupService.SaveAttachmentForm(attachment);

        //        //if (fileHandle != null) // Remove
        //        //{
        //        //    RemoveAttachmentFile(fileHandle);
        //        //}
        //    }
        //    else  // Add
        //    {
        //        if (fileHandle != null)
        //        {
        //            Attachment attachment = new Attachment
        //            {
        //                //AttachmentFileName = "",
        //                AttachmentFileHandle = fileHandle,
        //                //AttachmentFileSize = 0,
        //                //AttachmentFileType = "",
        //                //IsArchived = false,
        //                FormMode = FormMode.Create
        //            };

        //            lookupService.SaveAttachmentForm(attachment);
        //            return attachment.AttachmentId;
        //        }
        //    }
        //    return attachmentId;
        //}
        /// <summary>
        /// Will handle Attachment and return the AttachmentId of saved Attachment
        /// </summary>
        /// <param name="lookupService">Service for Attachment</param>
        /// <param name="attachmentId">AttachmentId</param>
        /// <param name="fileHandle">fileHandle</param>
        /// <param name="unsavedFileHandle">unSavedFileHandle</param>
        /// <param name="uploadFile">AttachmentFileName</param>
        /// <returns></returns>
        /// 


        //public int? ProcessAttchment(ILookupService lookupService, int? attachmentId, string fileHandle, string unsavedFileHandle, string uploadFile)
        //{
        //    if (attachmentId.HasValue) // Update or Remove
        //    {
        //        if (!string.IsNullOrEmpty(fileHandle) && (fileHandle == unsavedFileHandle)) return attachmentId;

        //        Attachment attachment = lookupService.SelectByAttachmentId(attachmentId.Value);
        //        attachment.AttachmentFileHandle = fileHandle;
        //        attachment.AttachmentFileName = uploadFile;
        //        lookupService.SaveAttachmentForm(attachment);

        //        //if (fileHandle != null) // Remove
        //        //{
        //        //    RemoveAttachmentFile(fileHandle);
        //        //}
        //    }
        //    else  // Add
        //    {
        //        if (fileHandle != null)
        //        {
        //            Attachment attachment = new Attachment
        //            {
        //                AttachmentFileName = uploadFile,
        //                AttachmentFileHandle = fileHandle,
        //                AttachmentFileSize = 0,
        //                AttachmentFileType = "",
        //                //IsArchived = false,
        //                FormMode = FormMode.Create
        //            };

        //            lookupService.SaveAttachmentForm(attachment);
        //            return attachment.AttachmentId;
        //        }
        //    }
        //    return attachmentId;
        //}

        protected bool RemoveAttachmentFile(string fileHandle)
        {
            return true;
        }

        protected bool RemoveAttachmentRecord(ILookupService lookupService, int attachmentId)
        {
            return true;
        }

        //public int? ProcessAttchment(ILookupService lookupService, Attachment attachmentToSave)
        //{
        //    if (attachmentToSave.AttachmentId != 0) // Update
        //    {
        //        Attachment attachment = lookupService.SelectByAttachmentId(attachmentToSave.AttachmentId);
        //        if (attachment == null)
        //        {
        //            return null;
        //        }
        //        if (attachmentToSave.AttachmentFileHandle != null)
        //        {
        //            attachment.AttachmentFileName = attachmentToSave.AttachmentFileName;
        //            attachment.AttachmentFileSize = attachmentToSave.AttachmentFileSize;
        //            attachment.AttachmentFileType = attachmentToSave.AttachmentFileType;
        //            attachment.AttachmentFileHandle = attachmentToSave.AttachmentFileHandle;
        //            attachment.IsArchived = attachmentToSave.IsArchived;
        //            lookupService.SaveAttachmentForm(attachment);
        //            return attachment.AttachmentId;
        //        }
        //    }
        //    else  // Add
        //    {
        //        if (attachmentToSave.AttachmentFileHandle != null)
        //        {
        //            Attachment attachment = new Attachment
        //            {
        //                FormMode = FormMode.Create,
        //                AttachmentFileName = attachmentToSave.AttachmentFileName,
        //                AttachmentFileSize = attachmentToSave.AttachmentFileSize,
        //                AttachmentFileType = attachmentToSave.AttachmentFileType,
        //                AttachmentFileHandle = attachmentToSave.AttachmentFileHandle,
        //                IsArchived = attachmentToSave.IsArchived,
        //            };
        //            lookupService.SaveAttachmentForm(attachment);
        //            return attachment.AttachmentId;
        //        }
        //    }
        //    return null;
        //}

        #endregion

        #region Download File

        /// <summary>
        /// Method will download binary data from given url and return a byte array containing that data.
        /// </summary>
        /// <param name="url">Remote address of file containing binary data.</param>
        /// <returns>Byte array containing binary data.</returns>
        protected byte[] ByteArrayFromUrl(string url)
        {
            return ((MemoryStream)StreamFromUrl(url)).ToArray();
        }

        /// <summary>
        /// Method will download binary data from Ink File Picker cloud and return a byte array containing that data.
        /// </summary>
        /// <param name="fileHandle">File handle of file containing binary data.</param>
        /// <returns>Byte array containing binary data.</returns>
        protected byte[] ByteArrayFromFileHandle(string fileHandle)
        {
            return ByteArrayFromUrl(GetFileUrl(fileHandle)); // Make url from file handle and pass to UrlToByteArray, return the result.
        }

        /// <summary>
        /// Method will provide stream from given url and return a byte array containing that data.
        /// </summary>
        /// <param name="url">Remote address of file</param>
        /// <returns>Stream containing uploaded file data.</returns>
        //protected Stream StreamFromUrl(string url)
        //{
        //    int readSize; // bytes read from response stream
        //    const int BUFFERSIZE = 5120;
        //    byte[] buffer = new byte[BUFFERSIZE]; // 5MB buffer to read from response and write to memory stream
        //    MemoryStream memoryStream = new MemoryStream(); // 5MB buffer to read from response and write to memory stream

        //    WebRequest webRequest = WebRequest.Create(url);

        //    using (WebResponse serverResponse = webRequest.GetResponse())
        //    {
        //        using (Stream responseStream = serverResponse.GetResponseStream())
        //        {
        //            while ((readSize = responseStream.Read(buffer, 0, buffer.Length)) > 0)
        //                memoryStream.Write(buffer, 0, readSize);
        //        }
        //    }
        //    memoryStream.Position = 0;
        //    return memoryStream;
        //}

        /// <summary>
        /// Method will download binary data from Ink File Picker cloud and return a byte array containing that data.
        /// </summary>
        /// <param name="fileHandle">File handle of file containing binary data.</param>
        /// <returns>Stream containing uploaded file data.</returns>
        protected Stream StreamFromFileHandle(string fileHandle)
        {
            return StreamFromUrl(GetFileUrl(fileHandle)); // Make url from file handle and pass to UrlToByteArray, return the result.
        }

        protected string GetFileUrl(string fileHandle)
        {
            return "https://cdn.filepicker.io/api/file/" + fileHandle + "?key=" + System.Configuration.ConfigurationManager.AppSettings["FileStackKey"] + "&signature=" + System.Configuration.ConfigurationManager.AppSettings["FileStackSignature"] + "&policy=" + System.Configuration.ConfigurationManager.AppSettings["FileStackPolicy"];
        }

        protected Stream StreamFromPdfFileHandle(string fileHandle, string docType)
        {
            return StreamFromUrl(GetPdfStream(fileHandle, docType)); // Make url from file handle and pass to UrlToByteArray, return the result.
        }
        protected string GetPdfStream(string fileHandle, string docType)
        {
            return "https://process.filestackapi.com/output=format:" + docType + "/" + fileHandle;
        }

        #endregion

        protected void GenerateExcelFile(string fileName, byte[] fileBytes)
        {
            HttpContext.Current.Response.Clear();
            //Default file name for "Save As.." after downloading.
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=\"" + fileName + "\"");
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.BinaryWrite(fileBytes);

            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.Close();
        }

        protected ExcelWorksheet GenerateExcelExportLabels(ExcelWorksheet worksheet, List<string> exportColumnsLabels)
        {
            int row = 0;
            foreach (var item in exportColumnsLabels)
            {
                row++;
                worksheet.Cells[1, row].Value = item;
                worksheet.Cells[1, row].StyleID = worksheet.Cells[1, row].StyleID;
                worksheet.Cells[1, row].Style.Font.Bold = true;
            }
            return worksheet;
        }
        protected ExcelWorksheet GenerateExcelExportData(ExcelWorksheet worksheet, List<string> exportColumns, dynamic data)
        {
            int row = 2;
            foreach (var item in data)
            {
                Type type = item.GetType();
                for (int m = 0; m < exportColumns.ToList().Count(); m++)
                {
                    var property = type.GetProperty(exportColumns[m]);
                    if (property != null)
                    {
                        if (property.PropertyType == typeof(DateTime?) || property.PropertyType == typeof(DateTime))
                        {
                            DateTime? dateTime = property.GetValue(item, null);
                            worksheet.Cells[row, m + 1].Value = dateTime.IsNotNull() ? CreatedUpdateList.Contains(exportColumns[m]) ? dateTime.GetSpecialFormattedDateValue() : dateTime.Value.TimeOfDay.Hours > 0 ? dateTime.GetFormattedTime24hrs() : !SpecialDateFromToList.Contains(exportColumns[m]) ? dateTime.GetSpecialFormattedDateValue() : dateTime.GetSpecialFormattedDateValue() : "";
                        }
                        else if (property.PropertyType == typeof(decimal?) || property.PropertyType == typeof(decimal))
                        {
                            decimal? numberDecimal = property.GetValue(item, null);
                            worksheet.Cells[row, m + 1].Value = numberDecimal.IsNotNull() ? numberDecimal.Value.GetFormattedValue() : "";
                        }
                        else if (property.PropertyType == typeof(bool?) || property.PropertyType == typeof(bool))
                        {
                            bool? isBoolean = property.GetValue(item, null);
                            worksheet.Cells[row, m + 1].Value = isBoolean.IsNotNull() ? isBoolean.Value.GetFormattedValue() : "";
                        }
                        else if (property.PropertyType == typeof(TimeSpan?) || property.PropertyType == typeof(TimeSpan))
                        {
                            TimeSpan? time = property.GetValue(item, null);
                            worksheet.Cells[row, m + 1].Value = time.IsNotNull() ? time.GetFormattedTime24hrs() : "";
                        }
                        else
                        {
                            worksheet.Cells[row, m + 1].Value = property.GetValue(item, null) ?? "";
                        }
                    }

                }
                row++;
            }
            return worksheet;
        }

        public int? ProcessAttchment(ILookupService lookupService, Attachment attachmentToSave)
        {
            if (attachmentToSave.AttachmentId != 0) // Update
            {
                Attachment attachment = lookupService.SelectByAttachmentId(attachmentToSave.AttachmentId);
                if (attachment == null)
                {
                    return null;
                }
                if (attachmentToSave.AttachmentFileHandle != null)
                {
                    attachment.AttachmentFileName = attachmentToSave.AttachmentFileName;
                    attachment.AttachmentFileSize = attachmentToSave.AttachmentFileSize;
                    attachment.AttachmentFileType = attachmentToSave.AttachmentFileType;
                    attachment.AttachmentFileHandle = attachmentToSave.AttachmentFileHandle;
                    attachment.IsArchived = attachmentToSave.IsArchived;
                    lookupService.SaveAttachmentForm(attachment);
                    return attachment.AttachmentId;
                }
            }
            else  // Add
            {
                if (attachmentToSave.AttachmentFileHandle != null)
                {
                    Attachment attachment = new Attachment
                    {
                        FormMode = FormMode.Create,
                        AttachmentFileName = attachmentToSave.AttachmentFileName,
                        AttachmentFileSize = attachmentToSave.AttachmentFileSize,
                        AttachmentFileType = attachmentToSave.AttachmentFileType,
                        AttachmentFileHandle = attachmentToSave.AttachmentFileHandle,
                        ConnectedTable = attachmentToSave.ConnectedTable,
                        ConnectedField = attachmentToSave.ConnectedField,
                        ConnectedId = attachmentToSave.ConnectedId,
                        IsArchived = attachmentToSave.IsArchived,
                    };
                    lookupService.SaveAttachmentForm(attachment);
                    attachment.ConnectedId = attachment.AttachmentId;
                    attachment.FormMode = FormMode.Edit;
                    lookupService.SaveAttachmentForm(attachment);
                    return attachment.AttachmentId;
                }
            }
            return null;
        }

        protected string Base64EncodedFile(byte[] fileBytes)
        {
            return Convert.ToBase64String(fileBytes);
        }

        public List<AllLookupView> LoadLookupExtraIntOptions(string viewName, string IdFieldName, string displayFieldName)
        {
            var list = new List<AllLookupView>();

            var serviceName = "PBASE.Service";
            var referenceAssemblyNames = Assembly.GetExecutingAssembly().GetReferencedAssemblies();
            var serviceAssemblyName = referenceAssemblyNames.FirstOrDefault(x => x.Name.Equals(serviceName));

            if (serviceAssemblyName != null)
            {
                var serviceAssembly = Assembly.Load(serviceAssemblyName);
                var viewFullName = serviceName + ".LookupService";

                Type viewMethodType = serviceAssembly.GetType(viewFullName);
                if (viewMethodType != null)
                {
                    object[] parametersArray = new object[] { };
                    object classInstance = GlobalConfiguration.Configuration.DependencyResolver.GetService(viewMethodType);
                    dynamic viewData = classInstance.InvokeMethod("SelectAll" + ConvertWordToPlural(viewName), parametersArray);

                    foreach (var item in viewData)
                    {
                        var idFieldValue = item.GetType().GetProperty(IdFieldName).GetValue(item, null);
                        var disabledFieldValue = item.GetType().GetProperty("IsArchived").GetValue(item, null);
                        var displayFieldValue = item.GetType().GetProperty(displayFieldName).GetValue(item, null);

                        //list.Add(new SelectListItem() { Value = idFieldValue.ToString(), Text = displayFieldValue.ToString() });
                        list.Add(new AllLookupView() { Value = idFieldValue, Text = displayFieldValue.ToString(), disabled = disabledFieldValue? disabledFieldValue: false, GroupBy = disabledFieldValue ? "ARCHIVED" : "ACTIVE" });
                    }
                }
            }

            return list;
        }

        private string ConvertWordToPlural(string word)
        {
            string value = word;

            Regex g = new Regex(@"s\b|z\b|x\b|sh\b|ch\b|ss\b|to\b|ro\b|ho\b|jo\b");
            Regex h = new Regex(@"lf\b|af\b|ef\b|sh\b|f\b");
            Regex i = new Regex(@"fe\b");
            Regex j = new Regex(@"py\b|ly\b|ny\b|ty\b|cy\b");
            Regex k = new Regex(@"ts\b|ies\b|es\b|ves\b|ys\b");

            MatchCollection matche1 = g.Matches(value);
            MatchCollection matche2 = h.Matches(value);
            MatchCollection matche3 = i.Matches(value);
            MatchCollection matche4 = j.Matches(value);
            MatchCollection matche5 = k.Matches(value);

            if (matche5.Count > 0)
            {
                if (value.EndsWith(matche5.ToString()))
                {
                    value = value.ToString();
                }
            }
            else if (matche1.Count > 0)
            {
                value += "es";
            }
            else if (matche2.Count > 0)
            {
                value = value.Remove(value.Length - 1, 1) + "ves";
            }
            else if (matche3.Count > 0)
            {
                value = value.Remove(value.Length - 2, 2) + "ves";
            }
            else if (matche4.Count > 0)
            {
                value = value.Remove(value.Length - 1, 1) + "ies";
            }
            else
            {
                value += "s";
            }

            return value.ToString();
        }


        protected string UploadFile(Stream fileToUpload, string filename)
        {
            fileToUpload.Position = 0;
            string URL = "https://www.filepicker.io/api/store/azure" +
                "?key=" + WebConfigurationManager.AppSettings["FileStackKey"].ToString() +
                "&signature=" + System.Configuration.ConfigurationManager.AppSettings["FileStackSignature"] +
                "&policy=" + System.Configuration.ConfigurationManager.AppSettings["FileStackPolicy"] +
                "&container=" + System.Configuration.ConfigurationManager.AppSettings["FileStackContainer"] +
                "&filename=" + filename;

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(URL);

            req.Method = "POST";
            req.ContentLength = fileToUpload.Length;
            req.AllowWriteStreamBuffering = true;
            Stream reqStream = req.GetRequestStream();
            byte[] inData = fileToUpload.ReadAllBytes();

            // put data into request stream
            reqStream.Write(inData, 0, (int)fileToUpload.Length);
            fileToUpload.Close();

            HttpWebResponse response = (HttpWebResponse)req.GetResponse();
            Stream receiveStream = response.GetResponseStream();

            StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
            string requestResult = readStream.ReadToEnd();
            reqStream.Close();
            receiveStream.Close();
            readStream.Close();
            Newtonsoft.Json.Linq.JObject jsonResponse = Newtonsoft.Json.Linq.JObject.Parse(requestResult);
            string thumbnailUrl = (string)jsonResponse.SelectToken("url");
            return thumbnailUrl.Substring(thumbnailUrl.LastIndexOf("/") + 1);
        }

        public static string GetTitle(string file)
        {
            Match m = Regex.Match(file, @"<title>\s*(.+?)\s*</title>");
            if (m.Success)
            {
                return m.Groups[0].Value;
            }
            else
            {
                return "";
            }
        }
        List<string> CreatedUpdateList = new List<string>(new[]
        {
            "CreatedDate",
            "UpdatedDate",
            "ClosedDateTime",
            "OpenDateTime"
        });


        protected int GetUserId()
        {
            return User.Identity.GetUserId<int>();
        }

        protected int IntParseId(string id)
        {
            int intId = int.MinValue;
            int.TryParse(id, out intId);
            return intId;
        }

        List<string> SpecialDateFromToList = new List<string>(new[]
{
            "DateFrom",
            "OpenFrom",
            "DateTo",
            "OpenTo"
        });
    }
}
