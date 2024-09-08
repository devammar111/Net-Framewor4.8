using Probase.GridHelper;
using PBASE.Entity;
using PBASE.Repository.Infrastructure;
using PBASE.Service;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using PBASE.WebAPI.Controllers;
using System.Threading.Tasks;
using PBASE.WebAPI.Helpers;
using System;

namespace PBASE.WebAPI.Controllers
{
    //[MenuPermission(Menus.Agreements)]
    public class vw_UserAgreementSubGridController : BaseController
    {
        #region Initialization

       
        private readonly IAgreementService agreementService;

        public vw_UserAgreementSubGridController(IAgreementService agreementService)
        {
            this.agreementService = agreementService;
        }

        #endregion Initialization

        // GET: api/vw_EmailGrid/{GridSetting}
        public async Task<IHttpActionResult> Get([ModelBinder(typeof(APIGridModelBinder))]GridSetting gridSetting)
        {
            var data = await agreementService.Selectvw_UserAgreementSubGridsByGridSettingAsync(gridSetting);
            var pagedResult = new PagedResult<vw_UserAgreementSubGrid>();
            pagedResult.Results = data;
            pagedResult.RowCount = gridSetting.Count;
            return Ok(pagedResult);
        }

        [HttpGet]
        [Route("api/GetAgreementData/{id}")]
        [Decrypt("id")]
        public async Task<IHttpActionResult> GetAgreementData([FromUri]string id)
        {
            Agreement Agreement = await agreementService.SelectByAgreementIdAsync(Convert.ToInt32(id));
            if (Agreement != null)
            {
                return Ok(Agreement.AgreementText);
            }
            else
            {
                return NotFound();
            }
        }
    }
}