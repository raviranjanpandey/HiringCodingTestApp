using HiringCodingTestApis.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HiringCodingTestApis.Core.DTO
{
    public class ValidationErrorDto
    {
        public string PropertyName { get; set; }
        public string ErrorMessage { get; set; }
        public object AttemptedValue { get; set; }
        public ValidationSeverity Severity { get; set; }
    }
}