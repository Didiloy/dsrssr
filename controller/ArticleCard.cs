using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace dsrssr.controller
{
    class ArticleCard : Box
    {
        [UI] private Label articleTitleLabel = null;
        [UI] private Label sourceNameLabel = null;
        [UI] private Label descriptionLabel = null;
        [UI] private Button seeArticleDetail = null;

        public ArticleCard() : this(new Builder("ArticleCard.glade"))
        {
        }

        private ArticleCard(Builder builder) : base(builder.GetRawOwnedObject("mainBox"))
        {
            builder.Autoconnect(this);
        }

        public void SetArticleTitle(string title)
        {
            articleTitleLabel.Text = title;
        }

        public void SetSourceName(string sourceName)
        {
            sourceNameLabel.Text = sourceName;
        }

        public void SetDescription(string description)
        {
            descriptionLabel.Text = description;
        }

        public void SetSeeArticleDetailAction(EventHandler action)
        {
            seeArticleDetail.Clicked += action;
        }
    }
}