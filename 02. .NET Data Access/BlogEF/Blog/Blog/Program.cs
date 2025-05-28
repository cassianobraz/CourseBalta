using Blog.Data;
using Blog.Models;
using Microsoft.EntityFrameworkCore;

using var context = new BlogDataContext();

var posts = GetPosts(context, 0, 25);

Console.WriteLine("Teste");


static List<Post> GetPosts(BlogDataContext context, int skip, int take = 25)
{
    var posts = context
        .Posts
        .AsNoTracking()
        .Skip(skip)
        .Take(take)
        .ToList();
    return posts;
}