using ef_csv;
using BloggingContext? db = new();

string[] users = File.ReadAllLines($"Users.csv");
string[] posts = File.ReadAllLines($"Posts.csv");
string[] blogs = File.ReadAllLines($"Blogs.csv");

// Filling in tables
foreach (var u in users)
{
    string[] splitUsers = u.Split(';');
    User user = new User
    {
        UserId = int.Parse(splitUsers[0]),
        Username = splitUsers[1],
        Email = splitUsers[2],
        Password = splitUsers[3],
    };
    
    db.Users.Add(user);
} 

db.SaveChanges();



