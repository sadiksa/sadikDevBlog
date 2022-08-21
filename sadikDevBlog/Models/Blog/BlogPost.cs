namespace sadikDevBlog.Models.Blog
{
    public class BlogPost{
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string Header { get; set; }
        public string ImagePath { get; set; }
        public string ShortContent { get; set; }
        public BlogPostDetail Content {get; set;}
    }
}