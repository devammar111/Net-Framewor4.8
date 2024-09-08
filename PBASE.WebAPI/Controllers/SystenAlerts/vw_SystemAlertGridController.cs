using Probase.GridHelper;
using PBASE.Entity;
using PBASE.Repository.Infrastructure;
using PBASE.Service;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using PBASE.WebAPI.Controllers;
using System.Threading.Tasks;
using PBASE.WebAPI.Helpers;

namespace PBASE.WebAPI.Controllers
{
    //[MenuPermission(Menus.Agreements)]
    public class vw_SystemAlertGridController : BaseController
    {
        #region Initialization

       
        private readonly ISystemAlertService systemAlertService;

        public vw_SystemAlertGridController(ISystemAlertService systemAlertService)
        {
            this.systemAlertService = systemAlertService;
        }

        #endregion Initialization

        // GET: api/vw_EmailGrid/{GridSetting}
        public async Task<IHttpActionResult> Get([ModelBinder(typeof(APIGridModelBinder))]GridSetting gridSetting)
        {
            var data = await systemAlertService.Selectvw_SystemAlertGridsByGridSettingAsync(gridSetting);
            var pagedResult = new PagedResult<vw_SystemAlertGrid>();
            pagedResult.Results = data;
            pagedResult.RowCount = gridSetting.Count;
            return Ok(pagedResult);
        }      
    }
}