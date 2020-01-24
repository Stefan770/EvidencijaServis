using EvidencijaServis.Controllers;
using EvidencijaServis.Models;
using EvidencijaServis.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace EvidencijaServis.Tests.Controllers
{
    [TestClass]
    public class ZaposleniControllerTest
    {
        [TestMethod]
        public void GetReturnsZaposleniWithSameId()
        {
            // Arrange
            var mockRepository = new Mock<IZaposleniRepo>();
            mockRepository.Setup(x => x.GetById(1)).Returns(new Zaposleni { Id = 1, ImeIPrezime = "Pera Peric", GodinaRodjenja = 1991 });

            var controller = new ZaposleniController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.Get(1);
            var contentResult = actionResult as OkNegotiatedContentResult<Zaposleni>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(1, contentResult.Content.Id);
        }

        [TestMethod]
        public void PutReturnsBadRequest()
        {
            // Arrange
            var mockRepository = new Mock<IZaposleniRepo>();
            var controller = new ZaposleniController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.Put(2, new Zaposleni { Id = 1, ImeIPrezime = "Pera Peric", GodinaRodjenja = 1991 });

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }

        [TestMethod]
        public void GetReturnsMultipleObjects()
        {
            // Arrange
            List<Zaposleni> zaposleni = new List<Zaposleni>();
            zaposleni.Add(new Zaposleni { Id = 1, ImeIPrezime = "Pera Peric", GodinaRodjenja = 1991, Plata = 3000 });
            zaposleni.Add(new Zaposleni { Id = 2, ImeIPrezime = "Zika Peric", GodinaRodjenja = 1991, Plata = 2500 });

            var mockRepository = new Mock<IZaposleniRepo>();
            mockRepository.Setup(x => x.GetAll()).Returns(zaposleni.AsEnumerable());
            var controller = new ZaposleniController(mockRepository.Object);

            // Act
            IEnumerable<Zaposleni> result = controller.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(zaposleni.Count, result.ToList().Count);
            Assert.AreEqual(zaposleni.ElementAt(0), result.ElementAt(0));
            Assert.AreEqual(zaposleni.ElementAt(1), result.ElementAt(1));
        }

        [TestMethod]
        public void PostFilteredReturnsMultipleObjects()
        {
            // Arrange
            List<Zaposleni> zaposleni = new List<Zaposleni>();
            zaposleni.Add(new Zaposleni { Id = 1, ImeIPrezime = "Pera Peric", GodinaRodjenja = 1991, GodinaZaposlenja = 1990 });
            zaposleni.Add(new Zaposleni { Id = 2, ImeIPrezime = "Zika Peric", GodinaRodjenja = 1991, GodinaZaposlenja = 1994 });

            ZaposleniFilter filter = new ZaposleniFilter { Pocetak = 1990, Kraj = 1995 };

            var mockRepository = new Mock<IZaposleniRepo>();
            mockRepository.Setup(x => x.GetByZaposlenje(filter)).Returns(zaposleni.AsEnumerable());
            var controller = new ZaposleniController(mockRepository.Object);

            // Act
            IEnumerable<Zaposleni> result = controller.PostFiltered(filter);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(zaposleni.Count, result.ToList().Count);
            Assert.AreEqual(zaposleni.ElementAt(0), result.ElementAt(0));
            Assert.AreEqual(zaposleni.ElementAt(1), result.ElementAt(1));
            Assert.IsTrue(result.ElementAt(0).GodinaZaposlenja >= filter.Pocetak && result.ElementAt(0).GodinaZaposlenja <= filter.Kraj);
            Assert.IsTrue(result.ElementAt(1).GodinaZaposlenja >= filter.Pocetak && result.ElementAt(1).GodinaZaposlenja <= filter.Kraj);
        }

    }
}
