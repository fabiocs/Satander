using System;
using System.Collections.Generic;
using System.Linq;

namespace TechAssessmentSantander.Entity.HackerNews.Mapper
{
    /// <summary>
    /// Mapper Class
    /// </summary>
    public static class Mapper
    {
        /// <summary>
        /// Map Object
        /// </summary>
        /// <param name="input">Story Object</param>
        /// <returns>Output Best Story view Object</returns>
        public static GetBestsResult MapObject(Story input)
        {
            GetBestsResult output = null;

            if (input != null)
            {
                output = new GetBestsResult()
                {
                    Title = input.Title,
                    Time = UnixTimeStampToDateTime(input.Time),
                    CommentCount = input.Descendants,
                    PostedBy = input.By,
                    Score = input.Score,
                    Uri = input.Url
                };
            }

            return output;
        }

        /// <summary>
        /// Map List
        /// </summary>
        /// <param name="input">Story Object List</param>
        /// <returns>Output Best Story view Object List</returns>
        public static List<GetBestsResult> MapList(List<Story> input)
        {
            var output = new List<GetBestsResult>();

            if (input != null && input.Any())
            {
                foreach (var inputItem in input)
                {
                    var outputItem = MapObject(inputItem);
                    if (outputItem != null)
                    {
                        output.Add(outputItem);
                    }
                }
            }

            return output;
        }

        /// <summary>
        /// Convert Unix TimeStamp to DateTime
        /// </summary>
        /// <param name="unixTimeStamp">Unix Date value</param>
        /// <returns>DateTime</returns>
        private static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }
    }
}
