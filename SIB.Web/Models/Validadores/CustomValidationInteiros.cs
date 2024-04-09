using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIB.Web.Models.Validadores
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class CustomValidationInteiros : System.ComponentModel.DataAnnotations.ValidationAttribute
    {        
        public override bool IsValid(object value)
        {
            if (value == null)
                return false;            

            int num;
            return int.TryParse(value.ToString(), out num);
        }        
    }
}