using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace QuotesService.Class
{
    public interface IQuotesSignature
    {
        string GetQuote();
    }

    public class QuotesSignature : IQuotesSignature
    {
        public string applicationDir { get; set; }

        public string GetQuote()
        {
            string Quote = string.Empty;
            applicationDir = Environment.CurrentDirectory;
            try
            {

                var jsonQuotes = File.ReadAllText(applicationDir + @"\Quotes\quotes.json");

                List<string> quotes = JsonConvert.DeserializeObject<List<string>>(jsonQuotes);

                System.Random RandNum = new System.Random();
                var index = RandNum.Next(0, quotes.Count() - 1);

                return quotes[index];

            }
            catch (Exception e)
            {

                UnicodeEncoding uniencoding = new UnicodeEncoding();
                string filename = applicationDir + @"\log.txt";

                byte[] result = uniencoding.GetBytes(e.ToString());

                using (FileStream SourceStream = File.Open(filename, FileMode.OpenOrCreate))
                {
                    SourceStream.Seek(0, SeekOrigin.End);
                    SourceStream.Write(result, 0, result.Length);
                }

                Quote = string.Empty;
            }


            return Quote;

        }
     }
}
