using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Rally.Lib.BigData.MongoDB.Standard
{
    public class MongoDBOperableStandard
    {
        public static MongoDBOperableStandard NewInstance(string ConnectionString, string DatabaseName, string CollectionName)
        {
            return new MongoDBOperableStandard(ConnectionString, DatabaseName, CollectionName);
        }

        private MongoClient mongoClient;

        private string databaseName;

        private string collectionName;

        public MongoDBOperableStandard(string ConnectionString, string DatabaseName, string CollectionName)
        {
            var url = new MongoUrl(ConnectionString);

            this.databaseName = DatabaseName;
            this.collectionName = CollectionName;

            this.mongoClient = new MongoClient(url);
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <typeparam name="T">传入参数实体类型</typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public T InsertOne<T>(T Item)
        {
            IMongoDatabase database = this.mongoClient.GetDatabase(this.databaseName);
            CreateCollection(database);
            IMongoCollection<T> collection = database.GetCollection<T>(this.collectionName);
            collection.InsertOne(Item);

            return Item;
        }

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Items"></param>
        /// <returns></returns>
        public IEnumerable<T> InsertAll<T>(IEnumerable<T> Items)
        {
            IMongoDatabase database = this.mongoClient.GetDatabase(this.databaseName);
            CreateCollection(database);
            IMongoCollection<T> collection = database.GetCollection<T>(this.collectionName);
            collection.InsertMany(Items);

            return Items;
        }

        /// <summary>
        /// 逐条更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Filter"></param>
        /// <param name="Update"></param>
        /// <returns></returns>
        public UpdateResult UpdateOne<T>(BsonDocument Filter, BsonDocument Update)
        {
            IMongoDatabase database = this.mongoClient.GetDatabase(this.databaseName);
            CreateCollection(database);
            IMongoCollection<T> collection = database.GetCollection<T>(this.collectionName);
            var result = collection.UpdateOne(Filter, Update);

            return result;
        }

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="Filter"></param>
        /// <param name="Update"></param>
        /// <returns></returns>
        public UpdateResult UpdateAll<T>(BsonDocument Filter, BsonDocument Update)
        {
            IMongoDatabase database = this.mongoClient.GetDatabase(this.databaseName);
            CreateCollection(database);
            IMongoCollection<T> collection = database.GetCollection<T>(this.collectionName);
            var result = collection.UpdateMany(Filter, Update);

            return result;
        }

        /// <summary>
        ///  删除
        /// </summary>
        ///<param name="Filter"></param>
        /// <returns></returns>
        public DeleteResult Delete<T>(BsonDocument Filter)
        {
            IMongoDatabase database = this.mongoClient.GetDatabase(this.databaseName);
            CreateCollection(database);
            IMongoCollection<T> collection = database.GetCollection<T>(this.collectionName);

            return collection.DeleteOne(Filter);
        }


        /// <summary>
        /// 批量删除
        /// </summary>
        ///<param name="Filter"></param>
        /// <returns></returns>
        public DeleteResult DeleteAll<T>(BsonDocument Filter)
        {
            IMongoDatabase database = this.mongoClient.GetDatabase(this.databaseName);
            CreateCollection(database);
            IMongoCollection<T> collection = database.GetCollection<T>(this.collectionName);

            return collection.DeleteMany(Filter);
        }

        #region 查询

        /// <summary>
        /// 查询一条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        ///<param name="Filter"></param>
        /// <returns></returns>
        public T GetOne<T>(BsonDocument Filter)
        {
            IMongoDatabase database = this.mongoClient.GetDatabase(this.databaseName);
            CreateCollection(database);
            IMongoCollection<T> collection = database.GetCollection<T>(this.collectionName);

            return (T)collection.Find(Filter).FirstOrDefault();
        }

        /// <summary>
        /// 查询有效数据的条数
        /// </summary>
        /// <param name="Filter"></param>
        /// <returns></returns>
        public long GetCount<T>(BsonDocument Filter)
        {
            IMongoDatabase database = this.mongoClient.GetDatabase(this.databaseName);
            CreateCollection(database);
            IMongoCollection<T> collection = database.GetCollection<T>(this.collectionName);

            return collection.CountDocuments(Filter);
        }


        /// <summary>
        /// 查询全部数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        ///<param name="Filter"></param>
        /// <returns></returns>
        public List<T> GetAll<T>(BsonDocument Filter)
        {
            IMongoDatabase database = this.mongoClient.GetDatabase(this.databaseName);
            CreateCollection(database);
            IMongoCollection<T> collection = database.GetCollection<T>(this.collectionName);

            return collection.Find(Filter).ToList();
        }


        /// <summary>
        ///     分页查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Filter"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="SortKey"></param>
        /// <returns></returns>
        public List<T> GetAll<T>(BsonDocument Filter, int PageIndex, int PageSize, string SortKey)
        {
            List<T> result = null;

            IMongoDatabase database = this.mongoClient.GetDatabase(this.databaseName);
            CreateCollection(database);
            IMongoCollection<T> collection = database.GetCollection<T>(this.collectionName);

            using (var cusor = collection.Find(Filter).Skip(PageSize * (PageIndex + 1)).Limit(PageSize).ToCursor())
            {
                result = cusor.ToList();
            }

            return result;
        }

        #endregion

        /// <summary>
        /// 判断集合是否存在，如果不存在就就新建集合
        /// </summary>
        /// <param name="database"></param>
        private void CreateCollection(IMongoDatabase database)
        {
            var collection = database.GetCollection<BsonDocument>(this.collectionName);

            if (collection == null)
            {
                database.CreateCollection(this.collectionName);
            }
        }

        /// <summary>
        /// 判断集合是否存在，如果存在就删除集合
        /// </summary>
        public void RemoveCollection()
        {
            var database = this.mongoClient.GetDatabase(this.databaseName);
            var collection = database.GetCollection<BsonDocument>(this.collectionName);

            if (collection == null)
            {
                database.DropCollection(this.collectionName);
            }
        }
    }
}
