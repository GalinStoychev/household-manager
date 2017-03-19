using HouseholdManager.Common.Constants;
using HouseholdManager.Common.Contracts;
using HouseholdManager.Logic.Contracts;
using HouseholdManager.Web.Areas.Comments.Models;
using HouseholdManager.Web.Controllers;
using HouseholdManager.Web.WebHelpers.Contracts;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace HouseholdManager.Web.Areas.Comments.Controllers
{
    [Authorize]
    public class CommentsController : BaseController
    {
        private readonly ICommentService commentService;

        public CommentsController(IMapingService mappingService, IWebHelper webHelper, ICommentService commentService) 
            : base(mappingService, webHelper)
        {
            if (commentService == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionConstants.ArgumentCannotBeNull, "commentService"));
            }

            this.commentService = commentService;
        }

        [HttpGet]
        public ActionResult Index(Guid expenseId)
        {
            if (!this.webHelper.CheckIfAjaxCall(this.HttpContext))
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
            return PartialView("_CreateCommentPartial", new CommentViewModel() { ExpenceId = expenceId });
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