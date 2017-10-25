using Microsoft.AspNet.Identity.EntityFramework;

namespace Elmatgar.Core.Models
{
    /// <summary>
    /// create the ApplicationRole class to inherit from IdentityRole so we can add name to each role from the conestructure
    /// </summary>
    public class ApplicationRole:IdentityRole
    {
    

        public ApplicationRole()
        {
                
        }

        public ApplicationRole(string name):base (name)
        {
            
        }
    }
}