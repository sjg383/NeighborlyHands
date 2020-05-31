using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using NeighborlyHands;
using NeighborlyHands.Controllers;
using NeighborlyHands.Models;

namespace NeighborlyHands.Test.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Edit()
        {
            // Arrange
            HomeController controller = new HomeController();
            // Act
            ViewResult result = controller.Edit(3) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
