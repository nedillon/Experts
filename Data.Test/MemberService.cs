using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Data.Test
{
    /// <summary>
    /// Unit tests on the Data.MemberService functions
    /// </summary>
    [TestClass]
    public class MemberService
    {
        /// <summary>
        /// Tests the AddMember function
        /// </summary>
        [TestMethod]
        public void MemberService_AddMember()
        {
            //Get the count before
            int count = Data.Services.MemberService.GetAllMembers().Count;

            //Add a new member
            long ID = Data.Services.MemberService.AddMember("That Guy", "http://somereallyreallyreallylongdomain.com/ThatGuy", "http://Short.url/TG", new string[] { "Heading1", "Heading2" }, null);

            //Make sure an ID was returned
            Assert.IsTrue(ID >= 0, "Member not added");

            int newCount = Data.Services.MemberService.GetAllMembers().Count;

            //Make sure the count increased
            Assert.IsTrue(count + 1 == newCount, "Count not increased by one");

        }

        /// <summary>
        /// Tests the AddMember function
        /// </summary>
        [TestMethod]
        public void MemberService_FindLinkedExperts()
        {
            //Member doesn't exist
            var result = Data.Services.MemberService.FindLinkedExperts(999, "hello");

            Assert.IsTrue(!result.Any(), "Records should not have been found for user 999");

            //No members with query string
            result = Data.Services.MemberService.FindLinkedExperts(1, "nonsense");

            Assert.IsTrue(!result.Any(), "Records should not have been found for query \"nonsense\"");

            //Test path with 3 results (and potential cycle)
            result = Data.Services.MemberService.FindLinkedExperts(1, "hello");

            Assert.IsTrue(result.Count() == 3, "There should be 3 paths to Tom");
        }

        //TODO: The other service functions

    }
}
