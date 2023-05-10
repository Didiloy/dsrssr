using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace dsrssr
{
    class ArticleCard : Box
    {
        [UI] private Label articleTitleLabel = null;
        [UI] private Label sourceNameLabel = null;
        [UI] private Label descriptionLabel = null;

        public ArticleCard() : this(new Builder("ArticleCard.glade"))
        {
        }

        private ArticleCard(Builder builder) : base(builder.GetRawOwnedObject("mainBox"))
        {
            builder.Autoconnect(this);
        }

    }
}