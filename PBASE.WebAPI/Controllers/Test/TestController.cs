using System;
using System.Linq;
using System.Web.Http;
using System.Threading.Tasks;
using Probase.GridHelper;
using PBASE.Entity;
using PBASE.Service;
using PBASE.WebAPI.ViewModels;
using PBASE.WebAPI.Helpers;

namespace PBASE.WebAPI.Controllers
{
    [MenuPermission(Menus.Test)]
    public class TestController : BaseController
    {
        #region Initialization

        private readonly ILookupService lookupService;
        private readonly ITestService agreementService;

        public TestController(ILookupService lookupService, ITestService agreementService
        )
        {
            this.lookupService = lookupService;
            this.agreementService = agreementService;
        }

        #endregion Initialization

        // GET: api/Template/5
        [HttpGet]
        [Decrypt("id")]
        [Route("api/Test/{id}")]
        public async Task<IHttpActionResult> Get([FromUri]string id)
        {
            var model = new TestViewModel();
            var test_Types = await agreementService.SelectAllvw_LookupTestTypesAsync();
            model.vw_LookupTestType = id == "0" ? test_Types.Where(x => x.GroupBy == "ACTIVE") : test_Types;

            if (id != "0")
            {
                Test Test = await agreementService.SelectByTestIdAsync(Convert.ToInt32(id));
                CopyEntityToViewModel(Test, model);
                if (Test == null)
                {
                    return NotFound();
                }
            }

            return Ok(model);
        }

        // POST: api/Template
        [HttpPost]
        [ReadOnlyValidation(Menus.Test)]
        [RequestValidation]
        [Route("api/Test")]
        public async Task<IHttpActionResult> Post(TestViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Test agreement = new Test();
                ModelCopier.CopyModel(model, agreement);
                agreement.TestId = 0;
                agreement.IsArchived = false;
                int effectedRows = await agreementService.SaveTestFormAsync(agreement);
                if (effectedRows <= 0)
                {
                    return NotFound();
                }

                return Ok(agreement.TestId);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        // PUT: api/Template/5 
        [HttpPut]
        [RequestValidation]
        [ReadOnlyValidation(Menus.Test)]
        [Route("api/Test/{id}")]
        [Decrypt("id")]
        public async Task<IHttpActionResult> Put([FromUri]string id, TestViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                Test agreement = await agreementService.SelectByTestIdAsync(Convert.ToInt32(id));
                CopyViewModelToEntity(model, agreement);

                int effectedRows = await agreementService.SaveTestFormAsync(agreement);
                if (effectedRows <= 0)
                {
                    return Conflict();
                }

                return Ok("Data has been updated Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        // DELETE: api/Template/5
        [HttpDelete]
        [ReadOnlyValidation(Menus.Test)]
        [Route("api/Test/{id}")]
        [Decrypt("id")]
        public async Task<IHttpActionResult> Delete([FromUri]string id)
        {
            Test agreement = await agreementService.SelectByTestIdAsync(Convert.ToInt32(id));
            if (agreement == null)
            {
                return NotFound();
            }
            int effectedRows = await agreementService.DeleteTestFormAsync(agreement.TestId.Value);
            if (effectedRows <= 0)
            {
                return NotFound();
            }
            return Ok(agreement.TestId.Value);
        }
    }
}