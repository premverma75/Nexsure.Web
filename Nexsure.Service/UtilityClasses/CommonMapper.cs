using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Nexsure.Service.UtilityClasses
{
    public static class CommonMapper
    {
        /// <summary>
        /// Maps properties from source object to a new instance of target type.
        /// Supports nested objects and lists.
        /// </summary>
        public static TTarget Map<TSource, TTarget>(TSource source)
            where TTarget : new()
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            var target = new TTarget();
            MapProperties(source, target);
            return target;
        }

        /// <summary>
        /// Maps properties from source object to target object.
        /// </summary>
        public static void MapProperties(object source, object target)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (target == null) throw new ArgumentNullException(nameof(target));

            var sourceType = source.GetType();
            var targetType = target.GetType();

            foreach (var targetProp in targetType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (!targetProp.CanWrite) continue;

                var sourceProp = sourceType.GetProperty(targetProp.Name, BindingFlags.Public | BindingFlags.Instance);
                if (sourceProp == null || !sourceProp.CanRead) continue;

                var sourceValue = sourceProp.GetValue(source);

                if (sourceValue == null)
                {
                    targetProp.SetValue(target, null);
                    continue;
                }

                // Handle simple types and strings
                if (IsSimpleType(targetProp.PropertyType))
                {
                    targetProp.SetValue(target, sourceValue);
                }
                // Handle lists
                else if (typeof(IEnumerable).IsAssignableFrom(targetProp.PropertyType) && targetProp.PropertyType != typeof(string))
                {
                    var targetListType = targetProp.PropertyType.IsArray
                        ? targetProp.PropertyType.GetElementType()
                        : targetProp.PropertyType.GetGenericArguments().FirstOrDefault();

                    if (targetListType != null && sourceValue is IEnumerable sourceEnumerable)
                    {
                        var list = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(targetListType));
                        foreach (var item in sourceEnumerable)
                        {
                            if (IsSimpleType(targetListType))
                            {
                                list.Add(item);
                            }
                            else
                            {
                                var mappedItem = Activator.CreateInstance(targetListType);
                                MapProperties(item, mappedItem);
                                list.Add(mappedItem);
                            }
                        }
                        targetProp.SetValue(target, list);
                    }
                }
                // Handle complex types
                else
                {
                    var mappedObj = Activator.CreateInstance(targetProp.PropertyType);
                    MapProperties(sourceValue, mappedObj);
                    targetProp.SetValue(target, mappedObj);
                }
            }
        }

        private static bool IsSimpleType(Type type)
        {
            return type.IsPrimitive
                || type.IsEnum
                || type == typeof(string)
                || type == typeof(decimal)
                || type == typeof(DateTime)
                || type == typeof(Guid)
                || (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>) && IsSimpleType(type.GetGenericArguments()[0]));
        }
    }
}