using ef_csv;
using BloggingContext? db = new();

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

foreach (User b in db.Users)
{
    Console.WriteLine(b.Email);
}

db.SaveChanges();













