using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ValidationPrototype.Models
{
	public class CheckValidatableObjectApproachRequestModel : IValidatableObject
	{
		public string Name { get; set; }
		public int Age { get; set; }
		public bool Status { get; set; }

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			// Name
			if (Name == null)
			{
				yield return new ValidationResult($"Required field: {nameof(Name)}.",
					new[] { nameof(Name) });
			}
			if (Name?.Length > 50 || Name?.Length < 3)
			{
				yield return new ValidationResult($"The length of {nameof(Name)} must be between 3 and 50 characters.",
					new[] { nameof(Name) });
			}

			// Age
			if (Age > 100 || Age < 1)
			{
				yield return new ValidationResult($"The {nameof(Age)} must be between 1 and 100 characters.",
					new[] { nameof(Age) });
			}
		}
	}
}
