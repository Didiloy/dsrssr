using System;
using CodeKoenig.SyndicationToolbox;

namespace dsrssr.rss;

public class RssArticle
{
    private string title;
    private string description;
    private string link;
    private string publisher;
    
    public RssArticle(string title, string description, string link, string publisher)
    {
        this.title = title;
        this.description = description;
        this.link = link;
        this.publisher = publisher;    
    }

    public static RssArticle FromFeedArticle(FeedArticle feedArticle, string name)
    {
        return new RssArticle(feedArticle.Title, feedArticle.Content, feedArticle.WebUri, name);
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