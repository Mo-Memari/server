using Microsoft.Extensions.FileProviders;

void WriteFile()
{
    const string path = "./serverdata/file.txt";

    if (File.Exists(path))
    {
        File.Delete(path);
    }

    using var fs = File.Create(path);
    var rand = new Random();
    var content = new byte[1024];
    rand.NextBytes(content);
    fs.Write(content, 0, content.Length);
}


var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/file.txt", () =>
{
    WriteFile();
    return "server app";
});

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "serverdata")),
});

app.Run();