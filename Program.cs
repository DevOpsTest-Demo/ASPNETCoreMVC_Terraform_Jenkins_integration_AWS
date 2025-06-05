var builder = WebApplication.CreateBuilder(args);

//Add controller classes services 
builder.Services.AddControllers();

var app = builder.Build();

//Add static files
app.UseStaticFiles();

//enable routing
app.UseRouting();

//Map controller to the class
app.MapControllers();

app.Run();