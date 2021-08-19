using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI.Models.Members
{
    /// <summary>
    /// Model used by the View Members page
    /// </summary>
    public class MemberList
    {
        /// <summary>
        /// List of all members to be shown
        /// </summary>
        public List<MemberListItem> Members { get; set; }

    }
}