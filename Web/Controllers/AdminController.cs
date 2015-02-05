﻿using System.Web.Mvc;
using Web.Controllers.Base;
using Web.Models.AdminModels;

namespace Web.Controllers
{
    public class AdminController : PokerBunchController
    {
        [Route("-/admin/sendemail")]
        public ActionResult SendEmail()
        {
            RequireAdmin();
            var result = UseCase.TestEmail.Execute();

            var model = new EmailModel(result);

            return View("Email", model);
        }

        [Route("-/admin/clearcache")]
        public ActionResult ClearCache()
        {
            RequireAdmin();
            var result = UseCase.ClearCache.Execute();

            var model = new ClearCacheModel(result);

            return View("ClearCache", model);
        }
    }
}
