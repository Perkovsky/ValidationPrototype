using System;

namespace ValidationPrototype.Attributes
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public class AllowAccessWithoutCheckPermissionsAttribute : Attribute
	{
	}
}
