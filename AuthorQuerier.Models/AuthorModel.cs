using System;
using System.Collections.Generic;
using System.Text;
namespace AuthorQuerier
{
    /// <summary>
    /// Creates an author model modelling the author data fetched from the api resource.
    /// </summary>
    public class AuthorModel
    {
        public int id { get; set; }
        public string username { get; set; }
        public string about { get; set; }
        public int submitted { get; set; }
        public DateTime updated_at { get; set; }
        public int submission_count { get; set; }
        public int comment_count { get; set; }
        public int created_at { get; set; }
    }
}
