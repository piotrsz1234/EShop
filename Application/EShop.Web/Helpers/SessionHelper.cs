using System.Web;
using EShop.Dtos.User.Dtos;

namespace EShop.Web.Helpers
{
    public static class SessionHelper
    {
        public static LoggedUserDto LoggedUser
        {
            get => (LoggedUserDto) HttpContext.Current.Session["LoggedUser"];
            set => HttpContext.Current.Session["LoggedUser"] = value;
        }

        // public static LoggedUserDto LoggedUser {
        //     get => new LoggedUserDto() {
        //         Id = 1,
        //         IsAdmin = true,
        //         UserName = "test1"
        //     };
        //     set => HttpContext.Current.Session["LoggedUser"] = value;
        // }
    }
}