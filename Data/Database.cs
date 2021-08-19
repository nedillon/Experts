using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    /// <summary>
    /// Class that will serve as the connection to the database (the data layer).
    /// 
    /// In a production environment, this would be a class that handles querying of an ADO SQL connection or an Entity Framework instance
    /// For testing, just using a hard-coded dataset
    /// </summary>
    class Database
    {

        static Database()
        {
            //When the application starts up, insert some dummy data
            CreateSomeTestData();
        }

        //Our "database". A DataSet that is created as a Singleton so that there is only ever one instance
        private static ExpertData _db = new ExpertData();

        /// <summary>
        /// The "database"
        /// </summary>
        public static ExpertData db { get { return _db; } }

        private static void CreateSomeTestData()
        {
            //Clear out any old data (just in case)
            db.Friendships.Clear();
            db.MemberHeadings.Clear();
            db.Members.Clear();

            //Add 4 members
            db.Members.AddMembersRow("Nick Dillon", "http://LongerURL/That/Has/More/Stuff", "http://blah.com/short");
            db.Members.AddMembersRow("Bob Bobson", "http://LongerURL/That/Has/Even/More/Stuff", "http://blah.com/a");
            db.Members.AddMembersRow("Jane Janedatter", "http://LongerURL/That/Has/Even/More/Stuff/Still", "http://blah.com/b");
            db.Members.AddMembersRow("Tom Thompson", "http://LongerURL/Somewhat", "http://blah.com/shortc");

            //Give each member some headings (areas of expertise)
            db.MemberHeadings.AddMemberHeadingsRow(db.Members[0], "One");
            db.MemberHeadings.AddMemberHeadingsRow(db.Members[0], "Two");
            db.MemberHeadings.AddMemberHeadingsRow(db.Members[0], "Three");

            db.MemberHeadings.AddMemberHeadingsRow(db.Members[1], "Some stuff");

            db.MemberHeadings.AddMemberHeadingsRow(db.Members[2], "Look at me");
            db.MemberHeadings.AddMemberHeadingsRow(db.Members[2], "I know that");

            db.MemberHeadings.AddMemberHeadingsRow(db.Members[3], "What now?");
            db.MemberHeadings.AddMemberHeadingsRow(db.Members[3], "Hello there");

            //Create some friendships
            db.Friendships.AddFriendshipsRow(db.Members[0], db.Members[1]);
            db.Friendships.AddFriendshipsRow(db.Members[0], db.Members[2]);
            db.Friendships.AddFriendshipsRow(db.Members[0], db.Members[3]);
            db.Friendships.AddFriendshipsRow(db.Members[1], db.Members[2]);
            db.Friendships.AddFriendshipsRow(db.Members[2], db.Members[3]);
        }
    }
}
