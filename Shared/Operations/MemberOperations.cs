using Shared.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Operations;

namespace Shared.Operations
{
    /// <summary>
    /// Class that provides functions, pertaining to Members, for reuse throughout the solution
    /// </summary>
    public static class MemberOperations
    {

        /// <summary>
        /// Creates a neww Member instance, shortening and parsing the given URL
        /// </summary>
        /// <param name="Name">Name of the new member</param>
        /// <param name="WebsiteURL">URL of the member's website</param>
        /// <returns>A new member instance with properties set</returns>
        public static Member CreateNewMember(string Name, string WebsiteURL)
        {
            //Create the instance
            Member m = new Member()
            {
                Name = Name,
                Website = WebsiteURL
            };

            //Shorten the URL
            m.ShortURL = General.ShortenURL(WebsiteURL);

            //Parse out any headings on the page
            m.Headings = General.ParseHeaders(WebsiteURL).ToList();

            return m;
        }

    }
}
