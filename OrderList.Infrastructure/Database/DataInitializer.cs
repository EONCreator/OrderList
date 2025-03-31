using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace OrderList.Infrastructure.Database
{
    public class DataInitializer
    {
        private readonly IMongoDatabase _database;
        private static bool _isInitialized;

        public DataInitializer(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task InitializeAsync()
        {
            if (_isInitialized) return;

            var collection = _database.GetCollection<Order>("Orders");
            await collection.Indexes.CreateOneAsync(new CreateIndexModel<Order>(
                Builders<Order>.IndexKeys.Ascending(o => o.DateCreated)));

            if (await collection.CountDocumentsAsync(FilterDefinition<Order>.Empty) == 0)
            {
                var firstNames = new[] { "Adam Dobson", "John Smith", "Emma Brown", "Olivia Johnson", "Liam Williams" };
                var addresses = new[] { "Paris", "Moscow", "New York", "London", "Berlin" };
                var products = new[] { "Samsung Galaxy S11", "iPhone 16", "Apple Watch" };
                var deliveryMethods = new[] { "To the door", "Pick up from point" };

                var documents = new List<Order>();
                var random = new Random();

                DateTime lastDateCreated = new DateTime(2025, 3, 30, 23, 59, 59);

                for (int i = 0; i < 25; i++)
                {
                    DateTime dateCreated = lastDateCreated.AddSeconds(-random.Next(0, 86400));
                    DateTime deliveryDate = new DateTime(2025, 3, 31).AddDays(random.Next(0, 30));
                    
                    int hour = random.Next(0, 24);
                    int minute = random.Next(0, 60);
                    string deliveryTime = $"{hour:D2}:{minute:D2}";
                    
                    var document = new Order
                    {
                        FirstName = firstNames[random.Next(firstNames.Length)],
                        Address = addresses[random.Next(addresses.Length)],
                        Product = products[random.Next(products.Length)],
                        DeliveryDate = deliveryDate.ToString("yyyy-MM-dd"),
                        DeliveryTime = deliveryTime,
                        DeliveryMethod = deliveryMethods[random.Next(deliveryMethods.Length)],
                        SendNotification = random.Next(0, 2) == 1,
                        DateCreated = dateCreated.ToString("MM/dd/yyyy HH:mm:ss")
                    };
                    documents.Add(document);
                }

                await collection.InsertManyAsync(documents);
            }

            _isInitialized = true;
        }
    }

    public class Order
    {
        [BsonElement("firstName")]
        public string FirstName { get; set; }

        [BsonElement("address")]
        public string Address { get; set; }

        [BsonElement("product")]
        public string Product { get; set; }

        [BsonElement("deliveryDate")]
        public string DeliveryDate { get; set; } 

        [BsonElement("deliveryTime")]
        public string DeliveryTime { get; set; } 

        [BsonElement("deliveryMethod")]
        public string DeliveryMethod { get; set; }

        [BsonElement("sendNotification")]
        public bool SendNotification { get; set; }

        [BsonElement("dateCreated")]
        public string DateCreated { get; set; }
    }
}
