using System.Text;
using Hustex_backend.Models;

public static class RequestProcess
{
    public static User ProcessForm(HttpRequest request)
    {
        string firstname  = "";
        string lastname  = "";
        string email  = "";
        string username  = "";
        string password  = "";

        if (request.Method == "POST")
        {
            var _form = request.Form;
            firstname = _form["firstname"].FirstOrDefault() ?? "";
            lastname = _form["lastname"].FirstOrDefault() ?? "";
            email = _form["email"].FirstOrDefault() ?? "";
            username = _form["username"].FirstOrDefault() ?? "";
            password = _form["password"].FirstOrDefault() ?? "";

            var thongbao = $@"Firstname: {firstname} - LastName: {lastname} - Username: {username} - Password: {password}";

            Console.WriteLine(thongbao);

            User newUser = new User() {
                FirstName = firstname,
                LastName = lastname,
                Email = email,
                Username = username,
                Password = password
            };
      
            return newUser;
        }

        if (request.Method == "GET")
        {
            var _form = request.Form;
            username = _form["username"].FirstOrDefault() ?? "";
            password = _form["password"].FirstOrDefault() ?? "";

            var thongbao = $@"Username: {username} - Password: {password}";

            Console.WriteLine(thongbao);

            User user = new User() {
                Username = username,
                Password = password
            };

            return user;
        }
        
        return null;
    }
}