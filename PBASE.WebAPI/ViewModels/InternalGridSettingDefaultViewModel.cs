using System.ComponentModel.DataAnnotations;
using PBASE.Entity;
using static PBASE.WebAPI.Helpers.CustomValidationAttribute;

namespace PBASE.WebAPI.Models
{
    public class InternalGridSettingDefaultViewModel
    {
        [Required]
        [checkValidString]
        public string StorageKey { get; set; }

        public InternalGridSettingDefault GetInternalGridSettingDefault(InternalGridSettingDefault _InternalGridSettingDefault)
        {
            InternalGridSettingDefault InternalGridSettingDefault = null;

            if (_InternalGridSettingDefault != null)
            {
                _InternalGridSettingDefault.FormMode = PBASE.Entity.Enum.FormMode.Edit;
                InternalGridSettingDefault = _InternalGridSettingDefault;
            }
            else
            {
                InternalGridSettingDefault = new InternalGridSettingDefault();
                InternalGridSettingDefault.FormMode = PBASE.Entity.Enum.FormMode.Create;
                InternalGridSettingDefault.StorageKey = StorageKey;
            }

            return InternalGridSettingDefault;
        }
    }
}