using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Personal.Movie.Database.API.IRepository;
using Personal.Movie.Database.Model.General;
using Personal.Movie.Database.Model.UserModel;
using Personal.Movie.Database.API.Models.ManageUser;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Personal.Movie.Database.API.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ManageUserController : Controller
    {
        public ManageUserController(IManageUserRepository manageUserRepositoryNew)
        {
            manageUserRepository = manageUserRepositoryNew;
        }

        private IManageUserRepository manageUserRepository;

        // POST http://localhost:62610/api/ManageUser/ValidateUserNameAndPassword
        [HttpPost, ActionName("ValidateUserNameAndPassword")]
        public async Task<ResponseData<ValidateUserResult>> ValidateUserNameAndPassword(
            [FromBody]ValidateUserNameAndPasswordInput validateUserNameAndPasswordInput)
        {
            return await manageUserRepository.ValidateUserNameAndPassword(validateUserNameAndPasswordInput.userName,
                validateUserNameAndPasswordInput.userPasswordHash);
        }
    }
}
