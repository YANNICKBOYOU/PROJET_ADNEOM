using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Adneom.CoffeMachine.Controllers;
using Adneom.CoffeMachine.Domain.Entities;
using Adneom.CoffeMachine.Domain.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTestProject1
{
    [TestClass]
    public class ControllerTests
    {
       // private Mock<ICountryService> _countryServiceMock;

       private Mock<IGenericRepository<Machine>> context;
        MachineController objController;
        List<Machine> listMachines;

        [TestInitialize]
        public void TestMethodInit()
        {
            context = new Mock<IGenericRepository<Machine>>();
            objController = new MachineController(context.Object);
            listMachines = new List<Machine>() {
                new Machine() { Id = 1, Nom = "US" },
                new Machine() { Id = 2, Nom = "India" },
                new Machine() { Id = 3, Nom = "Russia" }
            };
        }

        [TestMethod]
        public void Country_Get_All()
        {
            //Arrange
            context.Setup(x => x.GetAll()).Returns(listMachines);

            //Act
            // var result = ((objController.Index() as ViewResult).Model) as List<Machine>;

            var result = objController.Index();
            //Assert
            Assert.AreEqual(result.Count, 3);
            Assert.AreEqual("US", result[0].Nom);
            Assert.AreEqual("India", result[1].Nom);
            Assert.AreEqual("Russia", result[2].Nom);

        }


        //[TestMethod]
        //public void TestMethod1()
        //{
        //    var machineRepository = new Mock<IGenericRepository<Machine>>();
        //    var controller = new MachineController(machineRepository.Object);
        //    var result =  controller.Index();
        //    Assert.AreEqual<Task<IActionResult>>(result);
        //}
    }
}
