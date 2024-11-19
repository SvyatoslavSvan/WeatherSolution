using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace AuthorizationModule.Models
{
    public sealed class ApplicationUser : IdentityUser
    {
        private string _permission = null!;

        private const string DefaultPermission = "defaultPermission";

        public ApplicationUser(string? userName)
        {
            UserName = userName;
            Permission = DefaultPermission;
        }

        public ApplicationUser(string? userName, string permission)
        {
            UserName = userName;
            Permission = permission;
        }

        [MaxLength(32)]
        public string Permission
        {
            get => _permission;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _permission = DefaultPermission;
                }

                if (value.Length > 32)
                {
                    throw new ArgumentException("Permission length cannot be greater than 32 symbols",
                        nameof(Permission));
                }

                _permission = value;
            }
        }
    }
}
