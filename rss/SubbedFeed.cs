using System;
using System.Collections.Generic;
using System.Xml.Linq;
using dsrssr.common;
using path = System.IO.Path;


namespace dsrssr.rss;

public sealed class SubbedFeed
{
    //implement the singleton pattern
    private static SubbedFeed instance = null;

    public static SubbedFeed Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new SubbedFeed();
            }

            return instance;
        }
    }

    private List<Feed> feeds = new List<Feed>();
    private static readonly string CurrentPath = System.IO.Directory.GetCurrentDirectory();

    private static readonly string SavePath =
        CurrentPath + path.DirectorySeparatorChar + "data" + path.DirectorySeparatorChar + "feeds.xml";

    public List<Feed> Feeds
    {
        get => feeds;
    }

    private SubbedFeed()
    {
        Load();
    }

    public void SerializeAndSave()
    {
        //Create a XElement foreach feed in the feeds list
        List<XElement> feedElements = new List<XElement>();
        foreach (Feed feed in feeds)
        {
            feedElements.Add(
                new XElement("Feed",
                    new XElement("Name", feed.Name),
                    new XElement("Link", feed.Link),
                    new XElement("Actif", feed.Actif)
                )
            );
        }

        //Create the XDocument with the XElement list
        XDocument objXDoc = new XDocument(
            new XComment("Subbed feeds"),
            new XElement("Feeds", feedElements)
        );


        objXDoc.Declaration = new XDeclaration("1.0", "utf-8", "true");
        try
        {
            objXDoc.Save(SavePath);
            Logger.Instance.Log("SubbedFeed : SerializeAndSave => Saved to " + SavePath);
        }
        catch (Exception e)
        {
            Logger.Instance.Log("SubbedFeed : SerializeAndSave => Impossible to save file: " + e.ToString());
        }
        
    }


    public void Load()
    {
        //if the savePath file doesn't exist, create it 
        if (!System.IO.File.Exists(SavePath))
        {
            Logger.Instance.Log("SubbedFeed : Load => feeds.xml doesn't exist, creating it");
            System.IO.File.Create(SavePath).Dispose();
            //add the standard xml declaration
            XDocument objXDoc = new XDocument(
                new XComment("Subbed feeds"),
                new XElement("Feeds")
            );
            objXDoc.Save(SavePath);
        }
        try
        {
            //Load the xml file with XDocument
            XDocument doc = XDocument.Load(SavePath);
            //Get the root element of the xml file
            XElement root = doc.Root;
            //Get all the feed elements
            IEnumerable<XElement> feeds = root.Elements("Feed");

            foreach (XElement feed in feeds)
            {
                string name = feed.Element("Name").Value;
                string link = feed.Element("Link").Value;
                bool actif = bool.Parse(feed.Element("Actif").Value);

                //Create a new feed object
                Feed newFeed = new Feed(name, link, actif);
                this.feeds.Add(newFeed);
            }
        }
        catch (Exception e)
        {
            Logger.Instance.Log("SubbedFeed : Load => " + e.ToString());
        }
    }

    public void addNewFeed(Feed feed)
    {
        feeds.Add(feed);
        SerializeAndSave();
    }
    
    public void removeFeed(Feed feed)
    {
        feeds.Remove(feed);
        SerializeAndSave();
    }
}