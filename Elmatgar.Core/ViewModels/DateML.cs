using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Elmatgar.Core.ViewModels
{
   public class DateML:ValidationAttribute
    {
  
        public override bool IsValid(object value)
        {
            DateTime dateTime;
            var isValed = DateTime.TryParseExact(Convert.ToString(value),
                "d,MMM,yyyy",
            CultureInfo.CurrentCulture,
            DateTimeStyles.None,
            out dateTime);

            return (isValed && dateTime >= DateTime.Now);
        }
    }
}
