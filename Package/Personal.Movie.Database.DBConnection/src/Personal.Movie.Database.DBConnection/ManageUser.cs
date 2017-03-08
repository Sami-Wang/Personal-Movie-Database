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
                          and u.userPasswordHash = @userPassword",
                        new
                        {
                            userName = userName,
                            userPassword = userPasswordHash
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
    }
}
