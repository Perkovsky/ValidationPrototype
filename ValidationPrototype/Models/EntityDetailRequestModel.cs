using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ValidationPrototype.Models
{
	public class EntityDetailRequestModel
	{
		[Required]
		[FromRoute]
		public int Id { get; set; }

		public int UnitId { get; set; }
		public int BuildingId { get; set; }

		[Required]
		public DateTime From { get; set; }

		[Required]
		public DateTime To { get; set; }
	}
}
