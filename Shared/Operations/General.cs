using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace Shared.Operations
{
    /// <summary>
    /// Class that provides general functions for reuse throughout the solution
    /// </summary>
    public static class General
    {

        /// <summary>
        /// Shortens the given URL into a TinyURL
        /// </summary>
        /// <param name="URL">The URL to shorten</param>
        /// <returns>The new (short) URL</returns>
        /// <remarks>
        /// Based this off of function found here:
        /// http://www.freshcodehub.com/Article/38/convert-url-to-shorten-url-or-tiny-url-in-aspnet-with-c
        /// </remarks>
        public static string ShortenURL(string URL)
        {
            try
            {
                //Add http, if not already there
                if (!URL.ToLower().StartsWith("http"))
                    URL = "http://" + URL;

                //Create the request to tinyurl.com
                var request = WebRequest.Create("http://tinyurl.com/api-create.php?url=" + URL);
                var res = request.GetResponse();

                string ShortURL;

                using (var reader = new StreamReader(res.GetResponseStream()))
                {
                    //Read the result
                    ShortURL = reader.ReadToEnd();
                }

                return ShortURL;
            }
            catch(Exception ex)
            {
                //TODO: Log the error

                //If something went wrong, just return the original URL
                return URL;
            }
        }

        /// <summary>
        /// Loads the given URL and parses it to find any h1, h2, or h3 elements
        /// </summary>
        /// <param name="URL">The URL to parse</param>
        /// <returns>A list of the contents of any h1, h2, or h3 elements found</returns>
        public static IEnumerable<string> ParseHeaders(string URL)
        {
            //Load the page
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(URL);

            //Find the header elements in the page
            var nodes = doc.DocumentNode.SelectNodes("//h1//h2//h3");

            //Return the contents of the nodes
            return nodes
                .Select(n => n.InnerHtml)
                .ToList();
        }

    }
}
