using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Adneom.CoffeMachine.Controllers;
using Adneom.CoffeMachine.Domain.Entities;
using Adneom.CoffeMachine.Domain.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Adneom.UnitTestProject
{
    public class ControllerTests
    {
        [Fact]
        public void VerifyIndexViewType()
        {
            var machineRepository = new Mock<IGenericRepository<Machine>>();
            var controller = new MachineController(machineRepository.Object);
            var result = controller.Index();
            Assert.IsType<Task<IActionResult>>(result);

        }

        [Fact]
        public void VerifyListTypeBoissonCount()
        {
            var productRepository = new Mock<IGenericRepository<TypeBoisson>>();
            productRepository.Setup(x => x.GetAll()).Returns(new List<TypeBoisson>
            {
                new TypeBoisson()
                {
                    Id = 1, TypeBoisson1 = "CAFE"
                },
                new TypeBoisson()
                {
                    Id = 2, TypeBoisson1 = "CHOCOLAT"
                },
                new TypeBoisson()
                {
                    Id = 3, TypeBoisson1 = "THE"
                }
            });
          
            var controller = new TypeBoissonController(productRepository.Object);
            var result = Assert.IsType<Task<IActionResult>>(controller.Index());
            var model = Assert.IsType<List<TypeBoisson>>(result.Result);
            Assert.Equal(3, model.Count);
        }

    }
}
