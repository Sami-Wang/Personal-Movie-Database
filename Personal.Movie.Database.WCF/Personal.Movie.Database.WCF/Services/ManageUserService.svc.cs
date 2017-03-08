using Personal.Movie.Database.DBConnection;
using Personal.Movie.Database.Model.General;
using Personal.Movie.Database.Model.UserModel;
using Personal.Movie.Database.WCF.IServices;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;

namespace Personal.Movie.Database.WCF.Services
{
    public class ManageUserService: IManageUserService
    {
        public async Task<ResponseData<ValidateUserResult>> ValidateUserNameAndPassword(string userName,
            string userPasswordHash)
        {
            ResponseData<ValidateUserResult> responseData = new ResponseData<ValidateUserResult>();
            try {
                List<ValidateUserResult> validateUserResults = await ManageUser.ValidateUserNameAndPassword(ConfigurationManager.AppSettings["ConnectionString"],
                    userName, userPasswordHash);
                if (validateUserResults == null)
                {
                    responseData.responseCode = (int)ResponseStatusEnum.DatabaseError;
                    responseData.responseStatusDescription = "Database Error";
                }
                else {
                    responseData.responseCode = (int)ResponseStatusEnum.Success;
                    responseData.responseStatusDescription = "Success";
                }
                responseData.responseResults = validateUserResults;
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message.ToString());
                responseData.responseCode = (int)ResponseStatusEnum.WCFServerError;
                responseData.responseStatusDescription = "WCF Error";
            }           
            return responseData;
        }
    }
}
