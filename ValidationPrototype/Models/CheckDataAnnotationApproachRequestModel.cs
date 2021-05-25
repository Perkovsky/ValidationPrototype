using System;
using System.ComponentModel.DataAnnotations;

namespace ValidationPrototype.Models
{
	public class CheckDataAnnotationApproachRequestModel
	{
		[Required]
		[StringLength(50, MinimumLength = 3)]
		public string Name { get; set; }

		[Required]
		[Range(1, 100)]
		public int Age { get; set; }

		public bool Status { get; set; }
	}
}
