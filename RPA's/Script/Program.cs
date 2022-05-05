using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.RegularExpressions; 

namespace Script
{
    //https://wiki.archlinux.org/title/.NET
    //Segmentation Fault Problem Solution --> Use pacman to install .NET Core SDK and Runtime.

    //TODO: Should be able to restart or pause for a certain time. 
    //Should be able to add search parameter.
    //Run each search in its own thread.  

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

                    Strip_Text_From_HTML_RegEx_Kulturarv(contentString); 
                }   
                else {
                    Search_Point = 1; 
                }
            }
        }

        private async static Task Search_Europeana_Database()
        {
            string Host = ""; 
            string Path = ""; 
            int Search_Point = 1; 
            int Max_Search_Point = 3000; 
            HttpClient client = new HttpClient(); 
            while(true)
            {
                if(Search_Point <= Max_Search_Point)
                {
                    string uri = Host + Path + ""; 
                    HttpResponseMessage response = await client.GetAsync(uri);
                    string contentString = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(contentString);

                    Strip_Text_From_HTML_RegEx_Kulturarv(contentString);
                }
            }

        }
        private static void Cleanup_And_Prepare_Europeana_Data_For_Database(string data)
        {
            //TODO: Recognize data. 
            //Validate data. --> Language, content and maybe other missing parameters. 
            string result = ""; 
            Save_Result_To_File(result); 
        }

        private static void Strip_Text_From_HTML_RegEx_Kulturarv(string data){
            string RegEx = ""; 
            string result = ""; //List?

            Regex re = new Regex(RegEx, RegexOptions.IgnoreCase); 

            Save_Result_To_File(result); 
        }

        //This should be changed to save directly to the database (Neo4J).
        private static void Save_Result_To_File(string document){
            string Save_Path = @"C:\Users\Magnus Carlsen\Desktop\Script";
            System.IO.File.WriteAllText(Save_Path, document);
        }   
    }
}
