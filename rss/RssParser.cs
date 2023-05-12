using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CodeKoenig.SyndicationToolbox;
using dsrssr.common;
using dsrssr.controller;
using Gtk;

namespace dsrssr.rss;

public class RssParser
{
    public RssParser()
    {
    }

    private async Task<List<RssArticle>> requestFeed(string url, string name)
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
            Logger.Instance.Log("RssParser : requestFeed => " + e.ToString());
        }

        return rssArticles;
    }

    public async Task<List<RssArticle>> requestFeeds(ProgressBar pb)
    {
        List<RssArticle> rssArticles = new List<RssArticle>();
        var feedsList = SubbedFeed.Instance.Feeds;
        var count = feedsList.Count(feed => feed.Actif);
        foreach (var feed in feedsList.Where(feed => feed.Actif))
        {
            List<RssArticle> lr = await requestFeed(feed.Link, feed.Name);
            rssArticles.AddRange(lr);
            pb.Fraction += 1.0 / count;
        }

        //sort the list by date in inverted order
        rssArticles.Sort((x, y) => DateTime.Compare(y.PubDate, x.PubDate));

        // Console.WriteLine("RssParser : requestFeeds => " + rssArticles.Count + " articles récupérés");
        return rssArticles;
    }
}