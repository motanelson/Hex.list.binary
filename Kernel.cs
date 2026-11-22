using Cosmos.HAL.BlockDevice.Registers;
using System;
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text;
using Sys = Cosmos.System;


public class Kernel : Sys.Kernel
{
    Sys.FileSystem.CosmosVFS fs;
    protected override void BeforeRun()
    {
        fs = new Sys.FileSystem.CosmosVFS();
        Sys.FileSystem.VFS.VFSManager.RegisterVFS(fs);
    }

    protected override void Run()
    {
        string[] s = new string[2];
        string ss = "";
        while (true)
        {
            var fs_type = fs.GetFileSystemType(@"0:\");
            Console.WriteLine("File System Type: " + fs_type);
            var directory_list = Directory.GetFiles(@"0:\");
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Clear();

            foreach (var file in directory_list)
            {
                Console.WriteLine(file);
            }
            var available_space = fs.GetAvailableFreeSpace(@"0:\");
            Console.WriteLine("Available Free Space: " + available_space);
            Console.WriteLine("give me the input file");
            s[0] = "0:\\" + Console.ReadLine();
            Console.WriteLine("give me the output file");
            s[1] = "0:\\" + Console.ReadLine();
            Main(s);
            ss=Console.ReadLine();


        }
    }


    public static void Main(string[] argv) 
    {
        string s = "";
        string[] ss;
        int i = 0;
        int counter = 0;
        
        if (argv.Length < 2) return;
        s = File.ReadAllText(argv[0]);
        s = s.Replace('\r', ' ');
        s = s.Replace('\n', ',');
        ss = s.Split(",");
        byte[] b = new byte[ss.Length];
        foreach (string sss in ss) 
        {
            try
            {
                i = Convert.ToInt32(sss.Trim() ,16);
                //Console.WriteLine(sss.Trim());

                b[counter]=((byte)(i & 255));

            }
            catch 
            {
                Console.WriteLine("error");
            }
            counter++;
        
        }
        File.WriteAllBytes(argv[1], b);
        Console.WriteLine("ok!");
    
    
    
    }






}

