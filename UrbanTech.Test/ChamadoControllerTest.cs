using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrbanTech.Controllers;
using UrbanTech.Data.Contexts;
using UrbanTech.Models;

namespace UrbanTech.Test
{
    public class ChamadoControllerTest
    {
        private readonly Mock<DatabaseContext> _mockContext;
        private readonly ChamadoController _chamadoController;
        private readonly DbSet<ChamadoModel> _mockSet;

        public ChamadoControllerTest()
        {
            _mockContext = new Mock<DatabaseContext>();
            _mockSet = MockDbSet();
            _mockContext.Setup(m => m.Chamados).Returns(_mockSet);
            _chamadoController = new ChamadoController(_mockContext.Object);
        }

        private DbSet<ChamadoModel> MockDbSet()
        {
            var data = new List<ChamadoModel>
            {
                new ChamadoModel { Id = 1, Nome = "Cassio"},
                new ChamadoModel { Id = 2, Nome = "Henrique"}
            }.AsQueryable();

            var mockSet = new Mock<DbSet<ChamadoModel>>();

            mockSet.As<IQueryable<ChamadoModel>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<ChamadoModel>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<ChamadoModel>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<ChamadoModel>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            return mockSet.Object;
        }

        [Fact]
        public void Index_ReturnsViewWithChamados()
        {
            var result = _chamadoController.Index();

            var viewResult = Assert.IsType<ViewResult>(result);

            var model = Assert.IsAssignableFrom<IEnumerable<ChamadoModel>>(viewResult.Model);

            Assert.Equal(2, model.Count());
        }

        [Fact]
        public void Index_ReturnsViewWithoutChamados()
        {
            _mockSet.RemoveRange(_mockSet.ToList());
            _mockContext.Setup(m => m.Chamados).Returns(_mockSet);

            var result = _chamadoController.Index();

            var viewResult = Assert.IsType<ViewResult>(result);

            var model = Assert.IsAssignableFrom<IEnumerable<ChamadoModel>>(viewResult.Model);

            Assert.Empty(model);
        }

    }
}
