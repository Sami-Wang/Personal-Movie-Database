using Personal.Movie.Database.Model.General;
using Personal.Movie.Database.Model.UserModel;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Personal.Movie.Database.WCF.IServices
{
    [ServiceContract]
    interface IManageUserService
    {
        [OperationContract]
        Task<ResponseData<ValidateUserResult>> ValidateUserNameAndPassword(string userName,
            string userPasswordHash);

        [OperationContract]
        Task<ResponseData<ValidateUserResult>> RegisterUser(string userName, string userPasswordHash,
            int? userRoleID, string userFirstName, string userLastName, string userEmail);
    }
}
