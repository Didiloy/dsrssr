using System;
using System.Data;
using dsrssr.rss;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;


namespace dsrssr.controller;

public class AddRssFeed : Window
{
    [UI] private Button buttonSave = null;
    [UI] private Entry nameEntry = null;
    [UI] private Entry linkEntry = null;
    [UI] private Label errorLabel = null;
    private MainWindow mainWindow;
    
    public AddRssFeed(MainWindow w) : this(new Builder("AddRssFeed.glade"), w) { }

    private AddRssFeed(Builder builder, MainWindow w) : base(builder.GetRawOwnedObject("addRssFeed"))
    {
        mainWindow = w;
        builder.Autoconnect(this);
        DeleteEvent += Window_DeleteEvent;
        buttonSave.Clicked += buttonSave_Clicked;

    }
    
    private void Window_DeleteEvent(object sender, DeleteEventArgs a)
    {
        Destroy();
    }
    
    private void buttonSave_Clicked(object sender, EventArgs a)
    {
        if (nameEntry.Text == "" || linkEntry.Text == "")
        {
            errorLabel.Text = "Veuillez remplir tous les champs.";
            return;
        }
        Feed feed = new Feed(nameEntry.Text, linkEntry.Text, true);
        SubbedFeed.Instance.addNewFeed(feed);
        this.mainWindow.refreshRssArticles();
        Destroy();
    }
}