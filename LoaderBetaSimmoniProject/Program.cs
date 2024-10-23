using System;
using System.Net;
using System.IO;
using System.Diagnostics;
using System.Linq;
using Microsoft.Win32;
using System.Collections.Generic;

namespace UsernameVerification
{
    class Program
    {
        static void Main(string[] args)
        {
            string rawPasteUri = "https://raw.githubusercontent.com/AsasisKido/Loadersimmoni/main/username.txt";
            string rawPasteContent = new WebClient().DownloadString(rawPasteUri);
            string[] lines = rawPasteContent.Split(Environment.NewLine.ToCharArray());
            Dictionary<string, string> users = new Dictionary<string, string>();
            foreach (string line in lines)
            {
                if (line.Contains("="))
                {
                    string[] parts = line.Split('=');
                    users[parts[0]] = parts[1];
                }
            }
            
            Console.WindowWidth = 60; 
            
            string rawPasteUrl = "https://pastebin.com/raw/3tpjWX72";
            string title = new WebClient().DownloadString(rawPasteUrl);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("┌─────────────────────────────────────────────┐");
            Console.WriteLine("│                                             │");
            Console.WriteLine("│                                             │");
            Console.WriteLine("│" + title.PadLeft((48 + title.Length) / 2).PadRight(45) + "│");
            Console.WriteLine("│                                             │");
            Console.WriteLine("│                                             │");
            Console.WriteLine("└─────────────────────────────────────────────┘");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Введите свой юзернейм: ");
            string username = Console.ReadLine();
            
            bool isRegistered = users.ContainsKey(username);
            
            if (isRegistered)
            {
                Console.Write("Введите свой пароль: ");
                string password = Console.ReadLine();
                
                if (users[username] == password)
                {
                    hwid hwidObject = new hwid();
                    
                    string machineGuid = hwidObject.GetMachineGuid();
                    Console.WriteLine("Пароль принят.");
                    Console.WriteLine("Ваш HWID: " + machineGuid);
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Неправильный пароль.");
                    Console.WriteLine("Press any key to continue...");
                    
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("Пользователь не существует.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            }
    }
}
    
    class hwid
    {
        public string GetMachineGuid()
        {
            string location = @"SOFTWARE\Microsoft\Cryptography";
            string name = "MachineGuid";
            
            using (RegistryKey localMachineX64View = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
            {
                using (RegistryKey rk = localMachineX64View.OpenSubKey(location))
                {
                    if (rk == null)
                        throw new KeyNotFoundException(string.Format("Key Not Found: {0}", location));

                    object machineGuid = rk.GetValue(name);
                    if (machineGuid == null)
                        throw new IndexOutOfRangeException(string.Format("Index Not Found: {0}", name));

                    return machineGuid.ToString();
                }
            }
        }
    }
    


