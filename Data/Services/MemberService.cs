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

        #region Get Member(s)

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

            //Load the instane from the data record
            return LoadMember(memberRecord);
        }

        /// <summary>
        /// Gets all members in the database
        /// </summary>
        /// <returns>A list of Member instances, one for each member in the database</returns>
        public static List<Member> GetAllMembers()
        {
            //Get all members from the database and load instances from each record
            return db.Members
                .Select(m => LoadMember(m))
                .ToList();
        }

        /// <summary>
        /// Creates a Member instance from the data in the given Member data record
        /// </summary>
        /// <param name="memberRecord">DataRow from the Members table</param>
        /// <returns>A new Member instance with properties set from the given data record</returns>
        private static Member LoadMember(ExpertData.MembersRow memberRecord)
        {
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

        #region Add Member

        /// <summary>
        /// Add the given member to the database
        /// </summary>
        /// <param name="m">The member to add</param>
        /// <returns>The new member ID, if successful, -1 if unsuccessful</returns>
        public static long AddMember(Member m)
        {
            return AddMember(m.Name, m.Website, m.ShortURL, m.Headings, m.Friends);
        }

        /// <summary>
        /// Add a member to the database with the given values
        /// </summary>
        /// <param name="Name">Name of the member</param>
        /// <param name="Website">Original (long) website URL</param>
        /// <param name="ShortURL">Shortened website URL</param>
        /// <param name="Headings">List of headings parsed (areas of expertise)</param>
        /// <param name="Friends">List of friend IDs</param>
        /// <returns>The new member ID, if successful, -1 if unsuccessful</returns>
        public static long AddMember(string Name, string Website, string ShortURL, IEnumerable<string> Headings, IEnumerable<long> Friends)
        {
            try
            {
                //Create a new record for the member
                var m = db.Members.NewMembersRow();

                //Set the member values
                m.Name = Name;
                m.WebSite = Website;
                m.ShortURL = ShortURL;

                //Lock the table to prevent more than one insert at the same time
                //(this would be transaction if using SQL, and the lock would extend until the end of the function)
                lock(db.Members)
                {
                    //Add the member to the database
                    db.Members.AddMembersRow(m);
                }

                //If the member has headings, add those records as well
                if(Headings != null)
                {
                    foreach(string heading in Headings)
                    {
                        var h = db.MemberHeadings.NewMemberHeadingsRow();
                        h.MemberID = m.ID;
                        h.Heading = heading;

                        lock(db.MemberHeadings)
                        {
                            db.MemberHeadings.AddMemberHeadingsRow(h);
                        }
                    }
                }

                //If the member has friends, add those records as well
                if(Friends != null)
                {
                    //TODO: Add the friend records. (reuse code to come below)
                }

                return m.ID;
            }
            catch(Exception ex)
            {
                //TODO: Log the exception

                //Note that an error occurred
                return -1;
            }
        }

        #endregion

        //TODO: Create Friendship

        //TODO: Find linked experts (search)

    }
}
