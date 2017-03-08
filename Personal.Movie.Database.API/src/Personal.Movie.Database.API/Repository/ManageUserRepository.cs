using ManageUserServiceReference;
using Personal.Movie.Database.API.IRepository;
using Personal.Movie.Database.Model.General;
using Personal.Movie.Database.Model.UserModel;
using System;
using System.Threading.Tasks;

namespace Personal.Movie.Database.API.Repository
{
    public class ManageUserRepository: IManageUserRepository
    {
        public async Task<ResponseData<ValidateUserResult>> ValidateUserNameAndPassword(string userName,
            string userPasswordHash)
        {
            ManageUserServiceClient client = new ManageUserServiceClient();
            ResponseData<ValidateUserResult> responseData = new ResponseData<ValidateUserResult>();
            try
            {
                responseData = await client.ValidateUserNameAndPasswordAsync(userName, userPasswordHash);               
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message.ToString());
                responseData.responseCode = (int)ResponseStatusEnum.APIError;
                responseData.responseStatusDescription = "API Error";
                responseData.responseResults = null;
            }
            return responseData;
        }
    }
}
