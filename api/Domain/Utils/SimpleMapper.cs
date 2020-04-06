using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MediatorExample.Domain.Utils
{
    public static class SimpleMapper
    {
        public static void PropertyMap<T, TU>(T source, TU destination)
            where T : class, new()
            where TU : class, new()
        {
            PropertyMap(source, destination, Array.Empty<string>());
        }

        public static void PropertyMap<T, TU>(T source, TU destination, params string[] propertiesToIgnore)
            where T : class, new()
            where TU : class, new()
        {
            List<PropertyInfo> sourceProperties = source.GetType().GetProperties().ToList();
            List<PropertyInfo> destinationProperties = destination.GetType().GetProperties().ToList();

            foreach (PropertyInfo sourceProperty in sourceProperties)
            {
                PropertyInfo destinationProperty = destinationProperties
                    .Where(item => !propertiesToIgnore.Contains(item.Name))
                    .FirstOrDefault(item => item.Name == sourceProperty.Name);

                if (destinationProperty != null && destinationProperty.GetSetMethod() != null)
                {
                    try
                    {
                        destinationProperty.SetValue(destination, sourceProperty.GetValue(source, null), null);
                    }
                    catch (ArgumentException)
                    {
                    }
                }
            }
        }
    }
}