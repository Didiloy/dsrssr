using System;
using System.IO;
using path = System.IO.Path;

namespace dsrssr.common;

public class Logger
{
    //implement the singleton pattern
    private static Logger instance = null;
    public static Logger Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Logger();
            }
            return instance;
        }
    }
    //get the os of the user
    private static readonly string OS = Environment.OSVersion.Platform.ToString();
    private static readonly string LocalPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
    private static readonly string SavePath = LocalPath + 
                                              path.DirectorySeparatorChar + "dsrssr" + 
                                              path.DirectorySeparatorChar + "data" + 
                                              path.DirectorySeparatorChar + "logs.txt";
    
    
    private Logger()
    {
        //if savePath does not exist, create it
        if (!File.Exists(SavePath))
        {
            try
            {
                File.Create(SavePath);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
    
    public void Log(string message)
    {
        message = DateTime.Now + " : " + message;
        try
        {
            using (StreamWriter sw = File.AppendText(SavePath))
            {
                sw.WriteLine(message);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        
    }
    
}