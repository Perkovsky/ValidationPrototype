using System;

namespace ValidationPrototype.Attributes
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public class CheckPermissionsAttribute : Attribute
	{
		public string Feature { get; private set; }
		public string Action { get; set; }

		public CheckPermissionsAttribute(string feature) => Feature = feature;
	}
}
