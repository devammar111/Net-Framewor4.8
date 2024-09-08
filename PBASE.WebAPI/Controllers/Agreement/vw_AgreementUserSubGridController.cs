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
    public class vw_AgreementUserSubGridController : BaseController
    {
        #region Initialization

       
        private readonly IAgreementService agreementService;

        public vw_AgreementUserSubGridController(IAgreementService agreementService)
        {
            this.agreementService = agreementService;
        }

        #endregion Initialization

        // GET: api/vw_EmailGrid/{GridSetting}
        public async Task<IHttpActionResult> Get([ModelBinder(typeof(APIGridModelBinder))]GridSetting gridSetting)
        {
            var data = await agreementService.Selectvw_AgreementUserSubGridsByGridSettingAsync(gridSetting);
            var pagedResult = new PagedResult<vw_AgreementUserSubGrid>();
            pagedResult.Results = data;
            pagedResult.RowCount = gridSetting.Count;
            return Ok(pagedResult);
        }      
    }
}