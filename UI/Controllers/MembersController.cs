using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.Models.Members;
using Data.Services;
using Shared.Objects;
using Shared.Operations;
using System.Text;

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
        /// Performs the Search while viewing a member
        /// </summary>
        /// <param name="id">ID of the member being searched</param>
        /// <param name="collection">Information from the form</param>
        /// <returns>The Views.Members.View Page with search results</returns>
        /// <remarks>POST: Member/View/5 (search)</remarks>
        [HttpPost]
        public ActionResult View(long id, FormCollection collection)
        {
            //Note: If I had more time, this wouldn't be a post. It would be a client-side ajax call.
            //      I was running low on time and this was easier

            MemberView model = null;

            try
            {
                model = new MemberView();

                //Gets the search query string
                UpdateModel<MemberView>(model);

                //Need to load the member again
                //(since I designed the page to view only initially, this wouldn't be necessary with an ajax call)
                Member m = MemberService.GetMember(id);
                if (m != null)
                {
                    model.ID = m.ID;
                    model.Name = m.Name;
                    model.Website = m.Website;
                    model.ShortURL = m.ShortURL;
                    model.Headings = m.Headings.ToList();
                    model.Friends = m.Friends
                        .Select(f => MemberService.GetMember(f))
                        .Select(f => new MemberFriend() { ID = f.ID, Name = f.Name })
                        .ToList();
                }

                var results = MemberService.FindLinkedExperts(id, model.Search);

                if(results != null && results.Any())
                {
                    //If experts were found, create a list for each chain found

                    StringBuilder sb = new StringBuilder();

                    sb.AppendLine("<ul>");

                    foreach(List<Member> result in results)
                    {

                        //For each chain, create a string showing the result e.g.:
                        //  Member -> Friend A -> Friend B (Expertise A, Expertise B)

                        sb.AppendLine("<li>");

                        sb.Append(String.Join(" -> ", result.Select(mem => mem.Name)));
                        sb.Append(" (");
                        sb.Append(String.Join(", ", result.Last().Headings));
                        sb.Append(")");
                        sb.AppendLine();

                        sb.AppendLine("</li>");
                    }

                    sb.AppendLine("</ul>");

                    model.SearchResults = sb.ToString();
                }
                else
                {
                    model.SearchResults = "<p>No experts found</p>";
                }

                return View(model);
            }
            catch(Exception ex)
            {
                //TODO: Log error

                if (model == null)
                    model = new MemberView();


                return View(model);
            }
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

        /// <summary>
        /// Displays the blank Add friend page
        /// </summary>
        /// <returns>The Views.Members.AddFriends page</returns>
        /// <remars>GET: Members/AddFriends</remars>
        public ActionResult AddFriends()
        {
            return View(new AddFriends());
        }

        [HttpPost]
        public ActionResult AddFriends(FormCollection collection)
        {
            AddFriends model = null;

            try
            {
                model = new AddFriends();

                //Get the data entered on the form
                UpdateModel<AddFriends>(model);

                //Try to create the friendship
                var result = MemberService.CreateFriendship(model.PrimaryID, model.SecondaryID);


                //TODO: These aren't very good response messages
                //If I had a little bit more time, I would decorate the enumeration values with Description attributes that give a more user-friendly message
                //I would then create a function that gets the description based on the attribute. The message would be set to that instead.

                if(result == Shared.Enumerations.CreateFriendshipResult.Success)
                {
                    ModelState.Clear();
                    model = new AddFriends();
                    model.Message = "Success";
                }
                else
                {
                    //Go back to the page and show the error message
                    model.ErrorMessage = result.ToString();
                }

                return View(model);
            }
            catch(Exception ex)
            {
                //TODO: Log error

                if (model == null)
                    model = new AddFriends();

                model.ErrorMessage = Shared.Enumerations.CreateFriendshipResult.UnknownError.ToString();

                return View(model);
            }
        }

    }
}