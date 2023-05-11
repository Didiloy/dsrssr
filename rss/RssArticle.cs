using System;
using CodeKoenig.SyndicationToolbox;

namespace dsrssr.rss;

public class RssArticle
{
    private string title;
    private string description;
    private string link;
    private string publisher;
    private DateTime pubDate;

    public DateTime PubDate
    {
        get => pubDate;
    }

    public RssArticle(string title, string description, string link, string publisher, DateTime pubDate)
    {
        this.title = title;
        this.description = description;
        this.link = link;
        this.publisher = publisher;
        this.pubDate = pubDate;
    }


    public static RssArticle FromFeedArticle(FeedArticle feedArticle, string name)
    {
        
        return new RssArticle(feedArticle.Title, feedArticle.Content, feedArticle.WebUri, name, feedArticle.Published);
    } 

    public string GetTitle()
    {
        return title;
    }
    
    public string GetDescription()
    {
        return description;
    }
    
    public string GetLink()
    {
        return link;
    }
    
    public string GetPublisher()
    {
        return publisher;
    }
}