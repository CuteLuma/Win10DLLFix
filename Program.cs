using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Security.Cryptography;
namespace Win10DLL{
    class Program{
        static public string DllURL = "https://raw.githubusercontent.com/CuteLuma/Win10DLLFix/master";
        static Dictionary<int, string> DllInfo = new Dictionary<int, string>(){
            {1,"47d2843bfa6fee105abfcc4625bee8fbbfa0755c"}, {2,"3f042363595b3c1aab3ed7a011ab1316955b1c70"}, {3,"0cd7e7383b1c0bfd90f9c5e6f291c001d1ea9bc6"}, {4,"58d95f0907140a4b9fdac1b6c0c901be6f7d5b94"},
            {5,"0eebf3b27a72f82f2ad02fcb1710787e3bbdc5b7"}, {6,"757b3714859ec7844a7b3d1ad45cd92ab7e84b49"}, {7,"1f9d45d797eaa063a24859ec2a5a97383b3ef932"}
        };
        static private void HeaderText(){
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(".-.   .-. _           ,-. .--.   .---. .-.   .-.     .---.  _       ");
            Console.WriteLine(": :.-.: ::_;        .'  :: ,. :  : .  :: :   : :     : .--':_;      ");
            Console.WriteLine(": :: :: :.-.,-.,-.   `: :: :: :  : :: :: :   : :     : `;  .-..-.,-.");
            Console.WriteLine(": `' `' ;: :: ,. :    : :: :; :  : :; :: :__ : :__   : :   : :`.  .'");
            Console.WriteLine(" `.,`.,' :_;:_;:_;    :_;`.__.'  :___.':___.':___.'  :_;   :_;:_,._;");
            Console.ResetColor();
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
        }
        static private bool WinCheck(){
            if(Environment.OSVersion.Platform == PlatformID.Win32NT && Environment.OSVersion.Version.Major >= 6){
                Console.WriteLine("{0} Windows 10 detectado...", (char)16);
                return true;
            }
            Console.WriteLine("{0} Este computador não está executando uma versão do windows 10!", (char)16);
            return false;
        }
        static private int GetDllDownload(){
            int dllID = 1;
            foreach(KeyValuePair<int, string> dllUnit in DllInfo){
                using(FileStream fs = new FileStream(@"d3d9.dll", FileMode.Open, FileAccess.Read))
                using(var cryptoProvider = new SHA1CryptoServiceProvider()){
                    string hash = BitConverter.ToString(cryptoProvider.ComputeHash(fs)).ToLower().Replace("-", string.Empty);
                    fs.Close();
                    if(hash == dllUnit.Value && dllUnit.Value < 7){
                        dllID = dllUnit.Key + 1; break;
                    }
                }
            }
            return dllID;
        }
        static private void DllDownloading(){
            if(WinCheck()){ 
                int DllDownload = GetDllDownload();
                Console.WriteLine("{0}d3d9.dll v{1} detectada, baixando d3d9.dll v{2}", (char)16, DllDownload - 1, DllDownload);
                using (var client = new WebClient()){
                    client.DownloadFile(new Uri(DllURL + "/Dll/" + DllDownload + ".dll"), "d3d9.dll");
                }
                Console.WriteLine("{0}d3d9.dll v{1} baixada com sucesso!", (char)16, DllDownload);
            }
            Console.WriteLine("{0} Pressione qualquer tecla para fechar esta janela...", (char)16);
        }
        static void Main(string[] args){
            HeaderText(); DllDownloading(); Console.ReadKey();
        }
    }
}
