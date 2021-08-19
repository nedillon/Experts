using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Data.Database;
using Shared.Objects;
using static Shared.Enumerations;

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
                //Ideally there would be some sort of check to see if the record exists or not
                //But I don't think name is a good enough unique identifier for that
                //If we were storing email, I would constrain members to be unique on email


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
                    foreach (long friend in Friends)
                        CreateFriendship(m.ID, friend);
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

        #region Member Exists

        /// <summary>
        /// Checks to see if a member already exists in the databse
        /// </summary>
        /// <param name="ID">ID of the member to check for</param>
        /// <returns>True if found, False otherwise</returns>
        public static bool MemberExists(long ID)
        {
            return db.Members.Any(m => m.ID == ID);
        }

        #endregion

        #region Create Friendship

        /// <summary>
        /// Creates a friendship between the given members
        /// </summary>
        /// <param name="MemberA">ID of the first member</param>
        /// <param name="MemberB">ID of the second member</param>
        /// <returns>A CreateFriendshipResult indicating the success of the operation</returns>
        public static CreateFriendshipResult CreateFriendship(long MemberA, long MemberB)
        {
            //Can't be friends with yourself :(
            if (MemberA == MemberB)
                return CreateFriendshipResult.DuplicateMember;

            //Make sure the first member is found
            if (!MemberExists(MemberA))
                return CreateFriendshipResult.PrimaryMemberNotFound;

            //Make sure the second member is found
            if (!MemberExists(MemberB))
                return CreateFriendshipResult.SecondaryMemberNotFound;

            //Make sure the two aren't already friends
            if (FriendshipExists(MemberA, MemberB))
                return CreateFriendshipResult.FriendshipExists;

            try
            {
                //Create a new record for the database
                var f = db.Friendships.NewFriendshipsRow();

                //Store friendships with the lower ID as the primary and the higher as the secondary
                if(MemberA < MemberB)
                {
                    f.Primary = MemberA;
                    f.Secondary = MemberB;
                }
                else
                {
                    f.Primary = MemberB;
                    f.Secondary = MemberA;
                }

                //Add the record (locking to prevent two users adding at the same time)
                lock (db.Friendships)
                    db.Friendships.AddFriendshipsRow(f);

                return CreateFriendshipResult.Success;
            }
            catch(Exception ex)
            {
                //TODO: Log exception

                return CreateFriendshipResult.UnknownError;
            }

        }

        /// <summary>
        /// Determines whether or not a friendship exists between the two given members
        /// </summary>
        /// <param name="PrimaryFriend">The ID of the first member</param>
        /// <param name="SecondaryFriend">The ID of the second member</param>
        /// <returns>True if a friendship between the two members exists, False otherwise</returns>
        public static bool FriendshipExists(long PrimaryFriend, long SecondaryFriend)
        {
            //Need to check to see if the IDs are in either the primary or secondary field
            return db.Friendships.Any(f => (f.Primary == PrimaryFriend && f.Secondary == SecondaryFriend) ||
                                            (f.Primary == SecondaryFriend && f.Secondary == PrimaryFriend));
        }

        #endregion
        

        //TODO: Find linked experts (search)

    }
}
