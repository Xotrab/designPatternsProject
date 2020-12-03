namespace FileForgeDP
{
    public class OurMapper : IOurMapper
    {
        public OurMapper()
        {
        }

        public T Map<T>(object objectToMap) where T : new()
        {
            var resultObject = new T();
            var resultObjectType = typeof(T);

            var objectProperties = objectToMap.GetType().GetProperties();

            foreach (var property in objectProperties)
            {
                resultObjectType.GetProperty(property.Name).SetValue(resultObject, property.GetValue(objectToMap));
            }

            return resultObject;
        }
    }
}
