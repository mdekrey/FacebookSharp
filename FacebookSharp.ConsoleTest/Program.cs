using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace FacebookSharp.ConsoleTest
{
    class Program
    {
        static string apiKey;
        static string secret;
        static void Main(string[] args)
        {
            Console.Write("ApiKey: ");
            apiKey = Console.ReadLine();
            if (apiKey.Split(' ').Length > 0)
            {
                secret = apiKey.Split(' ')[1];
                apiKey = apiKey.Split(' ')[0];
            }
            else
            {
                Console.Write("Secret: ");
                secret = Console.ReadLine();
            }

            FacebookApi fbApi = new FacebookApi(apiKey, secret);
            fbApi = new FacebookApi("consoleEntry");


            System.Net.HttpStatusCode statusCode;
            
            // this call is weird because facebook double encodes the JSON
            var result = fbApi.Call("admin.getAppProperties", new Dictionary<string, FacebookSharp.Methods.JsonBase>()
            {
                { "properties", Methods.JsonBase.Create(new string[] { "application_name", "ip_list" }) },
            }, false, out statusCode);
            Console.WriteLine(result.ToJsonString());
            Console.WriteLine();

            try
            {
                var errorResult = fbApi.Call<StandardInfo[]>("users.getStandardInfo", new Dictionary<string, FacebookSharp.Methods.JsonBase>()
                {
                    { "uids", Methods.JsonBase.Create("44400541") },
                    { "fields", Methods.JsonBase.Create(new string[] { "last_name", "first_name" }) },
                }, false, out statusCode);
                DisplayStandardInfo(errorResult);
            }
            catch (Methods.Errors.InvalidCallException ex)
            {
                DisplayException(ex);
            }
            Console.WriteLine();

            var result2 = fbApi.Call("users.getStandardInfo", new Dictionary<string, FacebookSharp.Methods.JsonBase>()
            {
                { "uids", Methods.JsonBase.Create(44400541) },
                { "fields", Methods.JsonBase.Create(new string[] { "last_name", "first_name" }) },
            }, false, out statusCode);
            Console.WriteLine(result2.ToJsonString());
            Console.WriteLine();

            var result3 = fbApi.Call<StandardInfo[]>("users.getStandardInfo", new Dictionary<string, FacebookSharp.Methods.JsonBase>()
            {
                { "uids", Methods.JsonBase.Create(44400541) },
                { "fields", Methods.JsonBase.Create(new string[] { "last_name", "first_name" }) },
            }, false, out statusCode);
            DisplayStandardInfo(result3);
            Console.WriteLine();

            Console.ReadLine();
        }

        private static void DisplayException(FacebookSharp.Methods.Errors.InvalidCallException ex)
        {
            Console.WriteLine("({0}): {1}", ex.ErrorCode, ex.Message);
            Console.WriteLine(ex.ToString());
        }

        private static void DisplayStandardInfo(StandardInfo[] result)
        {
            Console.WriteLine("uid\tfirst_name\tlast_name");
            foreach (var si in result)
            {
                Console.WriteLine("{0}\t{1}\t{2}", si.UserId, si.FirstName, si.LastName);
            }
        }

        

        

        [DataContract]
        public class StandardInfo
        {
            [DataMember(Name = "first_name")]
            public string FirstName { get; set; }
            [DataMember(Name = "last_name")]
            public string LastName { get; set; }
            [DataMember(Name = "uid")]
            public Int64 UserId { get; set; }
        }

        public static void GetApiKeys(string name, out string apiKey, out string appSecret)
        {
            apiKey = Program.apiKey;
            appSecret = secret;
        }
    }
}
