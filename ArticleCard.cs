using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace dsrssr
{
    class ArticleCard : Box
    {
        [UI] private Image image = null;
        [UI] private Label articleTitleLabel = null;
        [UI] private Label sourceNameLabel = null;
        [UI] private Label descriptionLabel = null;

        public ArticleCard() : this(new Builder("ArticleCard.glade"))
        {
        }

        private ArticleCard(Builder builder) : base(builder.GetRawOwnedObject("mainBox"))
        {
            builder.Autoconnect(this);
            // image.File = "/home/dylan/Téléchargements/fond_ecran/mer_petit_canal.jpeg";
        }

    }
}