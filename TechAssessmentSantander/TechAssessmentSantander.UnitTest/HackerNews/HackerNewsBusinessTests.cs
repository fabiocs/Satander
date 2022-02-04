using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechAssessmentSantander.Business.HackerNews;
using TechAssessmentSantander.Entity.HackerNews;
using TechAssessmentSantander.InfraStructure.HackerNews;

namespace TechAssessmentSantander.UnitTest.HackerNews
{
    [TestFixture]
    public class HackerNewsBusinessTests
    {
        private Mock<ILogger<HackerNewsBusiness>> mockLogger = new Mock<ILogger<HackerNewsBusiness>>();
        private Mock<IConsumerHackerNews> mockConsumerHackerNews = new Mock<IConsumerHackerNews>();

        [SetUp]
        public void SetUp()
        {
            this.BuildConsumer();
        }

        [Test]
        public async Task GetBestStories_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var hackerNewsBusiness = this.CreateHackerNewsBusiness();

            // Act
            var result = await hackerNewsBusiness.GetBestStories();

            // Assert
            Assert.IsNotNull(result);
        }

        private HackerNewsBusiness CreateHackerNewsBusiness()
        {
            return new HackerNewsBusiness(
                this.mockLogger.Object,
                this.mockConsumerHackerNews.Object);
        }

        private void BuildConsumer()
        {
            List<int> ids = new List<int>();
            ids.Add(30150586);
            ids.Add(30150343);

            var bestStories = new BestStories();
            bestStories.Ids.AddRange(ids);

            var storyOne = new Story()
            {
                By = "grawprog",
                Descendants = 447,
                Id = 30150586,
                Score = 1042,
                Time = 1643649372,
                Title = "Moderna’s HIV vaccine has officially begun human trials",
                Type = "story",
                Url = "https://www.them.us/story/hiv-aids-vaccine-human-trials-moderna"
            };
            var storyTwo = new Story()
            {
                By = "amadeuspzs",
                Descendants = 599,
                Id = 30150343,
                Score = 1005,
                Time = 1643648604,
                Title = "The new hire who showed up is not the same person we interviewed",
                Type = "story",
                Url = "https://www.askamanager.org/2022/01/the-new-hire-who-showed-up-is-not-the-same-person-we-interviewed.html"
            };

            this.mockConsumerHackerNews.Setup(m => m.GetBestStories()).ReturnsAsync(bestStories);
            this.mockConsumerHackerNews.Setup(m => m.GetStory(ids[0])).ReturnsAsync(storyOne);
            this.mockConsumerHackerNews.Setup(m => m.GetStory(ids[1])).ReturnsAsync(storyTwo);
        }
    }
}
