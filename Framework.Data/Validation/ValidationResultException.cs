using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Framework.Data.Validation
{
    /// <summary>
    /// Exception thrown when a validation rule fails.  
    /// </summary>
    /// <remarks>
    /// End user can use this validation result to resolve the issues.
    /// </remarks>
    public class ValidationResultException : Exception
    {
        private readonly System.Collections.ObjectModel.ReadOnlyCollection<ValidationResult> _validationResults;
        
        public IEnumerable<ValidationResult> ValidationResults
        {
            get { return _validationResults; }
        }

        public ValidationResultException(string message, Exception innerException = null)
            : this(message, new ValidationResult[] { }, innerException)
        {
        }

        public ValidationResultException(ValidationResult validationResult, Exception innerException = null)
            : this(validationResult.ErrorMessage, validationResult, innerException)
        {
        }

        public ValidationResultException(string message, ValidationResult validationResult, Exception innerException = null)
            : this(
                message, validationResult == null ? new ValidationResult[] { } : new[] { validationResult },
                innerException)
        {
        }

        public ValidationResultException(string message, IEnumerable<ValidationResult> brokenRules,
            Exception innerException = null) : base(message, innerException)
        {
            _validationResults = brokenRules == null
                ? new System.Collections.ObjectModel.ReadOnlyCollection<ValidationResult>(new List<ValidationResult>())
                : new System.Collections.ObjectModel.ReadOnlyCollection<ValidationResult>(brokenRules.ToList());
        }
    }
}
