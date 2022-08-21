using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sadikDevBlog.Data;
using sadikDevBlog.Models;
using sadikDevBlog.Models.Blog;
using sadikDevBlog.Models.Request;

namespace sadikDevBlog.Controllers;

[Authorize]
public class AdminController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;


    public AdminController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }
    public List<BlogPost> GetListBlogPost()
    {
        return _context.BlogPosts.ToList();
    }
    public IActionResult Add()
    {
        return View();
    }
    public async Task<bool> AddPost([FromBody] AddPostModel model)
    {
        var postForAdded = new BlogPost
        {
            CreateDate = DateTime.Now,
            UpdateDate = DateTime.Now,
            Header = model.Header,
            ShortContent = model.ShortContent,
            ImagePath = model.ImagePath,
            Content = new BlogPostDetail{
                Content = model.Content
            }
        };
        _context.Add(postForAdded);
        await _context.SaveChangesAsync();

        return postForAdded.Id != 0;
    }
    public IActionResult Update(int postId)
    {
        var blogPost = _context.BlogPosts.First(x => x.Id == postId);
        return View(blogPost);
    }
    public IActionResult Delete(int postId)
    {
        var blogPost = _context.BlogPosts.First(x => x.Id == postId);
        return View(blogPost);
    }
}

