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
    [MenuPermission(Menus.Email)]
    public class vw_EmailTemplateGridController : BaseController
    {
        #region Initialization

       
        private readonly IEmailTemplateService emailtemplateService;

        public vw_EmailTemplateGridController(IEmailTemplateService emailtemplateService)
        {
            this.emailtemplateService = emailtemplateService;
        }

        #endregion Initialization

        // GET: api/vw_EmailTemplateGrid/{GridSetting}
        public async Task<IHttpActionResult> Get([ModelBinder(typeof(APIGridModelBinder))]GridSetting gridSetting)
        {
            var data = await emailtemplateService.Selectvw_EmailTemplateGridsByGridSettingAsync(gridSetting);
            var pagedResult = new PagedResult<vw_EmailTemplateGrid>();
            pagedResult.Results = data;
            pagedResult.RowCount = gridSetting.Count;
            return Ok(pagedResult);
        }      
    }
}