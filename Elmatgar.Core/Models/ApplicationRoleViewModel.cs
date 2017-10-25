using System.ComponentModel.DataAnnotations;

namespace Elmatgar.Core.Models
{
    public class ApplicationRoleViewModel
    {
        public string Id { get; set; }
        [Required(AllowEmptyStrings=false,ErrorMessage="you must enter name for role")]
        [StringLength(256,ErrorMessage = "the role name must be 256 characters or less")]
        [Display(Name="Role Name")]
        public string Name { get; set; }
    }
}