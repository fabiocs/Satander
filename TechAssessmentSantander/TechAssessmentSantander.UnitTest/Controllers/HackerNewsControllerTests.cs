using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechAssessmentSantander.Business.HackerNews;
using TechAssessmentSantander.Controllers;
using TechAssessmentSantander.Entity.HackerNews;

namespace TechAssessmentSantander.UnitTest.Controllers
{
    [TestFixture]
    public class HackerNewsControllerTests
    {
        private Mock<ILogger<HackerNewsController>> mockLogger      = new Mock<ILogger<HackerNewsController>>();
        private Mock<IHackerNewsBusiness> mockHackerNewsBusiness    = new Mock<IHackerNewsBusiness>();

        [SetUp]
        public void SetUp()
        {
            this.BuildBusiness();
        }

        [Test]
        public async Task Best20_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var hackerNewsController = this.CreateHackerNewsController();

            // Act
            var result = await hackerNewsController.Best20();

            // Assert
            Assert.IsNotNull(result);
        }

        private void BuildBusiness()
        {
            var getResult = new GetBestsResult()
            {
                PostedBy = "grawprog",
                CommentCount = 447,
                Score = 1042,
                Time = DateTime.Now,
                Title = "Moderna’s HIV vaccine has officially begun human trials",
                Uri = "https://www.them.us/story/hiv-aids-vaccine-human-trials-moderna"
            };

            var resultList = new List<GetBestsResult>();
            resultList.Add(getResult);

            this.mockHackerNewsBusiness.Setup(m => m.GetBestStories()).ReturnsAsync(resultList);
        }

        private HackerNewsController CreateHackerNewsController()
        {
            return new HackerNewsController(
                this.mockLogger.Object,
                this.mockHackerNewsBusiness.Object);
        }
    }
}
