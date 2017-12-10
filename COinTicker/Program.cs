using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Windows.Input;

namespace CoinTicker
{
    class Program
    {
        static string userInput;
        static double StartPrice;
        static double StartChangeLastHour;
        static double StartChangeLast24Hours;
        static double StartChangeLast7Days;

        /// <summary>
        /// IMPORTANT: MESSY CODE! CLEAN IT UP!
        /// Calculate the profit / loss starting at the base of when the application is started.
        /// Do the func task async while waiting for button to be pressed.
        /// Clean up the messy code
        /// Create a class for the functions
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            GatherStartInfo();
        }

        private static void GatherStartInfo()
        {
            Console.WriteLine("Please Enter The Coin You Would Like To Pull (ex: Bitcoin or Vertcoin): \n");
            userInput = Console.ReadLine().ToLower();
            try
            {
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create("https://api.coinmarketcap.com/v1/ticker/" + userInput);
                webReq.Method = "GET";

                HttpWebResponse webResp = (HttpWebResponse)webReq.GetResponse();


                string jString;
                using (Stream stream = webResp.GetResponseStream())
                {
                    StreamReader sr = new StreamReader(stream, Encoding.UTF8);
                    jString = sr.ReadToEnd();
                }

                List<CoinMarketCapIndex> CoinData = JsonConvert.DeserializeObject<List<CoinMarketCapIndex>>(jString);

                //Develop the lambda algorithm
                var firstHit = CoinData.First(x => x.name != "Coin");
                ///
                ///
                ///


                foreach (var coin in CoinData)
                {
                    StartPrice = Convert.ToDouble(coin.price_usd);
                    StartChangeLastHour = Convert.ToDouble(coin.percent_change_1h);
                    StartChangeLast24Hours = Convert.ToDouble(coin.percent_change_24h);
                    StartChangeLast7Days = Convert.ToDouble(coin.percent_change_7d);
                }
                Console.WriteLine("================ HERE ARE THE STARTING PRICES ================");
                Console.WriteLine("Starting Price: $" + StartPrice);
                Console.WriteLine("Starting Percent Last Hour: " + StartChangeLastHour + "%");
                Console.WriteLine("Starting Percent Last 24 Hours: " + StartChangeLast24Hours + "%");
                Console.WriteLine("Starting Percent Last 24 Hours: " + StartChangeLast7Days + "% \n");
                CoinDataRequest();
            }
            catch (WebException ex)
            {
                Console.Clear();
                if (ex.Message.Contains("404"))
                {
                    Console.WriteLine($"{userInput} not found! Please try again..\n");
                    GatherStartInfo();
                }
            }
        }

        private static void CoinDataRequest()
        {
            bool isTrue = true;
            try
            {
                while (isTrue)
                {
                    HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create("https://api.coinmarketcap.com/v1/ticker/" + userInput);
                    webReq.Method = "GET";

                    HttpWebResponse webResp = (HttpWebResponse)webReq.GetResponse();

                    Console.WriteLine(webResp.Server);
                    Console.WriteLine(webResp.StatusCode);

                    string jString;
                    using (Stream stream = webResp.GetResponseStream())
                    {
                        StreamReader sr = new StreamReader(stream, Encoding.UTF8);
                        jString = sr.ReadToEnd();
                    }
                    List<CoinMarketCapIndex> CoinData = JsonConvert.DeserializeObject<List<CoinMarketCapIndex>>(jString);
                    foreach (var coin in CoinData)
                    {
                        Console.WriteLine("ID: " + coin.id);
                        Console.WriteLine("Name: " + coin.name);

                        #region startPrice
                        switch (coin.price_usd)
                        {
                            case double LessThan when (LessThan < StartPrice):
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Current Price " + "$" + coin.price_usd);
                                Console.ForegroundColor = ConsoleColor.White;
                                break;

                            case double MoreThan when (MoreThan > StartPrice):
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("Current Price " + "$" + coin.price_usd);
                                Console.ForegroundColor = ConsoleColor.White;
                                break;
                            case double Equal when (Equal == StartPrice):
                                Console.WriteLine("Current Price " + "$" + coin.price_usd);
                                break;

                            default:
                                Console.WriteLine("How did you end up here?");
                                break;
                        }
                        #endregion

                        #region StartHour
                        switch (coin.percent_change_1h)
                        {
                            case double LessThan when (LessThan < StartChangeLastHour):
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Percant Change Last Hour " + coin.percent_change_1h + "%");
                                Console.ForegroundColor = ConsoleColor.White;
                                break;
                            case double MoreThan when (MoreThan > StartChangeLastHour):
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("Percant Change Last Hour " + coin.percent_change_1h + "%");
                                Console.ForegroundColor = ConsoleColor.White;
                                break;
                            case double Equal when (Equal == coin.percent_change_1h):
                                Console.WriteLine("Percant Change Last Hour " + coin.percent_change_1h + "%");
                                break;
                            default:
                                Console.WriteLine("How did you end up here?");
                                break;
                        }
                        #endregion

                        #region Start24Hours

                        switch (coin.percent_change_24h)
                        {
                            case double LessThan when (LessThan < StartChangeLast24Hours):
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Percant Change Last 24 Hours " + coin.percent_change_24h + "%");
                                Console.ForegroundColor = ConsoleColor.White;
                                break;
                            case double MoreThan when (MoreThan > StartChangeLast24Hours):
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("Percant Change Last 24 Hours " + coin.percent_change_24h + "%");
                                Console.ForegroundColor = ConsoleColor.White;
                                break;
                            case double Equals when (Equals == StartChangeLast24Hours):
                                Console.WriteLine("Percant Change Last 24 Hours " + coin.percent_change_24h + "%");
                                break;
                            default:
                                Console.WriteLine("How did you end up here?");
                                break;
                        }
                        #endregion

                        #region Start7Days



                        switch (coin.percent_change_7d)
                        {
                            case double LessThan when (LessThan < StartChangeLast7Days):
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Percant Change Last 24 Hours " + coin.percent_change_7d + "%");
                                Console.ForegroundColor = ConsoleColor.White;
                                break;
                            case double MoreThan when (MoreThan > StartChangeLast7Days):
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("Percant Change Last 24 Hours " + coin.percent_change_7d + "%");
                                Console.ForegroundColor = ConsoleColor.White;
                                break;
                            case double Equals when (Equals == StartChangeLast7Days):
                                Console.WriteLine("Percant Change Last 24 Hours " + coin.percent_change_7d + "%");
                                break;
                            default:
                                Console.WriteLine("How did you end up here?");
                                break;
                        }
                        #endregion
                        Console.WriteLine("Rank: " + coin.rank);
                        Console.WriteLine("Symbol: " + coin.symbol + "\n");
                    }
                    Thread.Sleep(10000);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.ReadLine();
            }


        }
    }
}