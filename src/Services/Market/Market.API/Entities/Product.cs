using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Market.API.Entities
{
    //This the product entity class that stores product collections into mongodb.
    public class Product
    {
        //Define the members of the product class. BsonId column will be generate from the mongodb as an 24 character ObjectId.
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        
        [BsonElement("Name")]
        public string Name { get; set; }
        
        [BsonElement("Category")]
        public string Category { get; set; }
        
        [BsonElement("Summary")]
        public string Summary { get; set; }
        
        [BsonElement("Description")]
        public string Description { get; set; }
        
        [BsonElement("ImageFile")]
        public string ImageFile { get; set; }
        
        [BsonElement("Price")]
        public decimal Price { get; set; }


    }
}
