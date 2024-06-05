// docker-compose up -d
using Hustex_backend.Helpers;
using Hustex_backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<LatexDb>((options) => {
    Console.WriteLine("IN!!!!!!");
    options.UseSqlServer(builder.Configuration.GetConnectionString("Connection1"));
    Console.WriteLine("success!");
    });
builder.Services.AddDbContext<LatexDb>(opt => opt.UseInMemoryDatabase("Hustex"));

builder.Services.AddCors(options =>  
    {
    options.AddDefaultPolicy(  
        policy =>  
        {  
            policy.WithOrigins("http://localhost:8080")  
                .AllowAnyHeader()  
                .AllowAnyMethod();  
        });  
});  

var app = builder.Build();

app.UseCors(policy => 
    policy.AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader()
); 

app.UseStaticFiles();

app.UseRouting();

app.UseEndpoints((endpoints) => {

    endpoints.MapGet("api/helloworld", async (context) => {
        context.Response.WriteAsync("Hello world");
    });

    endpoints.MapPost("api/login", async (HttpContext context, LatexDb db) => {
        var user = RequestProcess.ProcessForm(context.Request);

        if (user != null) {
            var userfinding = (from u in db.Users
                                where u.Username == user.Username
                                && u.Password == user.Password
                                select u).FirstOrDefault();
            
            if (userfinding != null) {
                context.Response.StatusCode = StatusCodes.Status200OK;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(userfinding));
            }
            else context.Response.StatusCode = StatusCodes.Status404NotFound;
        }
    });

    endpoints.MapGet("api/project/{userid:int}", async (HttpContext context, LatexDb db) => {
        var userid = Int32.Parse(context.Request.RouteValues["userid"].ToString());

        List<Project> projects = await (from p in db.Projects
                        where p.UserId == userid
                        select p).ToListAsync();
        
        string Json = JsonConvert.SerializeObject(projects, Formatting.Indented);
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(Json);
    });

    endpoints.MapGet("api/project/{userid:int}/{projectid:int}", async (HttpContext context, LatexDb db) => {
        var projectid = Int32.Parse(context.Request.RouteValues["projectid"].ToString());

        List<Hustex_backend.Models.File> files = await (from file in db.Files
                        where file.ProjectId == projectid
                        select file).ToListAsync();

        string Json = JsonConvert.SerializeObject(files, Formatting.Indented);
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(Json);
    });

    endpoints.Map("api/compile/{userid:int}/{projectid:int}/{filename}", async (HttpContext context, LatexDb db) => {
        var userid = int.Parse(string.Format("{0}", context.Request.RouteValues["userid"]));
        var projectid = int.Parse(string.Format("{0}", context.Request.RouteValues["projectid"]));
        var filename = string.Format("{0}", context.Request.RouteValues["filename"]);

        string location = @"D:\git-repos\project2\Hustex-backend\wwwroot\Files\" + userid + @"\" + projectid + @"\";

        // Console.WriteLine(location);
        Compiler.PDFConverter(location, filename);
    });

    endpoints.MapPut("api/save/{userid:int}/{projectid:int}/{filename}", async (HttpContext context, LatexDb db) => {
        var userid = int.Parse(string.Format("{0}", context.Request.RouteValues["userid"]));
        var projectid = int.Parse(string.Format("{0}", context.Request.RouteValues["projectid"]));
        var filename = string.Format("{0}", context.Request.RouteValues["filename"]);
        
        string location = @"D:\git-repos\project2\Hustex-backend\wwwroot\Files\" + userid + @"\" + projectid + @"\" + filename + ".tex";
        if(await LatexWriter.SaveToFile(context, location)) {
            return Results.Ok();
        }
        else {
            return Results.Problem();
        }
    });

    endpoints.MapPost("api/create-new-file/{userid:int}/{projectid:int}", async (HttpContext context, LatexDb db) => {
        var userid = int.Parse(string.Format("{0}", context.Request.RouteValues["userid"]));
        var projectid = int.Parse(string.Format("{0}", context.Request.RouteValues["projectid"]));
        var filename = string.Format("{0}", context.Request.Query["filename"]);
        var fileType = string.Format("{0}", context.Request.Query["filetype"]);

        var project = await (from p in db.Projects
                        where p.ProjectId == projectid
                        select p).FirstOrDefaultAsync();

        var newFile = new Hustex_backend.Models.File{
            FileName = filename,
            Project = project,
            DataType = fileType
        };

        db.Files.Add(newFile);
        db.SaveChanges();

        string fileLocation = @"D:\git-repos\project2\Hustex-backend\wwwroot\Files\" + userid + @"\" + projectid + @"\" + filename + "." + fileType;
        await System.IO.File.Create(fileLocation).DisposeAsync();
    });

    endpoints.MapDelete("api/delete-file/{userid:int}/{projectid:int}", async (HttpContext context, LatexDb db) => {
        var userid = int.Parse(string.Format("{0}", context.Request.RouteValues["userid"]));
        var projectid = int.Parse(string.Format("{0}", context.Request.RouteValues["projectid"]));
        var filename = string.Format("{0}", context.Request.Query["filename"]);
        var fileType = string.Format("{0}", context.Request.Query["filetype"]);

        var project = await (from p in db.Projects
                        where p.ProjectId == projectid
                        select p).FirstOrDefaultAsync();
        
        var file = new Hustex_backend.Models.File{
            FileName = filename,
            Project = project,
            DataType = fileType
        };

        db.Files.Remove(file);
        db.SaveChanges();

        string fileLocation = @"D:\git-repos\project2\Hustex-backend\wwwroot\Files\" + userid + @"\" + projectid + @"\" + filename + "." + fileType;
        System.IO.File.Delete(fileLocation);
    });

    endpoints.MapPost("api/import/{userid:int}/{projectid:int}", async (HttpContext context, LatexDb db) => {
        var userid = int.Parse(string.Format("{0}", context.Request.RouteValues["userid"]));
        var projectid = int.Parse(string.Format("{0}", context.Request.RouteValues["projectid"]));

        IFormFile file = context.Request.Form.Files[0];
        var filePath = @"D:\git-repos\project2\Hustex-backend\wwwroot\Files\" + userid + @"\" + projectid + @"\" + file.FileName;

        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(fileStream);
        }
        var project = await (from p in db.Projects
                    where p.ProjectId == projectid
                    select p).FirstOrDefaultAsync();
        var filename = file.FileName.Split('.')[0];
        var fileType = file.FileName.Split('.')[1];
        var newFile = new Hustex_backend.Models.File{
            FileName = filename,
            Project = project,
            DataType = fileType
        };

        Console.WriteLine(newFile.DataType);
        db.Files.Add(newFile);
        db.SaveChanges();
    });


