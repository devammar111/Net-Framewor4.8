using System.ComponentModel.DataAnnotations;
using PBASE.Entity;
using static PBASE.WebAPI.Helpers.CustomValidationAttribute;

namespace PBASE.WebAPI.Models
{
    public class InternalGridSettingViewModel
    {
        [Required]
        [checkValidString]
        public string StorageKey { get; set; }
        [checkValidString]
        public string StorageData { get; set; }
        [checkValidString]
        public string StateName { get; set; }
        public int? StateId { get; set; }
        public bool? IsDefault { get; set; }
        public bool? IsGlobal { get; set; }

        public InternalGridSetting GetInternalGridSetting(InternalGridSetting _internalGridSetting)
        {
            InternalGridSetting internalGridSetting = null;

            if (_internalGridSetting != null)
            {
                _internalGridSetting.FormMode = PBASE.Entity.Enum.FormMode.Edit;
                internalGridSetting = _internalGridSetting;
            }
            else
            {
                internalGridSetting = new InternalGridSetting();
                internalGridSetting.FormMode = PBASE.Entity.Enum.FormMode.Create;
                internalGridSetting.StorageKey = StorageKey;
            }
            internalGridSetting.StorageData = StorageData;
            if (!string.IsNullOrWhiteSpace(StateName))
            {
                internalGridSetting.StateName = StateName;
            }
            if (StateId.HasValue)
            {
                internalGridSetting.InternalGridSettingId = StateId.Value;
            }

            return internalGridSetting;
        }
    }
}