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
    [MenuPermission(Menus.Users)]
    public class vw_UserGridController : BaseController
    {
        #region Initialization

       
        private readonly ILookupService lookupService;

        public vw_UserGridController(ILookupService lookupService)
        {
            this.lookupService = lookupService;
        }

        #endregion Initialization

        // GET: api/vw_UserGrid/{GridSetting}
        public async Task<IHttpActionResult> Get([ModelBinder(typeof(APIGridModelBinder))]GridSetting gridSetting)
        {
            var data = await lookupService.Selectvw_UserGridsByGridSettingAsync(gridSetting);
            var pagedResult = new PagedResult<vw_UserGrid>();
            pagedResult.Results = data;
            pagedResult.RowCount = gridSetting.Count;
            return Ok(pagedResult);
        }      
    }
}