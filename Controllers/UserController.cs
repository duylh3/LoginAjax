using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ideal.ViewModels;
using ideal.Repository;
using ideal.Helper;

namespace ideal.Controllers
{
    public class UserController : Controller
    {
        private UserRepository _re = new UserRepository();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginView model)
        {
            var user = CheckLogin(model);
            if (user != null)
            {
                Session["Username"] = user.Username;
                Session["UserId"] = user.UserId;

            }
            return Redirect("~/QA/SendFeedback");
        }

        [HttpPost]
        public ActionResult UserLogin(string id, string password)
        {
            LoginView model = new LoginView();
            model.Username = id;
            model.Password = password;

            var user = CheckLogin(model);
            if (user != null)
            {
                Session["Username"] = user.Username;
                Session["UserId"] = user.UserId;

                return Json(new
                {
                    result = "OK"
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { result = "Error", message = "Login ID & Password is incorrect!" });
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Session.Abandon();

            return Redirect("~/QA/SendFeedback");
        }


        private UserView CheckLogin(LoginView model)
        {
            model.Password = ED5Helper.Encrypt(model.Password);

            return _re.Login(model);
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(string username, string password)
        {
            try
            {
                int CreatedBy = (int)Session["UserId"];
                string result = _re.Insert(username, password, CreatedBy);
                return View();

            }
            catch (Exception ex)
            {
                var Err = ex.InnerException;
                return null;
            }
        }
    }
}