using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;


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

        //TODO: The other service functions

    }
}
