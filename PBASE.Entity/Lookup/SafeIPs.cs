using System.ComponentModel.DataAnnotations;
namespace PBASE.Entity
{
    public partial class SafeIPs
    {
        [Key]
        public int? SafeIPsId { get; set; }
        public string IPAddress { get; set; }
    }
}

