using HouseholdManager.Common.Constants;
using HouseholdManager.Data.Contracts;
using HouseholdManager.Logic.Contracts;
using HouseholdManager.Logic.Contracts.Factories;
using HouseholdManager.Models;
using System;
using System.Collections.Generic;

namespace HouseholdManager.Logic.Services
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<Comment> commentRepositoryEF;
        private readonly ICommentFactory commentFactory;

        public CommentService(IUnitOfWork unitOfWork, IRepository<Comment> commentRepositoryEF, ICommentFactory commentFactory)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionConstants.ArgumentCannotBeNull, "unitOfWork"));
            }

            if (commentRepositoryEF == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionConstants.ArgumentCannotBeNull, "commentRepositoryEF"));
            }

            if (commentFactory == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionConstants.ArgumentCannotBeNull, "expenseFactory"));
            }

            this.unitOfWork = unitOfWork;
            this.commentRepositoryEF = commentRepositoryEF;
            this.commentFactory = commentFactory;
        }

        public void AddComment(Guid expenseId, string userId, string content)
        {
            var comment = this.commentFactory.CreateComment(userId, content, DateTime.Now, expenseId);
            this.commentRepositoryEF.Add(comment);
            this.unitOfWork.Commit();
        }

        public IEnumerable<Comment> GetExpenseComments(Guid expenseId)
        {
            var comments = this.commentRepositoryEF.GetAll<Comment>(x => x.ExpenseId == expenseId, null, x => x.User);
            return comments;
        }
    }
}