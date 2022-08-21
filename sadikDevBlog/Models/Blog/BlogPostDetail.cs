using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sadikDevBlog.Models.Blog
{
    public class BlogPostDetail
    {
        [Key, ForeignKey(nameof(BlogPost))]
        public int Id { get; set; }
        public BlogPost BlogPost { get; set; }
        public string Content { get; set; }
    }
}