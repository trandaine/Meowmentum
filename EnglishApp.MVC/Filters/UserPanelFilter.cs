using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EnglishApp.MVC.Filters
{
    public class UserPanelFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = context.Controller as Controller;
            var user = context.HttpContext.User;

            if (user.Identity.IsAuthenticated)
            {
                controller.ViewBag.UserName = user.Identity.Name;
            }
            else
            {
                controller.ViewBag.UserName = null;
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Không cần xử lý sau action
        }
    }
}
