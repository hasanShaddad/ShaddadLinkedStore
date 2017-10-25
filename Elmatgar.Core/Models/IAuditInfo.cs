using System;

namespace Elmatgar.Core.Models
{
    public interface IAuditInfo
    {
        DateTime? CreationDate { get; set; }

        DateTime? LastUpdateDate { get; set; }
        string CreatedBy { get; set; }

        string LastUpdatedBy { get; set; }
        Boolean ActiveFlag { get; set; }
    }
}
