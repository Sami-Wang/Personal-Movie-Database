using Personal.Movie.Database.Model.General;
using Personal.Movie.Database.Model.UserModel;
using System.Threading.Tasks;

namespace Personal.Movie.Database.API.IRepository
{
    public interface IManageUserRepository
    {
        Task<ResponseData<ValidateUserResult>> ValidateUserNameAndPassword(string userName,
            string userPasswordHash);

        Task<ResponseData<ValidateUserResult>> RegisterUser(string userName, string userPasswordHash,
            int? userRoleID, string userFirstName, string userLastName, string userEmail);
    }
}
