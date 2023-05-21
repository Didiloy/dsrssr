using System;
using dsrssr.rss;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace dsrssr.controller;

public class ListAllFeeds : Window
{
    [UI] private ListBox listBox = null;
    private MainWindow mainWindow;
    public ListAllFeeds(MainWindow w) : this(new Builder("ListAllFeeds.glade"), w) { }

    private ListAllFeeds(Builder builder, MainWindow w) : base(builder.GetRawOwnedObject("ListAllFeeds"))
    {
        builder.Autoconnect(this);
        this.mainWindow = w;
        DeleteEvent += Window_DeleteEvent;
        Realized += OnRealized;
    }
    
    private void Window_DeleteEvent(object sender, DeleteEventArgs a)
    {
        mainWindow.refreshRssArticles();
        Destroy();
    }


    private void OnRealized(object sender, EventArgs e)
    {
        //remove all widget in listBox
        foreach (Widget widget in listBox.Children)
        {
            listBox.Remove(widget);
        }

        for (var index = 0; index < SubbedFeed.Instance.Feeds.Count; index++)
        {
            var fe = SubbedFeed.Instance.Feeds[index];
            SingleFeed sf = new SingleFeed(fe);
            listBox.Insert(sf, index);
        }
    }
}