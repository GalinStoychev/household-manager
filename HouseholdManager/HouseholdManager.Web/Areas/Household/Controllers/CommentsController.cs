using HouseholdManager.Common.Constants;
using HouseholdManager.Common.Contracts;
using HouseholdManager.Logic.Contracts;
using HouseholdManager.Web.Areas.Household.Models;
using HouseholdManager.Web.WebHelpers.Contracts;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace HouseholdManager.Web.Areas.Household.Controllers
{
    [Authorize]
    public class CommentsController : Controller
    {
        private readonly IExpenseService expenseService;
        private readonly IMapingService mappingService;
        private readonly IWebHelper webHelper;

        public CommentsController(IExpenseService expenseService, IMapingService mappingService, IWebHelper webHelper)
        {
            if (expenseService == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionConstants.ArgumentCannotBeNull, "expenseService"));
            }

            if (mappingService == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionConstants.ArgumentCannotBeNull, "mappingService"));
            }

            if (webHelper == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionConstants.ArgumentCannotBeNull, "webHelper"));
            }

            this.expenseService = expenseService;
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

            var comments = this.expenseService.GetExpenseComments(expenseId);
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
        public ActionResult Create(CommentViewModel model)
        {
            // add comment to db
          
            return RedirectToRoute("Household_single_expense", new { name = this.webHelper.GetHouseholdNameFromCookie(), id = model.ExpenceId });
        }
    }
}