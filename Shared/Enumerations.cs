using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    /// <summary>
    /// Class that contains enumerations for reuse througout the solution
    /// </summary>
    public class Enumerations
    {

        /// <summary>
        /// Possible results of the CreateFriendship function call
        /// </summary>
        public enum CreateFriendshipResult
        {
            Success,
            DuplicateMember,
            PrimaryMemberNotFound,
            SecondaryMemberNotFound,
            FriendshipExists,
            UnknownError
        }

    }
}
