using System.Collections.Generic;

namespace ValidationPrototype.Models
{
	public class CustomValidationResult
	{
		public bool IsValid { get; set; }
		public IEnumerable<string> Errors { get; set; }
	}
}
