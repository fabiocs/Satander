using System.Threading.Tasks;
using TechAssessmentSantander.Entity.HackerNews;

namespace TechAssessmentSantander.InfraStructure.HackerNews
{
    /// <summary>
    /// Hacker News Api Consumer Interface
    /// </summary>
    public interface IConsumerHackerNews
    {
        /// <summary>
        /// Retrieve the Best Stories from HackerNews
        /// </summary>
        /// <returns>Best Stories Id List</returns>
        public Task<BestStories> GetBestStories();

        /// <summary>
        /// Retrieve a Story from HackerNews
        /// </summary>
        /// <returns>Story</returns>
        public Task<Story> GetStory(int id);
    }
}
