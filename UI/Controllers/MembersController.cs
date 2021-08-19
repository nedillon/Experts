using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.Models.Members;
using Data.Services;
using Shared.Objects;

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

        /// <summary>
        /// Gets the view displaying a single member
        /// </summary>
        /// <param name="id">The ID of the member to load</param>
        /// <returns>The Views.Members.View page</returns>
        /// <remarks>GET: Members/View/{id}</remarks>
        public ActionResult View(long id)
        {
            //Create a new model for the view
            MemberView model = new MemberView();

            //Get the member from the database
            Member m = MemberService.GetMember(id);

            //If the member was found, set values in the model according to the member
            if(m != null)
            {
                model.ID = m.ID;
                model.Name = m.Name;
                model.Website = m.Website;
                model.ShortURL = m.ShortURL;
                model.Headings = m.Headings;
                model.Friends = m.Friends
                    .Select(f => MemberService.GetMember(f))
                    .Select(f => new MemberFriend() { ID = f.ID, Name = f.Name })
                    .ToList();
            }

            return View(model);
        }

        /// <summary>
        /// Displays the blank Add member page
        /// </summary>
        /// <returns>The Views.Members.Add page</returns>
        /// <remarks>GET: Members/Add</remarks>
        public ActionResult Add()
        {
            return View(new MemberAdd());
        }

        //TODO: Add Member

        //TODO: Add Friends

        //TODO: Search for Experts
    }
}