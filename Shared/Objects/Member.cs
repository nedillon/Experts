using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Objects
{
    /// <summary>
    /// A representation of a member.
    /// This class will be used throughout the system (front-end, back-end, and shared functionality) to pass information about a member
    /// </summary>
    public class Member
    {
        /// <summary>
        /// Member ID
        /// </summary>
        public long ID { get; set; }
        
        /// <summary>
        /// Member name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Original (long) URL of the member's website
        /// </summary>
        public string Website { get; set; }

        /// <summary>
        /// Shortened URL of the member's website
        /// </summary>
        public string ShortURL { get; set; }

        /// <summary>
        /// List of any headings (areas of expertise) parsed from the member's website)
        /// </summary>
        public List<string> Headings { get; set; }

        /// <summary>
        /// List of member IDs for any Friends of this member
        /// </summary>
        public List<long> Friends { get; set; }

        //TODO: Might want to go ahead and get Member instances for friends too?
    }
}
