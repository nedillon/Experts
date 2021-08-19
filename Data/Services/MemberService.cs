using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Data.Database;
using Shared.Objects;

namespace Data.Services
{
    /// <summary>
    /// Class that handles all interactions with the database when it comes to members
    /// </summary>
    public static class MemberService
    {

        #region Get Member

        /// <summary>
        /// Gets a member instance for the member with the given ID
        /// </summary>
        /// <param name="ID">ID of the member to get</param>
        /// <returns>A new Member instance, if the ID was found, null otherwise</returns>
        public static Member GetMember(long ID)
        {
            //Look up the member
            var memberRecord = db.Members.Where(m => m.ID == ID).FirstOrDefault();

            //Not found, don't do anything
            if (memberRecord == null)
                return null;

            //Create the instance and set the basic values
            Member member = new Member()
            {
                ID = memberRecord.ID,
                Name = memberRecord.Name,
                Website = memberRecord.WebSite,
                ShortURL = memberRecord.ShortURL
            };

            //Get all headings for the member
            member.Headings = db.MemberHeadings
                .Where(mh => mh.MemberID == memberRecord.ID)
                .Select(mh => mh.Heading)
                .ToList();

            //Get the IDs of any friends the member has
            member.Friends = db.Friendships
                .Where(f => f.Primary == memberRecord.ID || f.Secondary == memberRecord.ID)
                .Select(f => f.Primary == memberRecord.ID ? f.Secondary : f.Primary)
                .ToList();

            return member;
        }

        #endregion


        //TODO: Get Members

        //TODO: Add Member

        //TODO: Create Friendship

        //TODO: Find linked experts (search)

    }
}
