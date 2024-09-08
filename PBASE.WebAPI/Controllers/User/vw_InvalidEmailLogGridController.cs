using Probase.GridHelper;
using PBASE.Entity;
using PBASE.Repository.Infrastructure;
using PBASE.Service;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Threading.Tasks;
using PBASE.WebAPI.Helpers;

namespace PBASE.WebAPI.Controllers
{
    [MenuPermission(Menus.InvalidEmailLog)]
    public class vw_InvalidEmailLogGridController : BaseController
    {
        #region Initialization
        private readonly IUserService userService;
        public vw_InvalidEmailLogGridController(IUserService userService)
        {
            this.userService = userService;
        }
        #endregion Initialization

        // GET: api/vw_InvalidEmailLogGrid/{GridSetting}
        public async Task<IHttpActionResult> Get([ModelBinder(typeof(APIGridModelBinder))]GridSetting gridSetting)
        {
            var data = await userService.Selectvw_InvalidEmailLogGridsByGridSettingAsync(gridSetting);
            var pagedResult = new PagedResult<vw_InvalidEmailLogGrid>();
            pagedResult.Results = data;
            pagedResult.RowCount = gridSetting.Count;
            return Ok(pagedResult);
        }      
    }
}