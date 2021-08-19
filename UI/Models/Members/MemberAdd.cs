using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UI.Models.Members
{
    /// <summary>
    /// Model used by the Add Member page
    /// </summary>
    public class MemberAdd
    {

        /// <summary>
        /// Name entered for the new member
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} is required")]
        public string Name { get; set; }

        /// <summary>
        /// Website URL
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} is required")]
        public string Website { get; set; }

    }
}