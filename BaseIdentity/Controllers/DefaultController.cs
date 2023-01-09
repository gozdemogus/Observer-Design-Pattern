using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseIdentity.PresentationLayer.DAL;
using BaseIdentity.PresentationLayer.Models;
using BaseIdentity.PresentationLayer.ObserverDesignPattern;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BaseIdentity.PresentationLayer.Controllers
{
    public class DefaultController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly UserObserverSubject _userObserver;


        public DefaultController(UserManager<AppUser> userManager, UserObserverSubject userObserver)
        {
            _userManager = userManager;
            _userObserver = userObserver;
        }


        // GET: /<controller>/
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(UserRegisterViewModel model)
        {
            var appUser = new AppUser()
            {
                UserName = model.Username,
                Email = model.Mail,
                Name = model.Name,
                Surname = model.Surname
            };

            var result = await _userManager.CreateAsync(appUser, model.Password);
            if (result.Succeeded)
            {
                _userObserver.NotifyObserver(appUser);
                ViewBag.message = "Üyelik sistemi basarılı bir sekilde olusturuldu";
            }
            else
            {
                ViewBag.message = "Üyelik kaydında bir hata olustu";
            }
            return View();
        }
    }
}

