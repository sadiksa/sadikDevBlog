using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    [HttpPost]
    public async Task<bool> AddBlog([FromBody] AddPostModel model)
    {
        var postForAdded = new BlogPost
        {
            CreateDate = DateTime.Now,
            UpdateDate = DateTime.Now,
            Header = model.Header,
            ShortContent = model.ShortContent,
            ImagePath = model.ImagePath,
            Content = new BlogPostDetail
            {
                Content = model.Content
            }
        };
        _context.Add(postForAdded);
        await _context.SaveChangesAsync();

        return postForAdded.Id != 0;
    }
    public IActionResult Update(int postId)
    {
        var blogPost = _context.Set<BlogPost>().Include(x => x.Content).First(x => x.Id == postId);
        return View(blogPost);
    }
    [HttpPost]
    public async Task<bool> UpdateBlog([FromBody] UpdatePostModel model)
    {
        var postForUpdated = _context.Set<BlogPost>().Include(x => x.Content).First(x => x.Id == model.Id);
        postForUpdated.Header = model.Header;
        postForUpdated.ShortContent = model.ShortContent;
        postForUpdated.ImagePath = model.ImagePath;
        postForUpdated.UpdateDate = DateTime.Now;
        postForUpdated.Content.Content = model.Content;
        _context.Update(postForUpdated);
        await _context.SaveChangesAsync();

        return true;
    }
    public async Task<IActionResult> Delete(int postId)
    {
        var blogPost = _context.BlogPosts.First(x => x.Id == postId);
        _context.Remove(blogPost);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index", "Admin");
    }
}

