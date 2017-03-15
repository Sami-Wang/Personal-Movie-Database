using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace IdentityServerDBConnection.UnitTest
{
    public class IdentityServerDBHelperUnitTest
    {
        // Test IdentityServerDBHelper.GetClientByClientID
        [Fact]
        public void GetClientByClientIDPassingTest()
        {
            var clients = IdentityServerDBHelper.GetClientByClientID(ConnectionConst.CORRECTCONNECTIONSTRING, 
                1).Result;
            Assert.NotNull(clients);
        }

        // Test IdentityServerDBHelper.GetAllowedScopeByClientID
        [Fact]
        public void GetAllowedScopeByClientIDPassingTest()
        {
            var allowedScopes = IdentityServerDBHelper.GetAllowedScopeByClientID(ConnectionConst.CORRECTCONNECTIONSTRING,
                1).Result;
            Assert.NotNull(allowedScopes);
        }

        // Test IdentityServerDBHelper.GetResourceByScopeName
        [Fact]
        public void GetResourceByScopeNamePassingTest()
        {
            var resources = IdentityServerDBHelper.GetResourceByScopeName(ConnectionConst.CORRECTCONNECTIONSTRING,
                "ManageUser").Result;
            Assert.NotNull(resources);
        }

        // Test IdentityServerDBHelper.ValidateUserNameAndPassword
        [Fact]
        public void ValidateUserNameAndPasswordPassingTest()
        {
            var users = IdentityServerDBHelper.ValidateUserNameAndPassword(ConnectionConst.CORRECTCONNECTIONSTRING,
                "PersonalMovieDBUI", "PersonalMovieDBUI00..").Result;
            Assert.NotNull(users);
        }

        // Test IdentityServerDBHelper.GetRolesByUserSubject
        [Fact]
        public void GetRolesByUserSubjectPassingTest()
        {
            var roles = IdentityServerDBHelper.GetRolesByUserSubject(ConnectionConst.CORRECTCONNECTIONSTRING,
                "3f765b07-8b93-4250-91e8-a053d5e1fe3b").Result;
            Assert.NotNull(roles);
        }

        // Test IdentityServerDBHelper.GetAllResources
        [Fact]
        public void GetAllResourcesPassingTest()
        {
            var resources = IdentityServerDBHelper.GetAllResources(ConnectionConst.CORRECTCONNECTIONSTRING).Result;
            Assert.NotNull(resources);
        }

        // Test IdentityServerDBHelper.GetRolesByClientID
        [Fact]
        public void GetRolesByClientIDPassingTest()
        {
            var roles = IdentityServerDBHelper.GetRolesByClientID(ConnectionConst.CORRECTCONNECTIONSTRING, 2).Result;
            Assert.NotNull(roles);
        }
    }
}
