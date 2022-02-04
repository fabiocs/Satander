using System.Collections.Generic;
using System.Threading.Tasks;
using TechAssessmentSantander.Entity.HackerNews;

namespace TechAssessmentSantander.Business.HackerNews
{
    /// <summary>
    /// Hacker News Business Interface
    /// </summary>
    public interface IHackerNewsBusiness
    {
        /// <summary>
        /// Retrieve Best 20 Stories
        /// </summary>
        /// <returns>Best Stories List</returns>
        public Task<List<GetBestsResult>> GetBestStories();
    }
}
