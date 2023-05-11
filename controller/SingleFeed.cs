using System;
using dsrssr.rss;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace dsrssr.controller;

public class SingleFeed : Box
{
    [UI] private Label nameLabel = null;
    [UI] private Button deleteButton = null;
    [UI] private Switch switchButton = null;
    private Feed feed;

    public SingleFeed(Feed feed) : this(new Builder("SingleFeed.glade"))
    {
        this.feed = feed;
        Realized += OnRealized;
    }

    private SingleFeed(Builder builder) : base(builder.GetRawOwnedObject("mainBox"))
    {
        builder.Autoconnect(this);
    }

    private void OnRealized(object sender, EventArgs e)
    {
        Console.WriteLine(switchButton);
        nameLabel.Text = feed.Name;
        deleteButton.Clicked += SetDeleteAction;
        switchButton.Active = feed.Actif;
        switchButton.AddNotification("active", OnActiveNotify); // recommended in gtk docs
    }

    [GLib.ConnectBefore] // some signals only get to your callback if callback connected before default signal handler
    void OnActiveNotify(object o, GLib.NotifyArgs args)
    {
        SetStateChangedSwitch(o, args);
    }

    private void SetStateChangedSwitch(object sender, EventArgs e)
    {
        feed.Actif = switchButton.Active;
        SubbedFeed.Instance.SerializeAndSave();
    }

    private void SetDeleteAction(object sender, EventArgs e)
    {
        SubbedFeed.Instance.removeFeed(feed);
        Destroy();
    }
}