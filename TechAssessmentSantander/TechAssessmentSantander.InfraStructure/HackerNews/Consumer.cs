using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechAssessmentSantander.Entity.HackerNews;

namespace TechAssessmentSantander.InfraStructure.HackerNews
{
    /// <summary>
    /// Hacker News Api Consumer Class
    /// </summary>
    public class Consumer : IConsumerHackerNews
    {
        private readonly ILogger<Consumer> logger;
        private readonly IConfiguration config;
        private readonly string baseUrl;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger">Logger Interface</param>
        /// <param name="config">Configuration Interface</param>
        public Consumer
        (
            ILogger<Consumer> logger,
            IConfiguration config
        )
        {
            this.logger = logger;
            this.config = config;
            this.baseUrl = this.config.GetSection("HackerNews").GetSection("BaseUrl").Value;
        }

        /// <summary>
        /// Retrieve the Best Stories from HackerNews
        /// </summary>
        /// <returns>Best Stories Id List</returns>
        public async Task<BestStories> GetBestStories()
        {
            try
            {
                var result = new BestStories();
                this.logger.LogInformation("Start Retrieve Best Stories");

                var bestStoriesUrl = this.config.GetSection("HackerNews").GetSection("BestStories").Value;

                var url = this.baseUrl + bestStoriesUrl;

                var ids = await HTTPClientWrapper<List<int>>.Get(url);

                if (ids != null && ids.Any())
                {
                    result.Ids.AddRange(ids);
                }

                this.logger.LogInformation("End Retrieve Best Stories");

                return result;
            }
            catch (System.Exception ex)
            {
                this.logger?.LogError(ex.Message);

                return null;
            }
        }

        /// <summary>
        /// Retrieve a Story from HackerNews
        /// </summary>
        /// <returns>Story</returns>
        public async Task<Story> GetStory(int id)
        {
            try
            {
                this.logger.LogInformation("Start Retrieve Story");

                var storyUrl = this.config.GetSection("HackerNews").GetSection("Story").Value;

                var url = this.baseUrl + string.Format(storyUrl, id);

                var result = await HTTPClientWrapper<Story>.Get(url);

                this.logger.LogInformation("End Retrieve Story");

                return result;
            }
            catch (System.Exception ex)
            {
                this.logger?.LogError(ex.Message);

                return null;
            }
        }
    }
}
