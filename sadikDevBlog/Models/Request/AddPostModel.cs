namespace sadikDevBlog.Models.Request
{
    public class AddPostModel{
        public string Header { get; set; }
        public string ShortContent { get; set; }
        public string Content { get; set; }
        public string ImagePath { get; set; }
    }
}