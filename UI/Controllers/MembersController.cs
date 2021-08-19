using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.Models.Members;
using Data.Services;
using Shared.Objects;
using Shared.Operations;

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

        /// <summary>
        /// Attempts to add the new member when the user clicks the add button
        /// </summary>
        /// <param name="collection">Data from the page</param>
        /// <returns>
        /// If successful, The Views.Members.Index Page
        /// If unsuccessful, The Views.Members.Add Page with errors listed
        /// </returns>
        /// <remarks>POST: Members/Add</remarks>
        [HttpPost]
        public ActionResult Add(FormCollection collection)
        {
            MemberAdd model = null;

            try
            {
                //Get the data entered on the page
                model = new MemberAdd();
                UpdateModel<MemberAdd>(model);

                //Create the new member instance (parsing/shortening the URL)
                Member newMember = MemberOperations.CreateNewMember(model.Name, model.Website);

                //Try to add the member to the database
                if (MemberService.AddMember(newMember) < 0)
                {
                    //If unsuccessful, show an error
                    model.Error = true;
                    return View(model);
                }
                else
                {
                    //If successful, go back to the list page
                    return RedirectToAction("Index");
                }
            }
            catch(Exception ex)
            {
                //TODO: Log Error

                if (model == null)
                    model = new MemberAdd();

                model.Error = true;

                return View(model);
            }
            

        }

        //TODO: Add Friends

        //TODO: Search for Experts
      }
}