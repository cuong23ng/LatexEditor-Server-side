// docker-compose up -d
using Hustex_backend.Helpers;
using Hustex_backend.Models;
using Microsoft.EntityFrameworkCore;
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

    endpoints.MapGet("api/files/{userid:int}/{projectid:int}", async (HttpContext context, LatexDb db) => {
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

        string location = $"wwwroot\\Files\\{userid}\\{projectid}";

        // Console.WriteLine(location);
        if (Compiler.PDFConverter(location, filename)) {
            return Results.Ok();
        }
        else {
            return Results.Problem();
        }
    });

    endpoints.MapPut("api/save/{userid:int}/{projectid:int}/{filename}", async (HttpContext context, LatexDb db) => {
        var userid = int.Parse(string.Format("{0}", context.Request.RouteValues["userid"]));
        var projectid = int.Parse(string.Format("{0}", context.Request.RouteValues["projectid"]));
        var filename = string.Format("{0}", context.Request.RouteValues["filename"]);
        
        string path = $"wwwroot\\Files\\{userid}\\{projectid}\\{filename}.tex";
        if(await LatexWriter.SaveToFile(context, path)) {
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

        string path = $"wwwroot\\Files\\{userid}\\{projectid}\\{filename}.{fileType}";
        await System.IO.File.Create(path).DisposeAsync();
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

        string path = $"wwwroot\\Files\\{userid}\\{projectid}\\{filename}.{fileType}";
        System.IO.File.Delete(path);
    });

    endpoints.MapPost("api/import/{userid:int}/{projectid:int}", async (HttpContext context, LatexDb db) => {
        var userid = int.Parse(string.Format("{0}", context.Request.RouteValues["userid"]));
        var projectid = int.Parse(string.Format("{0}", context.Request.RouteValues["projectid"]));

        IFormFile file = context.Request.Form.Files[0];
        var path = $"wwwroot\\Files\\{userid}\\{projectid}\\{file.FileName}";

        using (var fileStream = new FileStream(path, FileMode.Create))
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


    endpoints.MapPost("api/create-new-project/{userid:int}/{username}", async (HttpContext context, LatexDb db) => {
        var userid = int.Parse(string.Format("{0}", context.Request.RouteValues["userid"]));
        var username = string.Format("{0}", context.Request.RouteValues["username"]);
        using var reader = new StreamReader(context.Request.Body);

        var body = await reader.ReadToEndAsync();

        var newProject = JsonConvert.DeserializeObject<Project>(body);

        Hustex_backend.Models.File newFile = new Hustex_backend.Models.File() { 
            FileName = "main", 
            Project = newProject,
            DataType = "tex"
        };

        db.Files.Add(newFile);

        db.Projects.Add(newProject);

        db.SaveChanges();

        Console.WriteLine($"Id: {newProject.ProjectId} - Name: {newProject.ProjectName}");

        var location = $"wwwroot\\Files\\{userid}\\{newProject.ProjectId}";
        Directory.CreateDirectory(location);

        var content = @"\documentclass{article}
\usepackage[utf8]{vietnam}
\usepackage{graphicx} % Required for inserting images
        
\title{" + newProject.ProjectName + "}" + @"
\author{" + username + "}" + @"
\date{" + DateTime.Now.ToString("MMMM") + " " + DateTime.Now.Year + "}" + @"
        
\begin{document}
        
\maketitle
        
\section{Introduction}
        
\end{document}";
        await System.IO.File.WriteAllTextAsync(Path.Combine(location, "main.tex"), content);
    });

    endpoints.MapDelete("api/delete-project/{userid:int}/{projectid:int}", async (HttpContext context, LatexDb db) => {
        var userid = int.Parse(string.Format("{0}", context.Request.RouteValues["userid"]));
        var projectid = int.Parse(string.Format("{0}", context.Request.RouteValues["projectid"]));

        string path = $"wwwroot\\Files\\{userid}\\{projectid}";
        Directory.Delete(path, true);

        var project = await (from p in db.Projects
                        where p.ProjectId == projectid
                        select p).FirstOrDefaultAsync();

        db.Projects.Remove(project);
        await db.SaveChangesAsync();
    });

    endpoints.MapPost("api/user", async (HttpContext context, LatexDb db) => {
        var newUser = RequestProcess.ProcessForm(context.Request);

        if (newUser != null) {
            db.Users.Add(newUser);
            await db.SaveChangesAsync();
        }

        var path = $"wwwroot\\Files\\{newUser.UserId}";
        Directory.CreateDirectory(path);

        return Results.Ok();
    });

});

app.Run();
