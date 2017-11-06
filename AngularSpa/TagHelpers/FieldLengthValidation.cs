using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AngularSpa.TagHelpers
{
    public static class FieldLengthValidation
    {
        public enum Limit
        {
            Min,
            Max
        };

        public static int? GetMinLength(this ModelMetadata model)
        {
            return GetMinOrMaxLengthValue(model, Limit.Min);
        }

        public static int? GetMaxLength(this ModelMetadata model)
        {
            return GetMinOrMaxLengthValue(model, Limit.Max);
        }

        public static int? GetMinOrMaxLengthValue(this ModelMetadata model, Limit limit)
        {
            IList<object> validationItems = ((DefaultModelMetadata)model).ValidationMetadata.ValidatorMetadata;
            if (!validationItems.Any())
                return null;

            bool hasStringValidationItems =
                validationItems.Any() &&
                validationItems.Any(a => (a as ValidationAttribute).GetType().ToString().Contains("StringLengthAttribute"));

            var attr = validationItems
                .DefaultIfEmpty(null)
                .FirstOrDefault(a => (a as ValidationAttribute).GetType().ToString().Contains("StringLengthAttribute"));

            if (attr == null)
                return null;

            StringLengthAttribute slAttr = (attr as StringLengthAttribute);

            if (limit == Limit.Min)
                return slAttr.MinimumLength;
            else
                return slAttr.MaximumLength;
        }
    }
}
