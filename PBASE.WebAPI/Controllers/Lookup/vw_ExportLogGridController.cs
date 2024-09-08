using Probase.GridHelper;
using PBASE.Entity;
using PBASE.Repository.Infrastructure;
using PBASE.Service;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using PBASE.WebAPI;
using PBASE.WebAPI.Controllers;
using System.Threading.Tasks;
using PBASE.WebAPI.Helpers;

namespace PBASE.WebAPI.Controllers
{
    [MenuPermission(Menus.ExportLog)]
    public class vw_ExportLogGridController : BaseController
    {
        #region Initialization

       
        private readonly ILookupService lookupService;

        public vw_ExportLogGridController(ILookupService lookupService)
        {
            this.lookupService = lookupService;
        }

        #endregion Initialization
        // GET: api/vw_LookupTypeGrid/{GridSetting}
        public async Task<IHttpActionResult> Getvw_ExportLogGrid([ModelBinder(typeof(APIGridModelBinder))]GridSetting gridSetting)
        {
            var data = await lookupService.Selectvw_ExportLogGridsByGridSettingAsync(gridSetting);
            var pagedResult = new PagedResult<vw_ExportLogGrid>();
            pagedResult.Results = data;
            pagedResult.RowCount = gridSetting.Count;
            return Ok(pagedResult);
        }      
    }
}