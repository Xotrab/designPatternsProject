using System;
using System.Collections.Generic;

namespace FileForgeDP
{
    public class OurMapper : IOurMapper
    {
        private Dictionary<Type, Dictionary<string, Func<object, object>>> mMappingRules;

        public OurMapper(Dictionary<Type, Dictionary<string, Func<object, object>>> mappingRules)
        {
            mMappingRules = mappingRules;
        }

        public T Map<T>(object objectToMap) where T : new()
        {
            var resultObject = new T();
            var resultObjectType = typeof(T);

            var objectProperties = objectToMap.GetType().GetProperties();

            foreach (var property in objectProperties)
            {
                var resultProperty = resultObjectType.GetProperty(property.Name);
                if (resultProperty == null)
                    continue;

                resultProperty.SetValue(resultObject, property.GetValue(objectToMap));
            }

            var resultMappingRules = mMappingRules.GetValueOrDefault(resultObjectType);
            if (resultMappingRules != null)
            {
                foreach (var rule in resultMappingRules)
                {
                    var resultProperty = resultObjectType.GetProperty(rule.Key);
                    if (resultProperty == null)
                        continue;

                    resultProperty.SetValue(resultObject, rule.Value.Invoke(objectToMap));
                }
            }

            return resultObject;
        }
    }
}
