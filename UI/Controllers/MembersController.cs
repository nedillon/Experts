using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.Models.Members;
using Data.Services;

namespace UI.Controllers
{
    public class MembersController : Controller
    {

        /// <summary>
        /// Gets the view listing all membvers in the database
        /// </summary>
        /// <returns>The Views.Members.Index page</returns>
        /// <remarks>GET: Members</remarks>
        public ActionResult Index()
        {
            //Create the model used by the view
            MemberList model = new MemberList();

            //Load all members from the database and populate the models' list with them
            model.Members = MemberService.GetAllMembers()
                .Select(m => new MemberListItem() { ID = m.ID, Name = m.Name, ShortURL = m.ShortURL, FriendCount = m.Friends.Count() })
                .ToList();

            return View(model);
        }

        //TODO: View Member

        //TODO: Add Member

        //TODO: Add Friends

        //TODO: Search for Experts
    }
}