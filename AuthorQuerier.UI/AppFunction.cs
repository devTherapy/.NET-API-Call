using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AuthorQuerier
{
    public class AppFunction
    {
        /// <summary>
        /// Gets a list of most active authors according to a set threshold 
        /// </summary>
        /// <param name="threshold"></param>
        /// <returns>List of the author names</returns>
        public static async Task<List<string>> GetUserNames(int threshold)
        {
            List<string> list = new List<string>();
            var authorObject = await AuthorClient.AuthorProcessor();
            var query = authorObject.Where(x => x.submission_count >= threshold);
            foreach (var item in query)
            {
                list.Add(item.username);
            }
            return list;
        }
        /// <summary>
        /// Gets the author with the highest comment count. 
        /// </summary>
        /// <returns>Name of the author</returns>
        public static async Task<string> GetUsernameWithHighestCommentCount()
        {
            var result = "";
            var authorObject = await AuthorClient.AuthorProcessor();
            var query = authorObject.OrderByDescending(x => x.comment_count).Take(1);
            foreach (var item in query)
            {
                result = $"{item.username} is the authorr with the highest comment count\n";
            }
            return result;
        }
        /// <summary>
        /// Gets the list of the authors sorted by when their record was created according to a set threshold. 
        /// </summary>
        /// <param name="threshold"></param>
        /// <returns>a List containing the names of the authors</returns>
        public static async Task<List<string>> GetUsernamesSortedByRecordDate(int threshold)
        {
            List<string> list = new List<string>();
            var authorObject = await AuthorClient.AuthorProcessor();
            var query = authorObject.Where(x => x.created_at >= threshold);
            foreach (var item in query)
            {
                list.Add(item.username);
            }
            return list;
        }
        /// <summary>
        /// Gets the usernames of authors according to the range of articles submitted
        /// </summary>
        /// <param name="ranges"></param>
        /// <returns>a dictionary mapping the range specified and the list of authors that fall within that range</returns>
        public static async Task<Dictionary<string, List<string>>> GetUsernamesAccordingToRange(Dictionary<int, int> ranges)
        {
            var authorObject = await AuthorClient.AuthorProcessor();
            var result = new Dictionary<string, List<string>>();
            foreach (KeyValuePair<int, int> range in ranges)
            {
                var query = authorObject.Where(x => x.submitted >= range.Key && x.submitted <= range.Value).Select(x => x.username);
                var list = query.ToList();
                result[$"{range.Key}-{range.Value}"] = list;
            }
            return result;
        }
    }
}
