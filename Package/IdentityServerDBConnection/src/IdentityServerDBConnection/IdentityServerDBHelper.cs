using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Threading.Tasks;

namespace IdentityServerDBConnection
{
    public class IdentityServerDBHelper
    {
        /// <summary>
        /// Get Client By Client ID.
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="clientID"></param>
        /// <returns></returns>
        public static async Task<dynamic> GetClientByClientID(string connectionString, int clientID)
        {
            using (MySqlConnection mysqlConnection = new MySqlConnection(connectionString))
            {
                try
                {
                    mysqlConnection.Open();
                    dynamic clients = await mysqlConnection.QueryAsync(
                        @"select * from tblClient
                          where @clientID = clientID
                          and enabled = 1",
                        new
                        {
                            clientID = clientID
                        });
                    return clients;
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

        /// <summary>
        /// Get Allowed Scope By Client ID.
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="clientID"></param>
        /// <returns></returns>
        public static async Task<dynamic> GetAllowedScopeByClientID(string connectionString, int clientID)
        {
            using (MySqlConnection mysqlConnection = new MySqlConnection(connectionString))
            {
                try
                {
                    mysqlConnection.Open();
                    dynamic allowedScopes = await mysqlConnection.QueryAsync(
                        @"select s.scopeName from tblMapClientResource mcr, tblResource r, 
                          tblMapResourceScope mrs, tblScope s
                          where mcr.resourceID = r.resourceID
                          and mcr.resourceID = mrs.resourceID
                          and mrs.scopeID = s.scopeID
                          and mcr.clientID = @clientID
                          and r.enabled = 1",
                        new
                        {
                            clientID = clientID
                        });
                    return allowedScopes;
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

        /// <summary>
        /// Get Resource By Scope Name.
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="scopeName"></param>
        /// <returns></returns>
        public static async Task<dynamic> GetResourceByScopeName(string connectionString, string scopeName)
        {
            using (MySqlConnection mysqlConnection = new MySqlConnection(connectionString)) {
                try
                {
                    mysqlConnection.Open();
                    dynamic resources = await mysqlConnection.QueryAsync(
                        @"select r.resourceName, r.resourceDescription, r.resourceSecrets, r.enabled, s.scopeName, 
                          s.scopeDisplayName, s.scopeDescription from tblMapResourceScope mrs, tblResource r, tblScope s
                          where mrs.resourceID = r.resourceID
                          and mrs.scopeID = s.scopeID
                          and s.scopeName = @scopeName
                          and r.enabled = 1",
                        new
                        {
                            scopeName = scopeName
                        });
                    return resources;
                }
                catch (Exception ex) {
                    Console.WriteLine(ex.Message.ToString());
                    return null;
                }
                finally
                {
                    mysqlConnection.Close();
                }
            }              
        }

        /// <summary>
        /// Validate UserName and Password.
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="userName"></param>
        /// <param name="userPassword"></param>
        /// <returns></returns>
        public static async Task<dynamic> ValidateUserNameAndPassword(string connectionString,
            string userName, string userPassword)
        {
            using (MySqlConnection mysqlConnection = new MySqlConnection(connectionString))
            {
                try
                {
                    mysqlConnection.Open();
                    dynamic users = await mysqlConnection.QueryAsync(
                        @"select u.userSubject from tblUser u
                          where u.userName = @userName
                          and u.userPassword = @userPassword
                          and u.enabled = 1",
                        new
                        {
                            userName = userName,
                            userPassword = userPassword
                        });
                    return users;
                }
                catch (Exception ex) {
                    Console.WriteLine(ex.Message.ToString());
                    return null;
                }
                finally
                {
                    mysqlConnection.Close();
                }
            }
        }

        /// <summary>
        /// Get Roles By User Subject.
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="userSubject"></param>
        /// <returns></returns>
        public static async Task<dynamic> GetRolesByUserSubject(string connectionString,
            string userSubject)
        {
            using (MySqlConnection mysqlConnection = new MySqlConnection(connectionString)) {
                try
                {
                    mysqlConnection.Open();
                    dynamic roles = await mysqlConnection.QueryAsync(
                        @"select r.roleName from tblMapUserRole mur, tblUser u, tblRole r
                          where mur.userID = u.userID
                          and mur.roleID = r.roleID
                          and u.userSubject = @userSubject
                          and r.enabled = 1
                          and u.enabled = 1",
                        new
                        {
                            userSubject = userSubject
                        });
                    return roles;
                }
                catch (Exception ex) {
                    Console.WriteLine(ex.Message.ToString());
                    return null;
                }
                finally
                {
                    mysqlConnection.Close();
                }
            }
        }

        /// <summary>
        /// Get All Resources.
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static async Task<dynamic> GetAllResources(string connectionString)
        {
            using (MySqlConnection mysqlConnection = new MySqlConnection(connectionString))
            {
                try
                {
                    mysqlConnection.Open();
                    dynamic resources = await mysqlConnection.QueryAsync(
                        @"select r.resourceName, r.resourceDescription, r.resourceSecrets, r.enabled, s.scopeName, 
                          s.scopeDisplayName, s.scopeDescription from tblMapResourceScope mrs, tblResource r, tblScope s
                          where mrs.resourceID = r.resourceID
                          and mrs.scopeID = s.scopeID
                          and r.enabled = 1");
                    return resources;
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

        /// <summary>
        /// Get Roles By Client ID.
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="clientID"></param>
        /// <returns></returns>
        public static async Task<dynamic> GetRolesByClientID(string connectionString,
            int clientID)
        {
            using (MySqlConnection mysqlConnection = new MySqlConnection(connectionString)) {
                try
                {
                    mysqlConnection.Open();
                    dynamic roles = await mysqlConnection.QueryAsync(
                        @"SELECT r.roleName FROM tblMapClientRole mcr, tblClient c, tblRole r
                          where mcr.clientID = c.clientID
                          and mcr.roleID = r.roleID
                          and c.enabled = 1
                          and r.enabled = 1",
                        new
                        {
                            clientID = clientID
                        });
                    return roles;
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
