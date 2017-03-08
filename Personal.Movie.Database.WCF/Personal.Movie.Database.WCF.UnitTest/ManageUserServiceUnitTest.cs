﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Personal.Movie.Database.WCF.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal.Movie.Database.WCF.UnitTest
{
    [TestClass]
    public class ManageUserServiceUnitTest
    {
        private ManageUserService manageUserService;

        [TestInitialize]
        public void ManageUserServiceUnitTestInitialize()
        {
            manageUserService = new ManageUserService();
        }

        [TestMethod]
        public void ValidateUserNameAndPasswordPassingTest()
        {
            var responseData = manageUserService.ValidateUserNameAndPassword("test_f",
                "bb96c2fc40d2d54617d6f276febe571f623a8dadf0b734855299b0e107fda32cf6b69f2da32b36445d73690b93cbd0f7bfc20e0f7f28553d2a4428f23b716e90").Result;
            Assert.AreNotEqual(null, responseData);
        }
    }
}