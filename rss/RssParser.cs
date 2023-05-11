using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CodeKoenig.SyndicationToolbox;
using dsrssr.common;

namespace dsrssr.rss;

public class RssParser
{
    public RssParser()
    {
        
    }

    public async Task<List<RssArticle>> requestFeed(string url, string name)
    {
        List<RssArticle> rssArticles = new List<RssArticle>();
        try
        {
            HttpClient client = new HttpClient();
            using HttpResponseMessage response = await client.GetAsync(url);
            Console.WriteLine("Code de réponse: " + response.StatusCode);
            string rssString = await response.Content.ReadAsStringAsync();
            FeedParser feedParser = FeedParser.Create(rssString);
            CodeKoenig.SyndicationToolbox.Feed feed = feedParser.Parse();
            foreach (FeedArticle feedArticle in feed.Articles)
            {
                rssArticles.Add(RssArticle.FromFeedArticle(feedArticle, name));
            }
        }
        catch (Exception e)
        {
            Logger.Instance.Log("RssParser : requestFeed => " +  e.ToString());
        }

        return rssArticles;
    }
  
}