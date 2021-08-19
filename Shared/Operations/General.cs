using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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

    }
}
