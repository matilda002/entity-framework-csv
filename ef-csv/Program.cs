using ef_csv;
using Microsoft.EntityFrameworkCore;

using BloggingContext db = new();

string[] users = File.ReadAllLines($"Users.csv");
string[] posts = File.ReadAllLines($"Posts.csv");
string[] blogs = File.ReadAllLines($"Blogs.csv");


foreach (var row in users)
{
    string[] splitUsers = row.Split(';');
    int userId = int.Parse(splitUsers[0]);
    
    var existingUser = db.Users.Find(userId);
    if (existingUser != null)
    {
        existingUser.Username = splitUsers[1];
        existingUser.Email = splitUsers[2];
        existingUser.Password = splitUsers[3];
    }
    else
    {
        User newUser = new User
        {
            UserId = userId,
            Username = splitUsers[1],
            Email = splitUsers[2],
            Password = splitUsers[3],
        };
        db.Users.Add(newUser);
    }  
}
db.SaveChanges();

foreach (var row in blogs)
{
    string[] splitBlogs = row.Split(';');
    int blogId = int.Parse(splitBlogs[0]);
    
    var existingBlog = db.Blogs.Find(blogId);
    if (existingBlog != null)
    {
        existingBlog.Url = splitBlogs[1];
        existingBlog.BlogName = splitBlogs[2];
    }
    else
    {
        Blog newBlog = new Blog()
        {
            BlogId = blogId,
            Url = splitBlogs[1],
            BlogName = splitBlogs[2]
        };
        db.Blogs.Add(newBlog);
    }  
}
db.SaveChanges();

foreach (var row in posts)
{
    string[] splitPosts = row.Split(';');
    int postId = int.Parse(splitPosts[0]);
    
    var existingPost = db.Posts.Find(postId);
    if (existingPost != null)
    {
        existingPost.Title = splitPosts[1];
        existingPost.Content = splitPosts[2];
        existingPost.Published = splitPosts[3];
        existingPost.BlogId = Convert.ToInt32(splitPosts[4]); 
        existingPost.UserId = Convert.ToInt32(splitPosts[5]);
    }
    else
    {
        Post newPost = new Post()
        {
            PostId = postId,
            Title = splitPosts[1],
            Content = splitPosts[2],
            Published = splitPosts[3],
            BlogId = Convert.ToInt32(splitPosts[4]),
            UserId = Convert.ToInt32(splitPosts[5])
        };
        db.Posts.Add(newPost);
    }
}
db.SaveChanges();

var orderedPosts = db.Posts.OrderBy(p => p.Blog.BlogName)
    .Include(post => post.User).ToList();

foreach (Blog b in db.Blogs)
{
    if (b.BlogId == 1)
    {
        Console.WriteLine("╔═══════════════════════════════════════╗\n"+
                          $"            {b.BlogName}");
        Console.WriteLine("╚═══════════════════════════════════════╝");
        foreach (var p in orderedPosts)
        {
            if (p.BlogId == 1)
            
            {
                Console.WriteLine($"\nTitle: {p.Title}\n" +
                                  $"Content: {p.Content}\n" +
                                  $"Author: {p.User.Username}\n" + 
                                  $"Email: {p.User.Email} | Password: {p.User.Password}\n" +
                                  $"Published on: {p.Published}\n" +
                                  "-----------------------");
            }
        } 
    }

    Console.WriteLine();
    
    if (b.BlogId == 2)
    {
        Console.WriteLine("╔═══════════════════════════════════════╗\n"+
                          $"            {b.BlogName}");
        Console.WriteLine("╚═══════════════════════════════════════╝");
        foreach (var p in orderedPosts)
        {
            if (p.BlogId == 2)
            {
                Console.WriteLine($"\nTitle: {p.Title}\n" +
                                  $"Content: {p.Content}\n" +
                                  $"Author: {p.User.Username}\n" + 
                                  $"Email: {p.User.Email} | Password: {p.User.Password}\n" +
                                  $"Published on: {p.Published}\n" +
                                  "-----------------------");
            }
        }  
    }
}
db.SaveChanges();