//     endpoints.MapPost("api/create-new-project", async (HttpContext context, LatexDb db) => {
//         using var reader = new StreamReader(context.Request.Body);

//         var body = await reader.ReadToEndAsync();

//         var newProject = JsonConvert.DeserializeObject<Project>(body);

//         db.Add(newProject);

//         TexFile newTexFile = new TexFile() { 
//             FileName = "main", 
//             Content = "\\documentclass{article}\n\\usepackage{graphicx} % Required for inserting images\n\n\\title{tes}\n\\author{Hùng Cường Nguyễn}\n\\date{May 2024}\n\n\\begin{document}\n\n\\maketitle\n\n\\section{Introduction}\n\n\\end{document}",
//             project = newProject
//         };

//         db.Add(newTexFile);

//         db.SaveChanges();

//         Console.WriteLine($"Id: {newProject.ProjectId} - Name: {newProject.ProjectName}");
//     });

//     endpoints.MapPut("api/compile/{fileid:int}", async (HttpContext context, LatexDb db) => {
//         using var reader = new StreamReader(context.Request.Body);

//         var body = await reader.ReadToEndAsync();

//         var newFile = JsonConvert.DeserializeObject<TexFile>(body);
        
//         var file = await (from f in db.TexFiles
//                     where f.ProjectId == 1 && f.FileId == newFile.FileId 
//                     select f).FirstOrDefaultAsync();

//         if (file is null) return Results.NotFound();
        
//         file.Content = newFile.Content;

//         await db.SaveChangesAsync();

//         return Results.NoContent();
//     });


//     endpoints.MapPost("api/user", async (HttpContext context, LatexDb db) => {
//         var newUser = RequestProcess.ProcessForm(context.Request);

//         if (newUser != null) {
//             db.Users.Add(newUser);
//             await db.SaveChangesAsync();
//         }

//         return Results.Ok();
//     });

});

app.Run();
