using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using dsrssr.rss;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace dsrssr.controller
{
    public class MainWindow : Window
    {
        [UI] private Button addNewFeedButton = null;
        [UI] private Button modifyFeedButton = null;
        [UI] private Button refreshButton = null;
        [UI] private ListBox listBox = null;
        [UI] private ProgressBar progressBar = null;
        [UI] private Label articlesNumberLabel = null;
        
        public MainWindow() : this(new Builder("MainWindow.glade")) { }

        private MainWindow(Builder builder) : base(builder.GetRawOwnedObject("MainWindow"))
        {
            builder.Autoconnect(this);
            DeleteEvent += Window_DeleteEvent;
            Realized += OnRealized;
            addNewFeedButton.Clicked += addNewFeed_Clicked;
            modifyFeedButton.Clicked += modifyFeed_Clicked;
            refreshButton.Clicked += refresh_Clicked_thread;
            
        }

        private void OnRealized(object sender, EventArgs e)
        {
            refresh_Clicked_thread(sender, e);
        }

        private void Window_DeleteEvent(object sender, DeleteEventArgs a)
        {
            Application.Quit();
        }

        private void addNewFeed_Clicked(object sender, EventArgs a)
        {
            var win = new AddRssFeed();
            win.Show();
        }
        
        private void modifyFeed_Clicked(object sender, EventArgs a)
        {
            var win = new ListAllFeeds();
            win.Show();
        }
        

        private async void refresh_Clicked_thread(object sender, EventArgs a)
        {
            progressBar.Fraction = 0;
            //remove all widget in listBox
            foreach (Widget widget in listBox.Children)
            {
                listBox.Remove(widget);
            }
            
            if (SubbedFeed.Instance.Feeds.Count(sf => sf.Actif) == 0)
            {
                ArticleCard ac = new ArticleCard();
                ac.SetArticleTitle("Vous n'avez aucun flux RSS pour le moment");
                ac.SetDescription("Ajoutez ou activez-en un !");
                ac.SetSourceName("");
                listBox.Insert(ac, 0);
                progressBar.Hide();
                return;
            }
            progressBar.Show();
            progressBar.Fraction = 0.05;
            RssParser rssParser = new RssParser();

            List<RssArticle> rssArticles = await rssParser.requestFeeds(progressBar);
            articlesNumberLabel.Text = rssArticles.Count + " articles";


            for (var index = 0; index < rssArticles.Count; index++)
            {
                var ra = rssArticles[index];
                ArticleCard ac = new ArticleCard();
                ac.SetArticleTitle(ra.GetTitle());
                ac.SetSourceName(ra.GetPublisher());
                ac.SetDescription(ra.GetDescription());
                ac.SetSeeArticleDetailAction(
                    (sender1, e) =>
                    {
                        Process.Start(new ProcessStartInfo
                        {
                            FileName = ra.GetLink(),
                            UseShellExecute = true
                        });
                    });
                ac.Hexpand = true;
                ac.Vexpand = true;
                listBox.Insert(ac, index);
            }
            progressBar.Fraction = 1.0;
            await Task.Delay(500);
            progressBar.Hide();
        }
    }
}
