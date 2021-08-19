using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI.Models.Members
{
    /// <summary>
    /// Model used by the View Member page to list friends of the member
    /// </summary>
    public class MemberFriend
    {

        /// <summary>
        /// Member ID of the friend
        /// </summary>
        public long ID { get; set; }

        /// <summary>
        /// Name of the friend
        /// </summary>
        public string Name { get; set; }

    }
}