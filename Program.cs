using System;
using Gtk;
using dsrssr.controller;
using path = System.IO.Path;

namespace dsrssr
{
    class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application.Init();
            //get current path
            var currentPath = System.IO.Directory.GetCurrentDirectory();

            var app = new Application("org.dsrssr.dsrssr", GLib.ApplicationFlags.None);
            app.Register(GLib.Cancellable.Current);
            var cssPath = currentPath + path.DirectorySeparatorChar + "ui"
                          + path.DirectorySeparatorChar + "adw-gtk3"
                          + path.DirectorySeparatorChar + "gtk-3.0"
                          + path.DirectorySeparatorChar + "gtk.css";
            CssProvider cssProvider = new CssProvider();
            cssProvider.LoadFromPath(cssPath);
            
            var win = new MainWindow();
            StyleContext.AddProviderForScreen(Gdk.Screen.Default, cssProvider, 800); // couldn't find the equivalent to GTK_STYLE_PROVIDER_PRIORITY_USER so I set the priority to a random number
            
            app.AddWindow(win);
            win.Show();
            Application.Run();
        }
    }
}
