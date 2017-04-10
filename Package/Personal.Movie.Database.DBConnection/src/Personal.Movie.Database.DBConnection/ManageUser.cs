using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Personal.Movie.Database.Model.UserModel;
using MySql.Data.MySqlClient;
using Dapper;

namespace Personal.Movie.Database.DBConnection
{
    public class ManageUser
    {
        public static async Task<List<ValidateUserResult>> ValidateUserNameAndPassword(string connectionString, string userName, 
            string userPasswordHash) {
            using (MySqlConnection mysqlConnection = new MySqlConnection(connectionString))
            {
                try
                {
                    mysqlConnection.Open();
                    List<ValidateUserResult> validateUserResult = (await mysqlConnection.QueryAsync<ValidateUserResult>(
                        @"select userID, userName, roleID, roleName
                          from tblUser u, tblRole r 
                          where u.userRoleID = r.roleID 
                          and u.userName = @userName 
                          and u.userPasswordHash = @userPasswordHash",
                        new
                        {
                            userName = userName,
                            userPasswordHash = userPasswordHash
                        })).ToList();
                    return validateUserResult;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message.ToString());
                    return null;
                }
                finally {
                    mysqlConnection.Close();
                }
            }
        }

        public static async Task<List<ValidateUserResult>> RegisterUser(string connectionString, string userName,
            string userPasswordHash, int? userRoleID, string userFirstName, string userLastName, string userEmail)
        {
            using (MySqlConnection mysqlConnection = new MySqlConnection(connectionString))
            {
                try
                {
                    mysqlConnection.Open();
                    await mysqlConnection.ExecuteAsync(
                        @"insert into tblUser(userName, userPasswordHash, userRoleID, userFirstName, 
                          userLastName, userEmail) values (@userName, @userPasswordHash, @userRoleID, 
                          @userFirstName, @userLastName, @userEmail)",
                        new
                        {
                            userName = userName,
                            userPasswordHash = userPasswordHash,
                            userRoleID = userRoleID,
                            userFirstName = userFirstName,
                            userLastName = userLastName,
                            userEmail = userEmail
                        });
                    List<ValidateUserResult> validateUserResult = await ValidateUserNameAndPassword(
                        connectionString, userName, userPasswordHash);
                    return validateUserResult;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message.ToString());
                    return null;
                }
                finally
                {
                    mysqlConnection.Close();
                }
            }
        }
    }
}
