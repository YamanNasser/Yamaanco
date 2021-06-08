namespace Yamaanco.Application.DTOs.Comment
{
    public class CommentUpvotedDto
    {
        public string Id { get; set; }
        public string Parent { get; set; }
        public string Root { get; set; }
        public string Content { get; set; }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string UpvoteById { get; set; }
        public string UpvoteByName { get; set; }
        public int UpvoteCount { get; set; }
        public string CreatedById { get; set; }
    }
}