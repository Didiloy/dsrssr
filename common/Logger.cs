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
    private static readonly string CurrentPath = Directory.GetCurrentDirectory();
    private static readonly string SavePath = CurrentPath + path.DirectorySeparatorChar + "data" + path.DirectorySeparatorChar + "logs.txt";
    private Logger()
    {
        //if the data directory does not exist, create it
        if (!Directory.Exists(CurrentPath + path.DirectorySeparatorChar + "data"))
        {
            try
            {
                Directory.CreateDirectory(CurrentPath + path.DirectorySeparatorChar + "data");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
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