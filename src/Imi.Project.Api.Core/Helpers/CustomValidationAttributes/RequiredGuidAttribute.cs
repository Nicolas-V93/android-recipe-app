using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imi.Project.Api.Core.Helpers.CustomValidationAttributes
{
    public class RequiredGuidAttribute : ValidationAttribute
    {
        public RequiredGuidAttribute() => ErrorMessage = "{0} is required.";

        public override bool IsValid(object value)
            => value != null && value is Guid && !Guid.Empty.Equals(value);
    }
}
