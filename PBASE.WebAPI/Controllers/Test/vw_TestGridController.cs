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
    [MenuPermission(Menus.Test)]
    public class vw_TestGridController : BaseController
    {
        #region Initialization

       
        private readonly ITestService testService;

        public vw_TestGridController(ITestService testService)
        {
            this.testService = testService;
        }

        #endregion Initialization

        // GET: api/vw_TestGrid/{GridSetting}
        public async Task<IHttpActionResult> Get([ModelBinder(typeof(APIGridModelBinder))]GridSetting gridSetting)
        {
            var data = await testService.Selectvw_TestGridsByGridSettingAsync(gridSetting);
            var pagedResult = new PagedResult<vw_TestGrid>();
            pagedResult.Results = data;
            pagedResult.RowCount = gridSetting.Count;
            return Ok(pagedResult);
        }      
    }
}