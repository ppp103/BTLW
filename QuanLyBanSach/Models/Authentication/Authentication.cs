using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace QuanLyBanSach.Models.Authentication
{
    public class Authentication : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Session.GetString("TaiKhoan") == null)
            {
                context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                {
                    {"Area", "" },
                    { "Controller", "Access" },
                    { "Action", "Login" }
                });
            }
        }

    }
}
