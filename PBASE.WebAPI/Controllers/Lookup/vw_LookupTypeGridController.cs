using System.Web.Http;
using System.Threading.Tasks;
using Probase.GridHelper;
using System.Web.Http.ModelBinding;
using PBASE.Entity;
using PBASE.Service;
using PBASE.WebAPI.Helpers;
using PBASE.Repository.Infrastructure;

namespace PBASE.WebAPI.Controllers
{
    [MenuPermission(Menus.Lookups)]

    public partial class vw_LookupTypeGridController : BaseController
    {
        #region Initialization

        private readonly ILookupService lookupService;

        public vw_LookupTypeGridController(ILookupService lookupService
        )
        {
            this.lookupService = lookupService;
        }

        #endregion Initialization

        [HttpGet]
        // GET: api/vw_LookupTypeGrid/{GridSetting}
        public async Task<IHttpActionResult> Get([ModelBinder(typeof(APIGridModelBinder))]GridSetting gridSetting)
        {
            var data = await lookupService.Selectvw_LookupTypeGridsByGridSettingAsync(gridSetting);
            var pagedResult = new PagedResult<vw_LookupTypeGrid>();
            pagedResult.Results = data;
            pagedResult.RowCount = gridSetting.Count;
            return Ok(pagedResult);
        }
    }
}

