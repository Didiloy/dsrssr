using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CodeKoenig.SyndicationToolbox;

namespace dsrssr.rss;

public class RssParser
{
    public RssParser()
    {
        
    }

    public async Task<List<RssArticle>> requestFeed(string url, string name)
    {
        HttpClient client = new HttpClient();
        using HttpResponseMessage response = await client.GetAsync(url);
        Console.WriteLine("Code de réponse: " + response.StatusCode);
        string rssString = await response.Content.ReadAsStringAsync();
        FeedParser feedParser = FeedParser.Create(rssString);
        Feed feed = feedParser.Parse();
        List<RssArticle> rssArticles = new List<RssArticle>();
        foreach (FeedArticle feedArticle in feed.Articles)
        {
            rssArticles.Add(RssArticle.FromFeedArticle(feedArticle, name));
        }

        return rssArticles;
    }
  
}