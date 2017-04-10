using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Personal.Movie.Database.ViewModels.Account;
using Personal.Movie.Database.Helpers;
using Personal.Movie.Database.IRepository;
using Microsoft.AspNetCore.Identity;
using Personal.Movie.Database.Models.Account;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Personal.Movie.Database.Controllers
{
    public class AccountController : Controller
    {
        private SignInManager<AccountUser> signinManager { get; set; }
        private IAccountRepository accountRepository { get; set; }

        public AccountController(IAccountRepository accountRepositoryNew, SignInManager<AccountUser> signinManagerNew) {
            signinManager = signinManagerNew;
            accountRepository = accountRepositoryNew;
        }

        [HttpPost, ActionName("Register")]
        public async Task<string> Register(RegisterInputParameter registerInputParameter)
        {
            var validateUserResults = await accountRepository.Register(registerInputParameter);
            return "Hello";
        }

        [HttpPost, ActionName("Login")]
        public async Task<bool> Login(LoginInputParameter loginInputParameter) {
            var validateUserResults = await accountRepository.Login(loginInputParameter);
            if (validateUserResults.Count > 0)
            {
                AccountUser accountUser = new AccountUser();
                accountUser.userID = Convert.ToString(validateUserResults[0].userID);
                accountUser.userName = validateUserResults[0].userName;
                accountUser.Roles.Add(new AccountMapUserRole()
                {
                    userID = Convert.ToString(validateUserResults[0].userID),
                    roleID = Convert.ToString(validateUserResults[0].roleID)
                });
                await signinManager.SignInAsync(accountUser, false);
                return true;
            }
            else {
                return false;
            }
        }


        [HttpGet, ActionName("Logout")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            ViewData["userName"] = User.Identity.Name;
            if (ViewData["UserName"] != null)
            {
                await signinManager.SignOutAsync();
            }
            return RedirectToAction("Index", "Home");
        }

    }
}
