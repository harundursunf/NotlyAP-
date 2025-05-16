using DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Businness.Abstract;
using Businness.Concrete;
using DataAccess.Abstract;
using DataAccess.Ef;
using Core.Utilities.Security.JWT;
using Microsoft.AspNetCore.Builder; // WebApplicationBuilder ve WebApplication için gerekli
using Microsoft.Extensions.DependencyInjection; // AddDbContext, AddScoped, AddControllers için gerekli
using Microsoft.Extensions.Hosting; // IsDevelopment için gerekli
using Microsoft.Extensions.Configuration; // GetConnectionString için gerekli

var builder = WebApplication.CreateBuilder(args);

// DbContext'i ekle, connection string appsettings.json'dan okunuyor
builder.Services.AddDbContext<NotlyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Servisleri ekle (Scoped lifetime kullanýlýyor)
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

// Swagger servislerini ekle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ************************************
// CORS Yapýlandýrmasý Baþlangýcý
// ************************************
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontendOrigin", // Politikanýn adý
        builder => builder.WithOrigins("http://localhost:5174") // Frontend uygulamanýzýn ÇALIÞTIÐI DOÐRU ADRES
                         .AllowAnyHeader() // Tüm baþlýklara izin ver
                         .AllowAnyMethod()); // Tüm HTTP metotlarýna (GET, POST, PUT, DELETE vb.) izin ver

    // Eðer frontend'iniz farklý portlarda da çalýþýyorsa (örneðin hem 5173 hem 5174), birden fazla origin ekleyebilirsiniz:
    // options.AddPolicy("AllowFrontendOrigin",
    //     builder => builder.WithOrigins("http://localhost:5173", "http://localhost:5174")
    //                      .AllowAnyHeader()
    //                      .AllowAnyMethod());
});
// ************************************
// CORS Yapýlandýrmasý Sonu
// ************************************


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    // Swagger UI'yi root ("/") dizinine aç
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Notly API V1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

// ************************************
// CORS Middleware Kullanýmý Baþlangýcý
// ************************************
// CORS middleware'ini UseAuthorization'dan önce kullanýn
app.UseCors("AllowFrontendOrigin"); // Tanýmladýðýmýz politikanýn adýný belirtiyoruz
// ************************************
// CORS Middleware Kullanýmý Sonu
// ************************************


app.UseAuthorization();

app.MapControllers();

app.Run();
