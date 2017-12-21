using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using AutoMapper;
using ETohumProject.BLL.Services;
using ETohumProject.DAL.Models;
using ETohumProject.UI.Models;
using ETohumProject.UI.ViewModels;
using Hangfire;
using Postal;

namespace ETohumProject.UI.Controllers
{
    public class HomeController : Controller
    {
        private UserManager _um;

        public HomeController()
        {
            _um = new UserManager();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            var user = _um.Get(id);
            if (user == null)
                return HttpNotFound();
            return View(user);
        }

        public ActionResult New()
        {
            var viewModel = new UserFormViewModel
            {
                User = new User()
            };
            return View("UserForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(User user)
        {

            if (!ModelState.IsValid)
            {
                var viewModel = new UserFormViewModel()
                {
                    User = user
                };
                return View("UserForm", viewModel);
            }
            if (user.Id == 0)
            {
                _um.Add(user);
                //hangfire uygulamasını kullanarak maiileri queue'liyorum
                BackgroundJob.Enqueue(() => NotifyUser(user));
            }
            else
            {
                var userInDb = _um.Get(user.Id);

                var updatedUser = Mapper.Map(user, userInDb);
                _um.Update(user.Id, updatedUser);
                //hangfire uygulamasını kullanarak maiileri queue'liyorum
                BackgroundJob.Enqueue(() => NotifyUser(user));
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Edit(int id)
        {
            var user = _um.Get(id);
            if (user == null)
                return HttpNotFound();

            var viewModel = new UserFormViewModel()
            {
                User = user
            };
            return View("UserForm", viewModel);
        }

        //Açık kaynak kodlu "Postal" uygulamasını kullanarak emailleri oluşturuyor ve yolluyorum.
        public static void NotifyUser(User user)
        {
            var viewsPath = Path.GetFullPath(HostingEnvironment.MapPath(@"~/Views/Emails"));
            var engines = new ViewEngineCollection();
            engines.Add(new FileSystemRazorViewEngine(viewsPath));

            var emailService = new EmailService(engines);

            using (var db = new ProjectContext())
            {
                var email = new NotifyEmail
                {
                    To = user.Email
                };
                if (user.Id == 0)
                    email.Message = "Sayın" + user.FirstName + user.LastName + ", hesabınız başarıyla oluşturulmuştur.";
                else
                    email.Message = "Sayın" + user.FirstName + user.LastName + ", hesabınız başarıyla güncellenmiştir.";
                
                emailService.Send(email);
            }
        }

    }
}