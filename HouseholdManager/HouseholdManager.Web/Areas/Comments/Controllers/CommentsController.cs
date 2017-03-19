using HouseholdManager.Common.Constants;
using HouseholdManager.Common.Contracts;
using HouseholdManager.Logic.Contracts;
using HouseholdManager.Web.Areas.Comments.Models;
using HouseholdManager.Web.WebHelpers.Contracts;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace HouseholdManager.Web.Areas.Comments.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ICommentService commentService;
        private readonly IMapingService mappingService;
        private readonly IWebHelper webHelper;

        public CommentsController(ICommentService commentService, IMapingService mappingService, IWebHelper webHelper)
        {
            if (commentService == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionConstants.ArgumentCannotBeNull, "commentService"));
            }

            if (mappingService == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionConstants.ArgumentCannotBeNull, "mappingService"));
            }

            if (webHelper == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionConstants.ArgumentCannotBeNull, "webHelper"));
            }

            this.commentService = commentService;
            this.mappingService = mappingService;
            this.webHelper = webHelper;
        }

        [HttpGet]
        public ActionResult Index(Guid expenseId)
        {
            if (!this.Request.IsAjaxRequest())
            {
                return this.Redirect("/");
            }

            var comments = this.commentService.GetExpenseComments(expenseId);
            var model = new CommentsViewModel();
            model.ExpenseId = expenseId;
            model.Comments = new List<CommentViewModel>();
            foreach (var comment in comments)
            {
                model.Comments.Add(this.mappingService.Map<CommentViewModel>(comment));
            }

            return View(model);
        }

        [HttpGet]
        [ChildActionOnly]
        public ActionResult Create(Guid expenceId)
        {
            return View("_CreateCommentPartial", new CommentViewModel() { ExpenceId = expenceId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Exclude = "User, CreatedOnDate")] CommentViewModel model)
        {
            this.commentService.AddComment(model.ExpenceId, this.webHelper.GetUserId(), model.CommentContent);
          
            return RedirectToRoute("Household_single_expense", new { name = this.webHelper.GetHouseholdNameFromCookie(), id = model.ExpenceId });
        }
    }
}