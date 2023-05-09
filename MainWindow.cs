using System;
using Cairo;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace dsrssr
{
    class MainWindow : Window
    {
        [UI] private Button addNewFeedButton = null;
        [UI] private Button modifyFeedButton = null;
        [UI] private Button refreshButton = null;
        [UI] private Box mainBox = null;
        [UI] private FlowBox _flowBox = null;
        
        public MainWindow() : this(new Builder("MainWindow.glade")) { }

        private MainWindow(Builder builder) : base(builder.GetRawOwnedObject("MainWindow"))
        {
            builder.Autoconnect(this);
            DeleteEvent += Window_DeleteEvent;
            Realized += OnRealized;
            addNewFeedButton.Clicked += addNewFeed_Clicked;
            modifyFeedButton.Clicked += modifyFeed_Clicked;
            refreshButton.Clicked += refresh_Clicked;
            
        }

        private void OnRealized(object sender, EventArgs e)
        {
            Console.WriteLine("La fenêtre est prête.");
            var articleCard = new ArticleCard();
            for (int i = 0; i < 10; i++)
            {
                _flowBox.Insert(new ArticleCard(),i);
                
            }
            Console.WriteLine("done");
        }

        private void Window_DeleteEvent(object sender, DeleteEventArgs a)
        {
            Application.Quit();
        }

        private void addNewFeed_Clicked(object sender, EventArgs a)
        {
            Console.WriteLine("Le bouton ajouter un feed a été cliqué.");
            
        }
        
        private void modifyFeed_Clicked(object sender, EventArgs a)
        {
            Console.WriteLine("Le bouton modifier un feed a été cliqué.");
        }
        
        private void refresh_Clicked(object sender, EventArgs a)
        {
            Console.WriteLine("Le bouton refresh  a été cliqué.");
        }
    }
}
