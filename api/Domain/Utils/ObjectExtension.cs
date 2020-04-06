using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace MediatorExample.Domain.Utils
{
    public static class ObjectExtension
    {
        public static T Clone<T>(T source)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("The type must be serializable.", "source");
            }

            // Don't serialize a null object, simply return the default for that object
            if (Object.ReferenceEquals(source, null))
            {
                return default(T);
            }

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();

            using (stream)
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }

        public static T DeepClone<T>(this T obj) where T : class
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;

                return (T)formatter.Deserialize(ms);
            }
        }

        public static void InheritedPropertyMap<T, TU>(this T source, TU destination)
            where T : class, new()
            where TU : class, new()
        {
            List<PropertyInfo> sourceProperties = source.GetType().GetProperties().ToList();
            List<PropertyInfo> destinationProperties = destination.GetType().GetProperties().ToList();

            foreach (PropertyInfo sourceProperty in sourceProperties)
            {
                if (sourceProperty.Name != "Id")
                {
                    PropertyInfo destinationProperty = destinationProperties.Find(item => item.Name == sourceProperty.Name);

                    if (destinationProperty != null)
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

        public static T DeleteNestedProperties<T>(this T entity)
            where T : class, new()
        {
            Type entityType = typeof(T);
            List<PropertyInfo> properties = entityType.GetProperties()
                .Where(p => p.GetGetMethod().IsVirtual && !p.GetGetMethod().IsFinal).ToList();

            foreach (PropertyInfo property in properties)
            {
                if (!CheckIfVirtualPropertyShouldBeIgnored(property))
                    entityType.GetProperty(property.Name).SetValue(entity, null);
            }

            return entity;
        }

        private static bool CheckIfVirtualPropertyShouldBeIgnored(PropertyInfo propertyInfo)
        {
            return propertyInfo.GetCustomAttributes(typeof(IgnoredVirtual), true).Count() > 0;
        }

        public static string ConvertToJsonWithNestedProperties<T>(this T entity)
            where T : class, new()
        {
            return JsonConvert.SerializeObject(entity);
        }

        public static string ConvertToJsonWithoutNestedProperties<T>(this T entity)
            where T : class, new()
        {
            JObject jsonObject = new JObject();
            Type entityType = entity.GetType();
            foreach (PropertyInfo property in entityType.GetProperties().Where(p =>
                                     !(p.GetGetMethod().IsVirtual && !p.GetGetMethod().IsFinal)))
            {
                object propertyValue = property.GetValue(entity);
                jsonObject.Add(property.Name, propertyValue != null ? propertyValue.ToString() : String.Empty);
            }

            return JsonConvert.SerializeObject(jsonObject);
        }

        public static T2 ConvertTo<T1, T2>(this T1 source)
            where T1 : class, new()
            where T2 : class, new()
        {
            return JsonConvert.DeserializeObject<T2>(JsonConvert.SerializeObject(source));
        }

        public static void PropertyMap<T, TU>(this T destination, TU source)
            where T : class, new()
            where TU : class, new()
        {
            List<PropertyInfo> sourceProperties = source.GetType().GetProperties().ToList();
            List<PropertyInfo> destinationProperties = destination.GetType().GetProperties().ToList();

            foreach (PropertyInfo sourceProperty in sourceProperties)
            {
                PropertyInfo destinationProperty = destinationProperties.Find(item => item.Name == sourceProperty.Name);

                if (destinationProperty == null)
                    continue;

                try
                {
                    destinationProperty.SetValue(destination, sourceProperty.GetValue(source, null), null);
                }
                catch (ArgumentException)
                { }
            }
        }
    }
}