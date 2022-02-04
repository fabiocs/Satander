using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using TechAssessmentSantander.Business.HackerNews;

namespace TechAssessmentSantander.Controllers
{
    /// <summary>
    /// Hacker News Controller
    /// </summary>
    [Route("api/[controller]")]
    public class HackerNewsController : Controller
    {
        private readonly ILogger<HackerNewsController> logger;
        private readonly IHackerNewsBusiness business;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger">Logger Interface</param>
        /// <param name="business">Business Interface</param>
        public HackerNewsController
        (
            ILogger<HackerNewsController> logger,
            IHackerNewsBusiness business
        )
        {
            this.logger = logger;
            this.business = business;
        }

        /// <summary>
        /// Retrive best 20 Stories
        /// </summary>
        /// <returns>Stories List</returns>
        [HttpGet]
        public async Task<ActionResult<string>> Best20()
        {
            try
            {
                this.logger.LogInformation("Start Retrieve Best 20 Stories - Controller");

                var result = await this.business.GetBestStories();

                this.logger.LogInformation("End Retrieve Best 20 Stories - Controller");

                return Ok(result);
            }
            catch (System.Exception ex)
            {
                this.logger?.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
