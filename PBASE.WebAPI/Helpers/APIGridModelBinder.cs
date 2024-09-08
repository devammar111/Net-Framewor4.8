using Newtonsoft.Json;
using PBASE.Entity.Helper;
using Probase.GridHelper;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Http.ModelBinding;

namespace PBASE.WebAPI.Helpers
{
    public class APIGridModelBinder : IModelBinder
    {
        private const string DROPDOWN_VALUE_IDENTIFIER = "^";

        public bool BindModel(
        System.Web.Http.Controllers.HttpActionContext actionContext,
        System.Web.Http.ModelBinding.ModelBindingContext bindingContext)
        {
            try
            {
                var arguments = HttpUtility.ParseQueryString(actionContext.Request.RequestUri.Query);
                GridSetting gridSettings = new GridSetting
                {
                    IsSearch = bool.Parse(arguments["_search"] ?? "false"),
                    PageIndex = int.Parse(arguments["page"] ?? "1"),
                    PageSize = int.Parse(arguments["pageSize"] ?? "10"),
                    SortColumn = arguments["colId"] ?? "",
                    SortOrder = arguments["sort"] ?? "asc",
                    Where = JsonConvert.DeserializeObject<Filter>(arguments["filters"] ?? ""),
                    filters = arguments["filters"] ?? "",
                    IsExcelExport = bool.Parse(arguments["isExcelExport"] ?? "false"),
                    SpecialTotalRows = int.Parse(arguments["totalrows"] ?? "0")
                };


                foreach (var rule in gridSettings.Where.rules)
                {
                    if (rule.field.Contains("Id") || rule.field.Contains("Key"))
                    {
                        try
                        {
                            rule.data = CryptoEngine.Decrypt(rule.data);
                        }
                        catch (Exception)
                        {
                            //Donothing
                        }
                    }
                }

                // filters field may contain ^ special character. ^ special character is added when dropdown value is selected.
                //gridSettings.filters = gridSettings.filters.Replace(DROPDOWN_VALUE_IDENTIFIER, "");

                //if (gridSettings.SortColumn.Contains(" asc, "))
                //{
                //    gridSettings.SortColumn = gridSettings.SortColumn.Replace(" asc, ", "");
                //}

                if (gridSettings.SpecialTotalRows > 0)
                {
                    gridSettings.PageSize = gridSettings.SpecialTotalRows;
                }

                List<Rule> quickSearchWhereList = new List<Rule>();
                // If quick search criteria comes, then it will come after sixth element.
                //for (int i = 0; i < arguments.Keys.Count; i++)
                //{
                //    // Oper option indicates that user is trying export excel.
                //    if ((arguments.Keys[i].Equals("oper")))
                //    {
                //        gridSettings.IsExcelExport = true;
                //    }
                //    else if ((arguments.Keys[i].Equals("ExportServiceName")))
                //    {
                //        gridSettings.ExcelExportServiceName = arguments[i].ToString();
                //    }
                //    else if ((arguments.Keys[i].Equals("ExportMethodName")))
                //    {
                //        gridSettings.ExcelExportMethodName = arguments[i].ToString();
                //    }
                //    else if (
                //        // Not sure, why field with "_" name is added. Skip that field.
                //            !(arguments.Keys[i].Equals("_search"))
                //        && (!(arguments.Keys[i].Equals("page")))
                //        && (!(arguments.Keys[i].Equals("rows")))
                //        && (!(arguments.Keys[i].Equals("sidx")))
                //        && (!(arguments.Keys[i].Equals("sord")))
                //        && (!(arguments.Keys[i].Equals("filters")))
                //        && (!(arguments.Keys[i].Equals("totalrows")))
                //        && (!(arguments.Keys[i].Equals("_")))
                //        && (!(arguments.Keys[i].Equals("searchField")))
                //        && (!(arguments.Keys[i].Equals("searchString")))
                //        && (!(arguments.Keys[i].Equals("searchOper")))
                //        && (!(arguments.Keys[i].Equals("idToIgnore")))

                //        && (!(arguments.Keys[i].Equals("exportGridId")))
                //        && (!(arguments.Keys[i].Equals("exportColumnIds")))
                //        && (!(arguments.Keys[i].Equals("exportColumnLabels")))

                //        && (!(arguments.Keys[i].Equals("nd")))
                //        )
                //    {
                //        var value = arguments[i].ToString();
                //        if (value.StartsWith(DROPDOWN_VALUE_IDENTIFIER))
                //        {
                //            // Dropdown quicktoolbar selection.
                //            quickSearchWhereList.Add(new Rule() { field = arguments.Keys[i], data = value.Replace(DROPDOWN_VALUE_IDENTIFIER, ""), op = "eq" });
                //        }
                //        else
                //        {
                //            // normal quicktoolbar search.
                //            quickSearchWhereList.Add(new Rule() { field = arguments.Keys[i], data = value, op = "cn" });
                //        }
                //    }
                //}

                gridSettings.QuickSearchWhereList = quickSearchWhereList;
                //return gridSettings;
                bindingContext.Model = gridSettings;
            }
            catch (Exception ex)
            {
                bindingContext.ModelState.AddModelError("Grid binding Exception", ex.ToString());
            }

            return true;
        }

    }
}
