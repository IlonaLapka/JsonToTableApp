using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Aplikacja1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string answer = "";
            do
            {
                Console.Write("Podaj sciezke do pliku: ");
                string path = Console.ReadLine();

                List<dynamic> data = new List<dynamic>();
                List<string> headings = new List<string>();
                
                try
                {
                    using StreamReader r = new StreamReader(path);
                    string json = r.ReadToEnd();
                    if ((IsValidJson(json)))
                    {
                        data = JsonConvert.DeserializeObject<List<dynamic>>(json);                      

                        MakeLine();
                        Console.Write("|");
                        foreach (var item in data[0])
                        {
                            Console.Write("  {0}  |", item.Path.ToUpper());
                            headings.Add(item.Path);
                        }
                        Console.WriteLine();
                        MakeLine();

                        foreach (var d in data)
                        {
                            Console.Write("|");
                            for (int i = 0; i < headings.Count; i++)
                            {
                                Console.Write("  {0}  |", d[headings[i]]);
                            }
                            Console.WriteLine();
                            MakeLine();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                
                Console.WriteLine("Czy wczytać kolejny plik? t/n");
                answer = Console.ReadLine();
            } while (answer == "t");
        }

        static bool IsValidJson(string strInput)
        {
            if (string.IsNullOrWhiteSpace(strInput)) { return false; }
            strInput = strInput.Trim();
            if ((strInput.StartsWith("{") && strInput.EndsWith("}")) || //For object
                (strInput.StartsWith("[") && strInput.EndsWith("]"))) //For array
            {
                try
                {
                    var obj = JToken.Parse(strInput);
                    foreach (var item in obj)
                    {
                        foreach (var value in item.Values())
                        {
                            if(value.Type == JTokenType.Array || value.Type == JTokenType.Object)
                            {
                                Console.WriteLine("Plik nie powinien zawierać zagnieżdżeń obiektów, " +
                                    "ani tablic w danej właściwości");
                                return false;
                            }
                        }
                        
                    }
                    return true;
                }
                catch (JsonReaderException jex)
                {
                    //Exception in parsing json
                    Console.WriteLine(jex.Message);
                    return false;
                }
                catch (Exception ex) //some other exception
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Nieprawidłowy format.");
                return false;
            }
        }

        static void MakeLine()
        {
            for (int i = 0; i < 100; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine();
        }
    }
}
