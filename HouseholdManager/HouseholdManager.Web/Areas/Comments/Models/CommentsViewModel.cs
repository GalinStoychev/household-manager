using System;
using System.Collections.Generic;

namespace HouseholdManager.Web.Areas.Comments.Models
{
    public class CommentsViewModel
    {
        public Guid ExpenseId { get; set; }

        public IList<CommentViewModel> Comments { get; set; }
    }
}