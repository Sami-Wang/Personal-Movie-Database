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
            string userPassword) {
            using (MySqlConnection mysqlConnection = new MySqlConnection(connectionString))
            {
                try
                {
                    mysqlConnection.Open();
                    List<ValidateUserResult> validateUserResult = (await mysqlConnection.QueryAsync<ValidateUserResult>(
                        @"select userID, userName, roleID, roleName 
                          from wyhdb.User u, wyhdb.Role r 
                          where u.userRoleID = r.roleID 
                          and u.userName = @userName 
                          and u.userPassword = @userPassword",
                        new
                        {
                            userName = userName,
                            userPassword = userPassword
                        })).ToList();
                    return validateUserResult;
                }
                catch (Exception ex)
                {
                    return null;
                }
                finally {
                    mysqlConnection.Close();
                }
            }
        }
    }
}
