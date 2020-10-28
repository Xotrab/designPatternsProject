using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileForgeDP.Database.Builders
{
    public class MongoUpdateQueryBuilder<T>
    {
        private List<UpdateDefinition<T>> mUpdates;
        public MongoUpdateQueryBuilder()
        {
            mUpdates = new List<UpdateDefinition<T>>();
        }
        public static MongoUpdateQueryBuilder<T> Builder()
        {
            return new MongoUpdateQueryBuilder<T>();
        }
        //Patryk proszę nie patrz tu, nie patrz bo oślepniesz. Konrad nas opętał.
        public MongoUpdateQueryBuilder<T> Add(string field, Object element)
        {
            mUpdates.Add(Builders<T>.Update.Set(field, element));
            return this;
        }
        public UpdateDefinition<T> Build()
        {
            return Builders<T>.Update.Combine(mUpdates);
        }
    }
}
