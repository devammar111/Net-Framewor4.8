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
    public class vw_EmailGridController : BaseController
    {
        #region Initialization

       
        private readonly IEmailService emailService;

        public vw_EmailGridController(IEmailService emailService)
        {
            this.emailService = emailService;
        }

        #endregion Initialization

        // GET: api/vw_EmailGrid/{GridSetting}
        public async Task<IHttpActionResult> Get([ModelBinder(typeof(APIGridModelBinder))]GridSetting gridSetting)
        {
            var data = await emailService.Selectvw_EmailGridsByGridSettingAsync(gridSetting);
            var pagedResult = new PagedResult<vw_EmailGrid>();
            pagedResult.Results = data;
            pagedResult.RowCount = gridSetting.Count;
            return Ok(pagedResult);
        }      
    }
}