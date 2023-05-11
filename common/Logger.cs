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
    private static readonly string CurrentPath = System.IO.Directory.GetCurrentDirectory();
    private static readonly string SavePath = CurrentPath + path.DirectorySeparatorChar + "data" + path.DirectorySeparatorChar + "logs.txt";
    private Logger()
    {
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