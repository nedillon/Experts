using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UI.Models.Members
{
    /// <summary>
    /// Model used by the Add Friends page
    /// </summary>
    public class AddFriends
    {

        /// <summary>
        /// Primary member ID
        /// </summary>
        [Required(ErrorMessage = "Primary friend is required", AllowEmptyStrings = false)]
        public long PrimaryID { get; set; }

        /// <summary>
        /// Secondary member ID
        /// </summary>
        [Required(ErrorMessage = "Secondary friend is required", AllowEmptyStrings = false)]
        public long SecondaryID { get; set; }

        /// <summary>
        /// Success message (if any)
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Error message (if any)
        /// </summary>
        public string ErrorMessage { get; set; }

    }
}