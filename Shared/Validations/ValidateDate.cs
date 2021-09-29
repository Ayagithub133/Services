using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Shared.Validations
{
    class ValidateDate : RequiredAttribute
    {
        public override bool IsValid(object value)
        {
            if (Convert.ToDateTime(value) > DateTime.Now || Convert.ToDateTime(value )== DateTime.Now)
            {
                return true;
            }
            return false;
        }
       
      

    }
}
