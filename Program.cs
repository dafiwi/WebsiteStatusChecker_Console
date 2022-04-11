using System.Net;

namespace WebsiteStatusChecker
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string? websiteInput = null;
            string? contentInput = null;
            HttpResponseMessage? response = null;
            string? content = null;

            Console.WriteLine("This program allows you to enter the name of a website to check it's status.");
            Console.WriteLine("Don't forget to include the http(s):// attachment at the beginning to avoid an error.");

            Console.WriteLine("\nPlease enter or paste the name of a website: ");

            try
            {
                websiteInput = Console.ReadLine();

                HttpClient httpClient = new HttpClient();
                response = await httpClient.GetAsync(websiteInput);
                content = await response.Content.ReadAsStringAsync();

                Console.WriteLine("\nWebsite: " + "\t" + websiteInput);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                Console.WriteLine("StatusCode: " + "\t" + response.StatusCode + "\t" + "ReasonPhrase: " + response.ReasonPhrase);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("RequestMessage: " + "\t" + response.RequestMessage);
                Console.WriteLine("HTTP Version: " + "\t" + response.Version);
                Console.WriteLine();

                if (response.Headers != null)
                {
                    Console.WriteLine("Headers: " + "\n" + response.Headers);
                }

                if (response.TrailingHeaders != null)
                {
                    Console.WriteLine("Trailing Headers: " + "\n" + response.TrailingHeaders);
                }
            } 
            catch (Exception exception)
            {
                ShowRuntimeExceptionError(exception); 
            }            

            Console.WriteLine();

            if (websiteInput.Length > 0)
            {
                Console.WriteLine("\nDo you want to see the content of this web site? y/n");

                try
                {
                    contentInput = Console.ReadLine();

                    if (contentInput == "y" && content != null)
                    {
                        Console.WriteLine("Content: \n\n");
                        Console.WriteLine(content);
                    }
                    else
                    {
                        Console.WriteLine("\nThere is no content available for this website right now.");
                    }
                }
                catch (Exception exception)
                {
                    ShowRuntimeExceptionError(exception);
                }
            }           

            static void ShowRuntimeExceptionError(Exception exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nError: ");
                Console.WriteLine(exception.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }

            Console.WriteLine("\nPlease press any key to quit this program.");
            Console.ReadKey();
            Environment.Exit(0);
        }                
    }
}