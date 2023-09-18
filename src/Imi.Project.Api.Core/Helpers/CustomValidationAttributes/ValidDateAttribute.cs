using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imi.Project.Api.Core.Helpers.CustomValidationAttributes
{
    public class ValidDateAttribute : ValidationAttribute
    {
        private const int MinYear = 1900;
        public ValidDateAttribute()
        {

        }

        public string GetErrorMessage() => "Date can not be set in the future";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var date = (DateTime)value;

            if (DateTime.Compare(date, DateTime.Now) > 0) return new ValidationResult(GetErrorMessage());
            if (date.Year < MinYear) return new ValidationResult($"Date must be larger or equal than the year {MinYear}");
            else return ValidationResult.Success;
        }

    }
}
