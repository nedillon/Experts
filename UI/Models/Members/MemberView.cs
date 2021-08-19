using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI.Models.Members
{
    /// <summary>
    /// Model used by the View Member page
    /// </summary>
    public class MemberView
    {

        /// <summary>
        /// Member ID
        /// </summary>
        public long ID { get; set; }
        
        /// <summary>
        /// Member Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Original (long) website URL
        /// </summary>
        public string Website { get; set; }

        /// <summary>
        /// Shortened website URL
        /// </summary>
        public string ShortURL { get; set; }

        /// <summary>
        /// List of headings (areas of expertise) for the member
        /// </summary>
        public List<string> Headings { get; set; }

        /// <summary>
        /// List of friends of the member
        /// </summary>
        public List<MemberFriend> Friends { get; set; }

    }
}