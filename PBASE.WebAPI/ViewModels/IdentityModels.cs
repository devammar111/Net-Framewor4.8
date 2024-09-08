using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;
using System;

namespace PBASE.WebAPI.ViewModels
{
    public class ApplicationUser : IdentityUser<int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser, int> manager, string authenticationType)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailSignature { get; set; }
        public int? SignatureImageAttachmentId { get; set; }
        public int? UserGroupId { get; set; }
        public int? UserTypeId { get; set; }
        public int? UserAccessTypeId { get; set; }
        public bool? IsReadOnly { get; set; }
        public bool? IsDeleteDisabled { get; set; }
        public int? ProfileImageAttachmentId { get; set; }
        public int? CreatedUserId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Nullable<int> UpdatedUserId { get; set; }
        public Nullable<DateTime> UpdatedDate { get; set; }
        public void CopyTo(object target)
        {
            var targetProperties = target.GetType().GetProperties().Where(x => x.CanWrite);
            var sourceProperties = this.GetType().GetProperties().Where(x => x.CanRead);
            foreach (var prop in targetProperties)
            {
                var sourceProperty = sourceProperties.FirstOrDefault(x => x.Name == prop.Name);
                if (sourceProperty != null)
                {
                    prop.SetValue(target, sourceProperty.GetValue(this));
                }
            }
        }

        public void CopyFrom(object source)
        {
            var targetProperties = this.GetType().GetProperties().Where(x => x.CanWrite);
            var sourceProperties = source.GetType().GetProperties().Where(x => x.CanRead);
            foreach (var prop in sourceProperties)
            {
                var targetProperty = targetProperties.FirstOrDefault(x => x.Name == prop.Name);
                if (targetProperty != null)
                {
                    targetProperty.SetValue(this, prop.GetValue(source));
                }
            }
        }
    }

    public class CustomUserRole : IdentityUserRole<int> { }

    public class CustomUserClaim : IdentityUserClaim<int> { }

    public class CustomUserLogin : IdentityUserLogin<int> { }

    public class CustomRole : IdentityRole<int, CustomUserRole>
    {
        public CustomRole() { }
        public CustomRole(string name) { Name = name; }
    }

    public class CustomUserStore : UserStore<ApplicationUser, CustomRole, int,
        CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public CustomUserStore(ApplicationDbContext context)
            : base(context)
        {
        }
    }

    public class CustomRoleStore : RoleStore<CustomRole, int, CustomUserRole>
    {
        public CustomRoleStore(ApplicationDbContext context)
            : base(context)
        {
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public ApplicationDbContext()
            : base("AppContext")
        {
        }
        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}