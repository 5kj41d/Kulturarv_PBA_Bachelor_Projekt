using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Neo4j.Driver;
using System.Configuration;
using System.Collections.Specialized;
using Microsoft.Extensions.Configuration;

namespace Script
{
    #region Ressources
    //https://wiki.archlinux.org/title/.NET
    //Config file: https://www.c-sharpcorner.com/article/four-ways-to-read-configuration-setting-in-c-sharp/ 
    //https://docs.microsoft.com/en-us/dotnet/api/system.configuration.configurationmanager?view=dotnet-plat-ext-6.0 
    //Segmentation Fault Problem Solution --> Use pacman to install .NET Core SDK and Runtime and not Snap since it is prone to this error.
    //Regular Expressions Tutorial: https://www.youtube.com/watch?v=sa-TUpSx1JA&t=1s&ab_channel=CoreySchafer 
    //Nginx: https://www.nginx.com/learn/ 
    #endregion

    public class Program
    {
        private static IDriver _driver;
        private static string user; 
        private static string password; 
        private static IConfiguration _config; 
        static async Task Main(string[] args)   //username for Neo4J = args[0], Password for Neo4J = args[1]
        {
            if(args.Length == 0)
            {
                Console.WriteLine("Enter user and password");
                user = Console.ReadLine();
                password = Console.ReadLine(); 
            }
            else 
            {
            user = args[0]; 
            password = args[1];
            } 
            await InitAsync();
        }

        //TODO: FIX: Error reading app settings. 
        private static async Task InitAsync()
        {
            try
            {
                _config = new ConfigurationBuilder().AddXmlFile("App.xml", optional: false, reloadOnChange: true).Build();
            } 
            catch(ConfigurationException e)
            {
                Console.WriteLine("Could'nt load the xml file!");
                throw e; 
            }
            try{    
                var URL_searchSettings = ConfigurationManager.GetSection("SearchSettings") as NameValueCollection; 
                var connectionString = ConfigurationManager.GetSection("DatabaseSettings") as NameValueCollection;
                var regexs = ConfigurationManager.GetSection("RegularExpressions") as NameValueCollection;
                if(URL_searchSettings.Count == 0)
                {
                    Console.WriteLine("SearchSettings was empty!"); 
                }
                else 
                {
                    foreach(var key in URL_searchSettings.AllKeys)
                    {
                        //TODO: Get all the keys and place them in the right spot. 
                        Console.WriteLine(key + " = " + URL_searchSettings[key]);
                    }
                }
                if(connectionString.Count == 0)
                {
                    Console.WriteLine("DatabaseSettings was empty!");
                } 
                else 
                {
                    foreach(var key in connectionString.AllKeys)
                    {
                        //TODO: Get all the keys and place them in the right spot. 
                        Console.WriteLine(key + " = " + connectionString[key]);
                    }
                }
                if(regexs.Count == 0)
                {
                    Console.WriteLine("RegularExpressions was empty!");
                }
                else 
                {
                    foreach(var key in regexs.AllKeys)
                    {
                        //TODO: Get all the keys and place them in the right spot. 
                        Console.WriteLine(key + " = " + regexs[key]);
                    }
                }
            } catch(ConfigurationErrorsException)
            {
                Console.WriteLine("Error reading app settings..."); 
            }

            //TODO: Get the values and save them in these properties. 
            await Check_Database_Connection_Async(user, password, "", ""); 
            
            while(true)
            {
                await Search_Kulturarv_Async("");
                await Search_Europeana_Database(""); 
                //TODO: Break this loop if admin says so. 
            }
        }

        //TODO: Make a generic search method instead.
        private async static Task Search_Kulturarv_Async(string uri)
        { 
            HttpClient client = new HttpClient();  
            HttpResponseMessage response = await client.GetAsync(uri);
            string contentString = await response.Content.ReadAsStringAsync();
            Console.WriteLine(contentString);
            await Strip_Text_From_HTML_RegEx_KulturarvAsync(contentString);       
        }

        private async static Task Search_Europeana_Database(string uri)
        {
            HttpClient client = new HttpClient();  
            HttpResponseMessage response = await client.GetAsync(uri);
            string contentString = await response.Content.ReadAsStringAsync();
            Console.WriteLine(contentString);
            await Cleanup_And_Prepare_Europeana_Data_For_DatabaseAsync(contentString);    
        }

        private static async Task Cleanup_And_Prepare_Europeana_Data_For_DatabaseAsync(string data)
        {
            //TODO: Recognize data. 
            //Validate data. --> Language, content and maybe other missing parameters. 
            string result = ""; 
            await Log_Results_To_File(result);  //Demo.
            await Save_Result_To_Neo4J(result); 
        }

        private static async Task Strip_Text_From_HTML_RegEx_KulturarvAsync(string data){
            string pattern = "";  //--> Get from config file. 
            string result = "";
            string input = @"";
            RegexOptions options = RegexOptions.Multiline;
        
            foreach (Match m in Regex.Matches(input, pattern, options))
            {
                Console.WriteLine("'{0}' found at index {1}.", m.Value, m.Index);
                result += m.ToString();  
            }

            //TODO: Check HTML document with regex. 
            await Log_Results_To_File(result);  //Demo
            await Save_Result_To_Neo4J(result); 
        }

        private static async Task Save_Result_To_Neo4J(string document)
        {
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
                await session.CloseAsync(); 
            }
        }

        //TODO: Should be able to test more than one database. --> Check database type. 
        private static async Task Check_Database_Connection_Async(string username, string password, string URL, string port)
        {
            //Port is optional. 
            bool connected = false; 
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentException($"'{nameof(username)}' cannot be null or empty.", nameof(username));
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException($"'{nameof(password)}' cannot be null or empty.", nameof(password));
            }

            if (string.IsNullOrEmpty(URL))
            {
                throw new ArgumentException($"'{nameof(URL)}' cannot be null or empty.", nameof(URL));
            }
            _driver = GraphDatabase.Driver(URL, AuthTokens.Basic(user, password));
            var session = _driver.AsyncSession();
            var return_result = session.RunAsync("MATCH() " + "RETURN ''").As<string>();
            await session.CloseAsync(); 
            if(return_result != null)
            {
                Console.WriteLine("Connection to " + URL + " as " + user + " is successfull!");
                Console.WriteLine("Message revieved was: " + return_result); 
                connected = true; 
            }
            if(!connected)
            {
                Console.WriteLine("Connection to " + URL + " could not be completed!\n Waiting two seconds and trying again...");
                System.Threading.Thread.Sleep(2000);
                await Check_Database_Connection_Async(user, password, URL, port);
            }
        }

        //TODO: Make able to call this. 
        static void AddUpdateAppSettings(string key, string value)  
        {  
            try  
            {  
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);  
                var settings = configFile.AppSettings.Settings;  
                if (settings[key] == null)  
                {  
                    settings.Add(key, value);  
                }  
                else  
                {  
                    settings[key].Value = value;  
                }  
                configFile.Save(ConfigurationSaveMode.Modified);  
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);  //Change Appsettings to --> Se config fil. 
            }  
            catch (ConfigurationErrorsException)  
            {  
                Console.WriteLine("Error writing app settings");  
            }  
        }  

        //Demo purpose. 
        private static async Task Log_Results_To_File(string result_data)
        {
            //TODO: Log the matches of the regex to a file. 
            Console.WriteLine("Loading data to file: " + result_data);
        }
        
    }
}
