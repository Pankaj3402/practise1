using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using WebApplication1.Controllers;
using WebApplication1.Models;

namespace WebApplication1.Tests.Controllers
{
    [TestFixture]
    public class HomeControllerTests
    {
        private HomeController? _controller;
        private Mock<ILogger<HomeController>>? _mockLogger;

        [SetUp]
        public void Setup()
        {
            _mockLogger = new Mock<ILogger<HomeController>>();
            _controller = new HomeController(_mockLogger.Object);
        }

        [Test]
        public void Index_Returns_ViewResult()
        {
            var result = _controller!.Index();
            Assert.That(result, Is.InstanceOf<ViewResult>());
        }

        [Test]
        public void Privacy_Returns_ViewResult()
        {
            var result = _controller!.Privacy();
            Assert.That(result, Is.InstanceOf<ViewResult>());
        }

        [Test]
        public void Error_Returns_ViewResult_With_ErrorViewModel()
        {
            var result = _controller!.Error();

            Assert.That(result, Is.InstanceOf<ViewResult>());

            var viewResult = result as ViewResult;
            Assert.That(viewResult!.Model, Is.InstanceOf<ErrorViewModel>());
        }

        [Test]
        public void Error_Model_Should_Have_RequestId()
        {
            var result = _controller!.Error() as ViewResult;
            Assert.That(result, Is.Not.Null);

            var model = result!.Model as ErrorViewModel;
            Assert.That(model, Is.Not.Null);
            Assert.That(model!.RequestId, Is.Not.Empty);
        }
    }
}
