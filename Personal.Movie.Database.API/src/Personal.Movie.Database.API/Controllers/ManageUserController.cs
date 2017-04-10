using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Personal.Movie.Database.API.IRepository;
using Personal.Movie.Database.Model.General;
using Personal.Movie.Database.Model.UserModel;
using Personal.Movie.Database.API.Models.ManageUser;
using Microsoft.AspNetCore.Authorization;

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
        /// <summary>
        /// Validate User Name And Password.
        /// </summary>
        /// <remarks>
        /// Example Input:
        ///  
        ///     {
        ///        "userName": "test_f",
        ///        "userPasswordHash": "bb96c2fc40d2d54617d6f276febe571f623a8dadf0b734855299b0e107fda32cf6b69f2da32b36445d73690b93cbd0f7bfc20e0f7f28553d2a4428f23b716e90"
        ///     }
        /// 
        /// </remarks>
        /// <param name="validateUserNameAndPasswordInput"></param>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpPost, ActionName("ValidateUserNameAndPassword")]
        [ProducesResponseType(typeof(ResponseData<ValidateUserResult>), 200)]
        public async Task<ResponseData<ValidateUserResult>> ValidateUserNameAndPassword(
            [FromBody]ValidateUserNameAndPasswordInput validateUserNameAndPasswordInput)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    if (User.Claims.Single(r => r.Type == "role").Value ==
                        Startup.Configuration.GetSection("AccessLevel").GetSection("AccessLevel1").Value)
                    {
                        if (ModelState.IsValid)
                        {
                            return await manageUserRepository.ValidateUserNameAndPassword(
                            validateUserNameAndPasswordInput.userName,
                            validateUserNameAndPasswordInput.userPasswordHash);
                        }
                        else {
                            return new ResponseData<ValidateUserResult>()
                            {
                                responseCode = (int)ResponseStatusEnum.BadRequest,
                                responseStatusDescription = "Bad Request",
                                responseResults = null
                            };
                        }
                    }
                    else
                    {
                        return new ResponseData<ValidateUserResult>()
                        {
                            responseCode = (int)ResponseStatusEnum.AuthorizationFail,
                            responseStatusDescription = "Authorization Fail",
                            responseResults = null
                        };
                    }
                }
                else
                {
                    return new ResponseData<ValidateUserResult>()
                    {
                        responseCode = (int)ResponseStatusEnum.AuthenticationFail,
                        responseStatusDescription = "Authentication Fail",
                        responseResults = null
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return new ResponseData<ValidateUserResult>()
                {
                    responseCode = (int)ResponseStatusEnum.APIError,
                    responseStatusDescription = "API Error",
                    responseResults = null
                };
            }
        }

        // POST http://localhost:62610/api/ManageUser/RegisterUser
        /// <summary>
        /// Register New User.
        /// </summary>
        /// <remarks>
        /// Example Input:
        ///  
        ///     {
        ///        "userName": "test_user",
        ///        "userPasswordHash": "bb96c2fc40d2d54617d6f276febe571f623a8dadf0b734855299b0e107fda32cf6b69f2da32b36445d73690b93cbd0f7bfc20e0f7f28553d2a4428f23b716e90",
        ///        "userRoleID": 1,
        ///        "userFirstName": "Test",
        ///        "userLastName": "Test",
        ///        "userEmail": "test_free@gmail.com"
        ///     }
        /// 
        /// </remarks>
        /// <param name="registerUserInput"></param>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpPost, ActionName("RegisterUser")]
        [ProducesResponseType(typeof(ResponseData<ValidateUserResult>), 200)]
        public async Task<ResponseData<ValidateUserResult>> RegisterUser(
            [FromBody]RegisterUserInput registerUserInput)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    if (User.Claims.Single(r => r.Type == "role").Value ==
                        Startup.Configuration.GetSection("AccessLevel").GetSection("AccessLevel1").Value)
                    {
                        if (ModelState.IsValid)
                        {
                            return await manageUserRepository.RegisterUser(registerUserInput.userName,
                                registerUserInput.userPasswordHash, registerUserInput.userRoleID,
                                registerUserInput.userFirstName, registerUserInput.userLastName,
                                registerUserInput.userEmail);
                        }
                        else
                        {
                            return new ResponseData<ValidateUserResult>()
                            {
                                responseCode = (int)ResponseStatusEnum.BadRequest,
                                responseStatusDescription = "Bad Request",
                                responseResults = null
                            };
                        }
                    }
                    else
                    {
                        return new ResponseData<ValidateUserResult>()
                        {
                            responseCode = (int)ResponseStatusEnum.AuthorizationFail,
                            responseStatusDescription = "Authorization Fail",
                            responseResults = null
                        };
                    }
                }
                else
                {
                    return new ResponseData<ValidateUserResult>()
                    {
                        responseCode = (int)ResponseStatusEnum.AuthenticationFail,
                        responseStatusDescription = "Authentication Fail",
                        responseResults = null
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return new ResponseData<ValidateUserResult>()
                {
                    responseCode = (int)ResponseStatusEnum.APIError,
                    responseStatusDescription = "API Error",
                    responseResults = null
                };
            }
        }
    }
}
