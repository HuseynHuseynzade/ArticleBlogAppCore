using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArticleBlogAppCore.Extensions
{
    public static class ControllerExtensions
    {
        public static void AddErrorsToModelState(this Controller controller,IEnumerable<IdentityError> identityErrors)
        {
            
                foreach (IdentityError item in identityErrors)
                {
                    controller.ModelState.AddModelError("", item.Description);
                }
            
        }
    }
}
