using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Configuration;

public class Info
{
    public ObjectId _id { get; set; }
    public string RetweetNumber { get; set; }
    public string TweetText { get; set; }
    public string Establish { get; set; }
    //public void Connection()
    //{
    //    MongoServer server = MongoServer.Create(ConfigurationManager.AppSettings["connectionString"]);
    //    MongoDatabase myDB = server.GetDatabase("XceedDB");
    //    var collection = myDB.GetCollection<Info>("XceedCollection");
    //}
}
