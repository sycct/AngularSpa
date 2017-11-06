using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AngularSpa.TagHelpers
{
    public static class RegularExpressionValidation
    {
        public static string RegexExpression(this ModelMetadata model)
        {
            string regex = string.Empty;
            var items = ((DefaultModelMetadata)model).ValidationMetadata.ValidatorMetadata;
            if (items.Any())
            {
                var regexExpression = items.DefaultIfEmpty(null).FirstOrDefault(a => (a as ValidationAttribute).GetType().ToString().Contains("RegularExpressionAttribute"));
                if (regexExpression != null)
                {
                    regex = (regexExpression as RegularExpressionAttribute).Pattern;
                }
            }


            return regex;
        }
    }
}
