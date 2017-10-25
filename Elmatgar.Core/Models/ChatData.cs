using System;

namespace Elmatgar.Core.Models
{
    public partial class ChatData
    {
        public int id { get; set; }
        public Nullable<System.DateTime> startcontime { get; set; }
        public Nullable<System.DateTime> endcontime { get; set; }
        public string username { get; set; }
        public int OrderId { get; set; }
        public string connid { get; set; }
    }
}
