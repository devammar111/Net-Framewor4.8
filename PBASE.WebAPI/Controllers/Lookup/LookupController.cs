using PBASE.Entity;
using PBASE.Entity.Enum;
using PBASE.Service;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using PBASE.WebAPI.Controllers;
using PBASE.WebAPI.ViewModels;
using System.Web.Configuration;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Diagnostics;
using System.Reflection;
using PBASE.WebAPI.Helpers;

namespace PBASE.WebAPI.Controllers
{
    [MenuPermission(Menus.Lookups)]
    public class LookupController : BaseController
    {
        #region Initialization

        private readonly ILookupService lookupService;

        public LookupController(ILookupService lookupService
        )
        {
            this.lookupService = lookupService;
        }

        #endregion Initialization

        #region Lookup

        // GET: api/Lookup/5
        [HttpGet]
        [Decrypt("id")]
        [Decrypt("lookupTypeId")]
        [Route("api/Lookup/{id}")]
        public async Task<IHttpActionResult> Get([FromUri]string id, string lookupTypeId, string enclookupTypeId)
        {
            Lookup_LookupFormViewModel model = new Lookup_LookupFormViewModel();
            int declookupTypeId = Convert.ToInt32(lookupTypeId);
            LookupType lookupType = lookupService.SelectByLookupTypeId(declookupTypeId);
            if (lookupType != null)
            {
                if (lookupType.LookupView != null)
                {
                    model.LookupView = lookupType.LookupView;
                    model.LookupViewLabel = lookupType.LookupViewLabel;
                    model.allLookupView = base.LoadLookupExtraIntOptions(model.LookupView, lookupType.LookupViewIdField, lookupType.LookupViewDisplayField);
                    model.allLookupView = model.allLookupView.OrderBy(x => x.disabled).ThenBy(x => x.Text).ToList();
                }
                if (lookupType.LookupView2 != null)
                {
                    model.LookupView2 = lookupType.LookupView2;
                    model.LookupViewLabel2 = lookupType.LookupViewLabel2;
                    model.allLookupView2 = base.LoadLookupExtraIntOptions(model.LookupView2, lookupType.LookupViewIdField2, lookupType.LookupViewDisplayField2);
                    model.allLookupView2 = model.allLookupView2.OrderBy(x => x.disabled).ThenBy(x => x.Text).ToList();

                }
                if (lookupType.LookupView3 != null)
                {
                    model.LookupView3 = lookupType.LookupView3;
                    model.LookupViewLabel3 = lookupType.LookupViewLabel3;
                    model.allLookupView3 = base.LoadLookupExtraIntOptions(model.LookupView3, lookupType.LookupViewIdField3, lookupType.LookupViewDisplayField3);
                    model.allLookupView3 = model.allLookupView3.OrderBy(x => x.disabled).ThenBy(x => x.Text).ToList();
                }
                if (lookupType.LookupView4 != null)
                {
                    model.LookupView4 = lookupType.LookupView4;
                    model.LookupViewLabel4 = lookupType.LookupViewLabel4;
                    model.allLookupView4 = base.LoadLookupExtraIntOptions(model.LookupView4, lookupType.LookupViewIdField4, lookupType.LookupViewDisplayField4);
                    model.allLookupView4 = model.allLookupView4.OrderBy(x => x.disabled).ThenBy(x => x.Text).ToList();
                }
                model.EncryptedLookupTypeId = enclookupTypeId;
            }
            if (id != "0")
            {
                Lookup Lookup = await lookupService.SelectByLookupIdAsync(Convert.ToInt32(id));
                base.CopyEntityToViewModel(Lookup, model);
            }
            else
            {
                model.LookupId = 0;
                IEnumerable<Lookup> SortOrder = lookupService.SelectMany_Lookup(x => x.LookupTypeId == declookupTypeId && x.SortOrder != null).OrderByDescending(x => x.SortOrder);
                if (SortOrder.Count() == 0)
                {
                    model.SortOrder = 1;
                }
                else
                {
                    model.SortOrder = SortOrder.First().SortOrder + 1;
                }
            }
           
            return Ok(model);
        }

        // POST: api/Lookup
        [HttpPost]
        [RequestValidation]
        [ReadOnlyValidation(Menus.Lookups)]
        [Route("api/Lookup")]
        public async Task<IHttpActionResult> Post(Lookup_LookupFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Lookup Lookup = new Lookup();
            base.CopyViewModelToEntity(model, Lookup);
            int effectedRows = await lookupService.SaveLookupFormAsync(Lookup);
            if (effectedRows <= 0)
            {
                return NotFound();
            }
            return Ok(Lookup);

        }

        // PUT: api/Lookup/5
        [HttpPut]
        [RequestValidation]
        [ReadOnlyValidation(Menus.Lookups)]
        [Decrypt("id")]
        [Route("api/Lookup/{id}")]
        public async Task<IHttpActionResult> Put([FromUri]string id, Lookup_LookupFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Lookup Lookup = await lookupService.SelectByLookupIdAsync(Convert.ToInt32(id));
            base.CopyViewModelToEntity(model, Lookup);
            int effectedRows = await lookupService.SaveLookupFormAsync(Lookup);
            if (effectedRows <= 0)
            {
                return NotFound();
            }
            return Ok(Lookup);

        }

        // DELETE: api/Lookup/5
        [HttpDelete]
        [Decrypt("id")]
        [ReadOnlyValidation(Menus.Lookups)]
        [Route("api/Lookup/{id}")]
        public async Task<IHttpActionResult> Delete([FromUri]string id)
        {
            Lookup Lookup = await lookupService.SelectByLookupIdAsync(Convert.ToInt32(id));
            if (Lookup == null)
            {
                return NotFound();
            }

            int effectedRows = await lookupService.DeleteLookupFormAsync(Convert.ToInt32(id));
            if (effectedRows <= 0)
            {
                return NotFound();
            }
            return Ok(Lookup);
        }

        #endregion
    }
}
