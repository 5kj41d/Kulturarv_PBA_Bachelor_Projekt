using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Script
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await SearchAsync(); 
        }

        private async static Task SearchAsync(){
            string Host = "https://www.kulturarv.dk/"; 
            string Path = "fundogfortidsminder/Lokalitet/"; 
            int Search_Point = 1;
            int Max_Search_Point = 3000; //Should be changed.
            HttpClient client = new HttpClient();  
            while(true){
                if(Search_Point <= Max_Search_Point){
                    string uri = Host + Path + Search_Point.ToString();
                    HttpResponseMessage response = await client.GetAsync(uri);
                    string contentString = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(contentString);

                    Strip_Text_From_HTML_RegEx(contentString); 
                }   
                else {
                    Search_Point = 1; 
                }
            }
        }

        private static void Strip_Text_From_HTML_RegEx(string data){
            string RegEx = ""; 
            string result = ""; //List?

            //DO THE WORK HERE!

            Save_Result_To_File(result); 
        }

        //This should be changed to save directly to the database (Neo4J).
        private static void Save_Result_To_File(string document){
            string Save_Path = @"C:\Users\Magnus Carlsen\Desktop\Script";
            System.IO.File.WriteAllText(Save_Path, document);
        }   
    }
}
