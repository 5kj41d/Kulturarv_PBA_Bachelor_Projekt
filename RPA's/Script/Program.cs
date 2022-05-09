using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Neo4j.Driver;

namespace Script
{
    //https://wiki.archlinux.org/title/.NET
    //Segmentation Fault Problem Solution --> Use pacman to install .NET Core SDK and Runtime.

    //TODO: Should be able to restart or pause for a certain time. 
    //Should be able to add search parameter.
    //Run each search in its own thread.  

    public class Program
    {
        private static string KulturArv_Location;       
        private static IDriver _driver;
        private static string uri = ""; //Neo4J IP
        static async Task Main(string[] args)   //Kommune = 0, Password = 1
        {
            KulturArv_Location = args[0];
            string user = args[1];
            string password = args[2];
            _driver = GraphDatabase.Driver(uri, AuthTokens.Basic(user, password));
            
            while(true)
            {
                await SearchAsync();
                await Search_Europeana_Database(); 
            }
        }

        private async static Task SearchAsync(){
            string Host = "https://www.kulturarv.dk/"; 
            string Path = "fundogfortidsminder/Lokalitet/"; 
            int Search_Point = 1;
            int Max_Search_Point = 3000; //TODO: Should be changed.
            HttpClient client = new HttpClient();  
           
                if(Search_Point <= Max_Search_Point){
                    string uri = Host + Path + Search_Point.ToString();
                    HttpResponseMessage response = await client.GetAsync(uri);
                    string contentString = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(contentString);

                    await Strip_Text_From_HTML_RegEx_KulturarvAsync(contentString); 
                }   
                else {
                    Search_Point = 1; 
                }
        }

        private async static Task Search_Europeana_Database()
        {
            string Host = "";   //TODO: Mangler. EDM --> Undersøg. 
            string Path = ""; 
            int Search_Point = 1; 
            int Max_Search_Point = 3000; 
            HttpClient client = new HttpClient(); 
          
                if(Search_Point <= Max_Search_Point)
                {
                    string uri = Host + Path + ""; 
                    HttpResponseMessage response = await client.GetAsync(uri);
                    string contentString = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(contentString);

                    await Strip_Text_From_HTML_RegEx_KulturarvAsync(contentString);
                }
            

        }

        private static async Task Cleanup_And_Prepare_Europeana_Data_For_DatabaseAsync(string data)
        {
            //TODO: Recognize data. 
            //Validate data. --> Language, content and maybe other missing parameters. 
            string result = ""; 
            Save_Result_To_Neo4J(result); 
        }

        private static async Task Strip_Text_From_HTML_RegEx_KulturarvAsync(string data){
            string RegEx = "(<!-- Start free text for location -->)"; 
            string Result = "";
            Regex re = new Regex(RegEx, RegexOptions.None);

            //TODO: Check HTML document with regex. 

            Result = re.ToString();  
            Save_Result_To_Neo4J(Result); 
        }

        private static void Save_Result_To_Neo4J(string document)
        {

            Check_Database_Connection();
            using (var session = _driver.AsyncSession())
            {
                var return_message = session.WriteTransactionAsync(async x =>
                {
                    var result = await x.RunAsync("CREATE (a:Greeting) " +
                                        "SET a.message = $message " +
                                        "RETURN a.message + ', from node ' + id(a)",
                        new { document });
                    return result;
                });
                Console.WriteLine("Return message: " + return_message);
            }
        }

        private static void Check_Database_Connection()
        {
            bool connected = false; 
            while(!connected)
            {
                Console.WriteLine("Connection to " + uri + " could not be completed!\n Waiting two seconds and trying again...");
                System.Threading.Thread.Sleep(1000);
                //TODO: Check database connection to Neo4J.
            }
        }

        private static void Pause_Program()
        {
            //TODO: Pause program command. 
        }   

        private static void Start_Program()
        {
            //TODO: Start program command. 
        }
    }
}
