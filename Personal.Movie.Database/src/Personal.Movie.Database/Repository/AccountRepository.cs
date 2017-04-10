using Newtonsoft.Json;
using Personal.Movie.Database.Helpers;
using Personal.Movie.Database.IRepository;
using Personal.Movie.Database.Model.UserModel;
using Personal.Movie.Database.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Personal.Movie.Database.Repository
{
    public class AccountRepository: IAccountRepository
    {
        public async Task<List<ValidateUserResult>> Register(RegisterInputParameter registerInputParameter)
        {
            try
            {
                Dictionary<string, string> registerInput = new Dictionary<string, string>();
                registerInput.Add("userName", registerInputParameter.userName);
                // Hash Password
                SHA512 sha512 = SHA512.Create();
                byte[] userPasswordHashBytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(registerInputParameter.password));
                string userPasswordHash = BitConverter.ToString(userPasswordHashBytes).Replace("-", "").ToLower();
                registerInput.Add("userPasswordHash", userPasswordHash);
                registerInput.Add("userRoleID", (1).ToString());
                registerInput.Add("userFirstName", registerInputParameter.firstName);
                registerInput.Add("userLastName", registerInputParameter.lastName);
                registerInput.Add("userEmail", registerInputParameter.email);
                string validateUserResultString = await PersonalMovieDatabaseAPIHandler.PostReqestHandler("ManageUser/RegisterUser",
                    registerInput);
                List<ValidateUserResult> validateUserResults = JsonConvert.DeserializeObject<List<ValidateUserResult>>(validateUserResultString);
                return validateUserResults;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return null;
            }
        }

        public async Task<List<ValidateUserResult>> Login(LoginInputParameter loginInputParameter)
        {
            try
            {
                Dictionary<string, string> loginInput = new Dictionary<string, string>();
                loginInput.Add("userName", loginInputParameter.userName);
                // Hash Password
                SHA512 sha512 = SHA512.Create();
                byte[] userPasswordHashBytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(loginInputParameter.password));
                string userPasswordHash = BitConverter.ToString(userPasswordHashBytes).Replace("-", "").ToLower();
                loginInput.Add("userPasswordHash", userPasswordHash);
                string validateUserResultString = await PersonalMovieDatabaseAPIHandler.PostReqestHandler("ManageUser/ValidateUserNameAndPassword",
                    loginInput);
                List<ValidateUserResult> validateUserResults = JsonConvert.DeserializeObject<List<ValidateUserResult>>(validateUserResultString);
                return validateUserResults;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return null;
            }
        }
    }
}
