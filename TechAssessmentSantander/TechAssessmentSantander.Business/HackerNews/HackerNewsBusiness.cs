using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechAssessmentSantander.Entity.HackerNews;
using TechAssessmentSantander.InfraStructure.HackerNews;

namespace TechAssessmentSantander.Business.HackerNews
{
    /// <summary>
    /// Hacker News Business Class
    /// </summary>
    public class HackerNewsBusiness : IHackerNewsBusiness
    {
        private readonly ILogger<HackerNewsBusiness> logger;
        private readonly IConsumerHackerNews consumer;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger">Logger Interface</param>
        /// <param name="consumer">Consumer Interface</param>
        public HackerNewsBusiness
        (
            ILogger<HackerNewsBusiness> logger,
            IConsumerHackerNews consumer
        )
        {
            this.logger = logger;
            this.consumer = consumer;
        }

        /// <summary>
        /// Retrieve Best 20 Stories
        /// </summary>
        /// <returns>Best Stories List</returns>
        public async Task<List<GetBestsResult>> GetBestStories()
        {
            try
            {
                this.logger.LogInformation("Start Retrieve Best Story - Business");

                var result = new List<GetBestsResult>();
                var stories = await this.GetStories();
                if (stories != null && stories.Any())
                {
                    result = this.ProcessStories(stories);
                }

                this.logger.LogInformation("End Retrieve Best Story - Business");

                return result;
            }
            catch (Exception ex)
            {
                this.logger?.LogError(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Retrieve Stories
        /// </summary>
        /// <returns>Stories List</returns>
        private async Task<List<Story>> GetStories()
        {
            try
            {
                this.logger.LogInformation("Start Retrieve Story - Business");

                var result = new List<Story>();
                var bestStories = await this.consumer.GetBestStories();
                if (bestStories != null && bestStories.Ids.Any())
                {
                    var tasks = bestStories.Ids.Select(id => this.consumer.GetStory(id));
                    var responses = await Task.WhenAll(tasks);

                    result.AddRange(responses);

                }

                this.logger.LogInformation("End Retrieve Story - Business");

                if (!result.Any())
                {
                    this.logger.LogInformation("None Story Retrieved - Business");
                    return null;
                }

                return result;
            }
            catch (Exception ex)
            {
                this.logger?.LogError(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Process the stories retrieved
        /// </summary>
        /// <param name="stories"></param>
        /// <returns>Best Stories List</returns>
        private List<GetBestsResult> ProcessStories(List<Story> stories)
        {
            try
            {
                this.logger.LogInformation("Start Process Stories - Business");

                if (stories != null && stories.Any())
                {
                    var orderedStories = stories.OrderByDescending(h => h.Score).Take(20).ToList();

                    var result = Entity.HackerNews.Mapper.Mapper.MapList(orderedStories);

                    if (!result.Any())
                    {
                        this.logger.LogInformation("None Story Retrieved - Business");
                        return null;
                    }

                    this.logger.LogInformation("End Process Stories - Business");

                    return result;
                }
                else
                {
                    this.logger.LogInformation("None Story Retrieved - Business");
                    return null;
                }
            }
            catch (Exception ex)
            {
                this.logger?.LogError(ex.Message);
                return null;
            }
        }
    }
}
