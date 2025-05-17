using DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Businness.Abstract;
using Businness.Concrete;
using DataAccess.Abstract;
using DataAccess.Ef;
using Core.Utilities.Security.JWT;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// DbContext
builder.Services.AddDbContext<NotlyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Servisler
builder.Services.AddScoped<ICommentService, CommentManager>();
builder.Services.AddScoped<ICommentDal, EfCommentDal>();

builder.Services.AddScoped<INoteService, NoteManager>();
builder.Services.AddScoped<INoteDal, EfNoteDal>();

builder.Services.AddScoped<IUserService, UserManager>();
builder.Services.AddScoped<IUserDal, EfUserDal>();

builder.Services.AddScoped<ILikeService, LikeManager>();
builder.Services.AddScoped<ILikeDal, EfLikeDal>();

builder.Services.AddScoped<ICourseService, CourseManager>();
builder.Services.AddScoped<ICourseDal, EfCourseDal>();

builder.Services.AddScoped<INoteAttachmentService, NoteAttachmentManager>();
builder.Services.AddScoped<INoteAttachmentDal, EfNoteAttachmentDal>();

builder.Services.AddScoped<ITokenHandler, TokenHandler>();
builder.Services.AddScoped<IAuthService, AuthManager>();

builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS Yapýlandýrmasý
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontendOrigin",
        policy => policy.WithOrigins("http://localhost:5173", "http://localhost:5174") // Her iki porta da izin verildi
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});

var app = builder.Build();

// Geliþtirme ortamý için Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Notly API V1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseRouting(); // ROUTING EKLENDÝ — önemli!

app.UseCors("AllowFrontendOrigin"); // CORS MIDDLEWARE doðru sýrada

app.UseAuthorization();

app.MapControllers();

app.Run();
