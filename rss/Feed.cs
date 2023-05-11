namespace dsrssr.rss;

public class Feed
{
    private string name;
    private string link;
    private bool actif;
    public Feed(string name, string link, bool actif)
    {
        this.name = name;
        this.link = link;
        this.actif = actif;
    }

    public string Name
    {
        get => name;
        set => name = value;
    }

    public string Link
    {
        get => link;
        set => link = value;
    }

    public bool Actif
    {
        get => actif;
        set => actif = value;
    }
}