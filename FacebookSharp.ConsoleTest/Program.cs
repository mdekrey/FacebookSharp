using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FacebookSharp.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("ApiKey: ");
            string apiKey = Console.ReadLine();
            string secret;
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
            string result = fbApi.Call("admin.getAppProperties", new Dictionary<string, FacebookSharp.Methods.FacebookArgumentBase>()
            {
                { "properties", Methods.FacebookArgumentBase.Create(new string[] { "application_name", "ip_list" }) },
            }, false);
        }
    }
}
