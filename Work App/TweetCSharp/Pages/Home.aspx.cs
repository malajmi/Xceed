using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using TweetSharp;

public partial class Pages_Home : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var service = new TwitterService
            ("d1kKwwczj1cVTsHuVs7UQw",
            "sqQ7vqv4ZyGlvrtIaoVCRA0SkLYzKk9Ng1ef4mw8"
            );
        service.AuthenticateWith
            (
            "170774066-sQDEnurFcBDvjti0QgpU4cD3KRpsE19akXoZqEql",
            "EDfyhcn7h6cgMcLM4DzqzuGye1zMNC4xjcVq27M8xiYKp"
            );

        ListTweetsOnHomeTimelineOptions option = new ListTweetsOnHomeTimelineOptions();
        option.Count = 100;
        var tweets = service.ListTweetsOnHomeTimeline(option);

        //  *** POST TWEET ***
        //SendTweetOptions SendTweet = new SendTweetOptions();
        //SendTweet.Status = " Tweet Here ......   API";

        foreach (var tweet in tweets)
        {
            Panel myFieldSet = new Panel();
            HtmlGenericControl myOrderedList = new HtmlGenericControl("ol style='list-style-type:none'");
            HtmlGenericControl listItem = new HtmlGenericControl("li");
            HtmlGenericControl listItem1 = new HtmlGenericControl("li");
            HtmlGenericControl listItem2 = new HtmlGenericControl("li");
            HtmlGenericControl listItem3 = new HtmlGenericControl("li");
            HtmlGenericControl listItem4 = new HtmlGenericControl("li");
            myOrderedList.Controls.Add(listItem);
            myOrderedList.Controls.Add(listItem1);
            myOrderedList.Controls.Add(listItem2);
            myOrderedList.Controls.Add(listItem3);
            myOrderedList.Controls.Add(listItem4);
            myFieldSet.Controls.Add(myOrderedList);
            listItem.InnerText = tweet.Text;
            listItem1.InnerText = tweet.User.Name;
            listItem2.InnerText = tweet.RetweetCount.ToString();
            listItem3.InnerText = tweet.User.ScreenName;
            listItem4.InnerText = tweet.User.CreatedDate.ToString();
            div1.Controls.Add(myFieldSet);

            List<Info> names = new List<Info>();
            MongoServer server = MongoServer.Create(ConfigurationManager.AppSettings["connectionString"]);
            MongoDatabase myDB = server.GetDatabase("XceedDB");
            var collection = myDB.GetCollection<Info>("XceedCollection");

            //*****  STORE Data To MongoDB  ******

            Info infoObject = new Info
            {
                RetweetNumber = listItem2.InnerText,
                TweetText = listItem.InnerText,
                Establish = listItem4.InnerText
            };
            collection.Insert(infoObject);

            //*****  Get Data form MongoDB and Get 10 User have Retweet ****
            // I use Bubble sort then take first 10 index in array
            MongoCollection<Info> collectio2;
            var query = Query<Info>.EQ(b => b.RetweetNumber, listItem2.InnerText);
            foreach (Info collectionItem in collection.FindAs<Info>(query))
            {
                bool flag = true;
                int temp;
                int[] MostRetweet;
                MostRetweet = new int[100];// 100 is number of user 
                for (int i = 1; (i <= MostRetweet.Length - 1) && flag; i++)
                {
                    flag = false;
                    for (int j = 0; j < (MostRetweet.Length - 1); j++)
                    {
                        if (MostRetweet[j + 1] > MostRetweet[j])
                        {
                            temp = MostRetweet[j];
                            MostRetweet[j] = MostRetweet[j + 1];
                            MostRetweet[j + 1] = temp;
                            flag = true;
                        }
                    }
                }
                for (int i = 0; i < 11; i++)
                {
                    //query= MostRetweet[i]; Here Proplem
                }
            }
        }
    }
}