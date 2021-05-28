using System;

namespace ValidationPrototype.Attributes
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public class InternalAuthorizationAttribute : Attribute
	{
	}
}
