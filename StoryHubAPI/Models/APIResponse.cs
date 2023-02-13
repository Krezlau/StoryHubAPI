using System.Net;

namespace StoryHubAPI.Models
{
    public class APIResponse<T> where T : class
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public List<string> ErrorMessages { get; set; } = new List<string>();
        public T? Result { get; set; }
    }
}
