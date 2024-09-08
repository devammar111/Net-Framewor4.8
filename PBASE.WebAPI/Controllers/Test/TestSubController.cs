using System;
using System.Linq;
using System.Web.Http;
using System.Threading.Tasks;
using Probase.GridHelper;
using PBASE.Entity;
using PBASE.Service;
using PBASE.WebAPI.ViewModels;
using PBASE.WebAPI.Helpers;
using PBASE.Entity.Helper;

namespace PBASE.WebAPI.Controllers
{
    [MenuPermission(Menus.Test)]
    public class TestSubController : BaseController
    {
        #region Initialization

        private readonly ILookupService lookupService;
        private readonly ITestService agreementService;

        public TestSubController(ILookupService lookupService, ITestService agreementService
        )
        {
            this.lookupService = lookupService;
            this.agreementService = agreementService;
        }

        #endregion Initialization

        // GET: api/Template/5
        [HttpGet]
        [Decrypt("id")]
        [Route("api/TestSub/{id}")]
        public async Task<IHttpActionResult> Get([FromUri]string id)
        {
            var model = new TestSubViewModel();
            var test_Types = await agreementService.SelectAllvw_LookupTestTypesAsync();
            model.vw_LookupTestType = id == "0" ? test_Types.Where(x => x.GroupBy == "ACTIVE") : test_Types;

            if (id != "0")
            {
                TestSub Test = await agreementService.SelectByTestSubIdAsync(Convert.ToInt32(id));
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
        [Route("api/TestSub")]
        public async Task<IHttpActionResult> Post(TestSubViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                TestSub agreement = new TestSub();
                ModelCopier.CopyModel(model, agreement);
                agreement.TestSubId = 0;
                agreement.IsArchived = false;
                if (model.EncTestId.IsNotNull())
                {
                    string testId = CryptoEngine.Decrypt(model.EncTestId.ToString());
                    agreement.TestId = Convert.ToInt32(testId);
                }
                int effectedRows = await agreementService.SaveTestSubFormAsync(agreement);
                if (effectedRows <= 0)
                {
                    return NotFound();
                }

                return Ok(agreement.TestSubId);
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
        [Route("api/TestSub/{id}")]
        [Decrypt("id")]
        public async Task<IHttpActionResult> Put([FromUri]string id, TestSubViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                TestSub agreement = await agreementService.SelectByTestSubIdAsync(Convert.ToInt32(id));
                CopyViewModelToEntity(model, agreement);

                int effectedRows = await agreementService.SaveTestSubFormAsync(agreement);
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
        [Route("api/TestSub/{id}")]
        [Decrypt("id")]
        public async Task<IHttpActionResult> Delete([FromUri]string id)
        {
            TestSub agreement = await agreementService.SelectByTestSubIdAsync(Convert.ToInt32(id));
            if (agreement == null)
            {
                return NotFound();
            }
            int effectedRows = await agreementService.DeleteTestSubFormAsync(agreement.TestSubId.Value);
            if (effectedRows <= 0)
            {
                return NotFound();
            }
            return Ok(agreement.TestSubId.Value);
        }
    }
}