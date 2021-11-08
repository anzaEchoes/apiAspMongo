using System;
using System.Collections.Generic;
using Catalog.Entitites;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Catalog.Repositories{

    public class MongoDbItemsRepository : IItemsRepository
    {

        private readonly IMongoCollection<Item> itemsCollection;

        private const string databaseName = "catalog";
        private const string collectionName = "items";

        private readonly FilterDefinitionBuilder<Item> filterBuilder = Builders<Item>.Filter; 
 

        public MongoDbItemsRepository(IMongoClient mongoClient){ 
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            itemsCollection = database.GetCollection<Item>(collectionName);


        }
        public void CreateItem(Item item)
        {
            itemsCollection.InsertOne(item);
        }



        public Item GetItem(Guid id)
        {
            var filter = filterBuilder.Eq(itemsCollection => itemsCollection.Id, id);
            return itemsCollection.Find(filter).SingleOrDefault();
        }

        public IEnumerable<Item> GetItems()
        {
            return itemsCollection.Find(new BsonDocument()).ToList();
        }

        public void UpdateItem(Item item)
        {
            var filter = filterBuilder.Eq(existingItem => existingItem.Id, item.Id);
            itemsCollection.ReplaceOne(filter, item);
        }

        public void DeleteItem(Guid id)
        {
            var filter = filterBuilder.Eq(item =>item.Id, id);
            itemsCollection.DeleteOne(filter);
        }


    }

}