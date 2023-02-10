using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using ToDo;
using ToDo.Entity;
using ToDo.Exceptions;
using ToDo.Repository;
using ToDo.Service;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ToDo.Service.telegram.actions;
using ToDo.Telegram.Bot;
using ToDo.Telegram.actions;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Internal;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// получаем строку подключения из файла конфигурации
string connection = builder.Configuration.GetConnectionString("DefaultConnection");

// добавляем контекст ApplicationContext в качестве сервиса в приложение
builder.Services.AddDbContextFactory<ToDoContext>( //AddDbContext
    options => options.UseNpgsql(connection));

builder.Services.AddAutoMapper(typeof(AppMappingProfile));


builder.Services.AddTransient<TagRepository>();
builder.Services.AddTransient<TagService>();

builder.Services.AddTransient<PostRepository>();
builder.Services.AddTransient<PostService>();

//bot
builder.Services.AddTransient<IChain, Start>();
builder.Services.AddTransient<IChain, CreatePost>();
builder.Services.AddTransient<IChain, GetTitlePost>();
builder.Services.AddTransient<IChain, TagsListNextPage>();
builder.Services.AddTransient<IChain, TagsHasBeenSelected>();


builder.Services.AddTransient<BotHandler>();
builder.Services.AddHostedService<InitBot>();
var app = builder.Build();




using (var lifeCheckConnecntion = app.Services.GetService<IDbContextFactory<ToDoContext>>()?.CreateDbContext())
{
    if (!lifeCheckConnecntion?.Database.CanConnect() ?? true)
    {
        throw new Exception("Data base connection error");
    }

}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseAuthorization();

app.UseExceptionHandler(exptionApp =>
{
    exptionApp.Run(async context =>
    {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = Text.Plain;


        var exceptionHandlerPathFeature =
            context.Features.Get<IExceptionHandlerPathFeature>();

        if (exceptionHandlerPathFeature?.Error is NotFoundException)
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            await context.Response.WriteAsync("The row was not found.");
        }
    });
});


app.MapControllers();

app.Run();