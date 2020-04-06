using System;
namespace MediatorExample.Domain.Utils
{
    /// <summary>
    /// This parameter attribute should be applied in the properties that should be ignored when the entity repository deals with navigation properties (nested properties). 
    /// This should be applied if you don't want that the entity repository consider this virtual property as a navigation property.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class IgnoredVirtual : Attribute
    { }
}
