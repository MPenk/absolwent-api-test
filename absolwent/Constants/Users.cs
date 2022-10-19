using absolwent.Models;

namespace absolwent.Constants
{
    public class Users
    {

        public Dictionary<string, User> List = new Dictionary<string, User>
        {
            {"michu.penk@gmail.com", new User { Id = 1, Email = "michu.penk@gmail.com", Password = "123" }},
            {"mimi20000808@gmail.com", new User { Id = 2, Email = "mimi20000808@gmail.com", Password = "123" }},
            {"absolwent.best@gmail.com", new User { Id = 3, Email = "absolwent.best@gmail.com", Password = "123" }},
            {"kowalkewit@gmail.com", new User { Id = 4, Email = "kowalkewit@gmail.com", Password = "123" }},
            {"jakubsewastianow@gmail.com", new User { Id = 5, Email = "jakubsewastianow@gmail.com", Password = "123" }}
        };

    }
}
