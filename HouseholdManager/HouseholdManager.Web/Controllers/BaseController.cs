using HouseholdManager.Common.Constants;
using HouseholdManager.Common.Contracts;
using HouseholdManager.Web.WebHelpers.Contracts;
using System;
using System.Web.Mvc;

namespace HouseholdManager.Web.Controllers
{
    public class BaseController : Controller
    {
        public readonly IMapingService mappingService;
        public readonly IWebHelper webHelper;

        public BaseController(IMapingService mappingService, IWebHelper webHelper)
        {
            if (mappingService == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionConstants.ArgumentCannotBeNull, "mappingService"));
            }

            if (webHelper == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionConstants.ArgumentCannotBeNull, "webHelper"));
            }

            this.mappingService = mappingService;
            this.webHelper = webHelper;
        }
    }
}