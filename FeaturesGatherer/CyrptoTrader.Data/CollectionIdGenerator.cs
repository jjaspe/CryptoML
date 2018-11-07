using MongoDB.Bson.Serialization;
using System;

namespace CyrptoTrader.Data
{
    public class CollectionIdGenerator : IIdGenerator
    {
        public object GenerateId(object container, object document)
        {
            return Guid.NewGuid().ToString();
        }

        public bool IsEmpty(object id)
        {
            return id == null || String.IsNullOrEmpty(id.ToString());
        }
    }
}
