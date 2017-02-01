using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Personal.Movie.Database.DBConnection.UnitTest
{
    public class ManageUserUnitTest
    {
        // Test ManageUser.ValidateUserNameAndPassword
        [Fact]
        public void ValidateUserNameAndPasswordPassingTest()
        {
            // Test Account Number
            var validateUserResult = ManageUser.ValidateUserNameAndPassword(
                ConnectionConst.CORRECTCONNECTIONSTRING, "test_f",
                @"bb96c2fc40d2d54617d6f276febe571f623a8dadf0b734855299b0e107fda32cf6b69f2da32b36445d73690b93cb
                  d0f7bfc20e0f7f28553d2a4428f23b716e90").Result;
            Assert.NotNull(validateUserResult);
        }
    }
}
