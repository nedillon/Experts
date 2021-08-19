using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI.Models.Members
{
    /// <summary>
    /// Model used by the View Members page. Represents a single Member in the list
    /// </summary>
    public class MemberListItem
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
        /// Shortened URL of the member's page
        /// </summary>
        public string ShortURL { get; set; }

        /// <summary>
        /// Number of friends the member has
        /// </summary>
        public int FriendCount { get; set; }

    }
}