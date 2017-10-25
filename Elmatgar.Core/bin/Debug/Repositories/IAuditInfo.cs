using System;

//here we well set the creation and update values                     
namespace Elmatgar.Core.Repositories
{
  public  interface IAuditInfo
    {
         DateTime? CreationDate { get; set; }

         DateTime? LastUpdateDate { get; set; }
        string CreatedBy { get; set; }

        string LastUpdatedBy { get; set; }
    }
}
