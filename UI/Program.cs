using DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Businness.Abstract;
using Businness.Concrete;
using DataAccess.Abstract;
using DataAccess.Ef;
using Core.Utilities.Security.JWT;
using Microsoft.AspNetCore.Builder; // WebApplicationBuilder ve WebApplication i�in gerekli
using Microsoft.Extensions.DependencyInjection; // AddDbContext, AddScoped, AddControllers i�in gerekli
using Microsoft.Extensions.Hosting; // IsDevelopment i�in gerekli
using Microsoft.Extensions.Configuration; // GetConnectionString i�in gerekli

var builder = WebApplication.CreateBuilder(args);

// DbContext'i ekle, connection string appsettings.json'dan okunuyor
builder.Services.AddDbContext<NotlyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Servisleri ekle (Scoped lifetime kullan�l�yor)
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
// CORS Yap�land�rmas� Ba�lang�c�
// ************************************
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontendOrigin", // Politikan�n ad�
        builder => builder.WithOrigins("http://localhost:5174") // Frontend uygulaman�z�n �ALI�TI�I DO�RU ADRES
                         .AllowAnyHeader() // T�m ba�l�klara izin ver
                         .AllowAnyMethod()); // T�m HTTP metotlar�na (GET, POST, PUT, DELETE vb.) izin ver

    // E�er frontend'iniz farkl� portlarda da �al���yorsa (�rne�in hem 5173 hem 5174), birden fazla origin ekleyebilirsiniz:
    // options.AddPolicy("AllowFrontendOrigin",
    //     builder => builder.WithOrigins("http://localhost:5173", "http://localhost:5174")
    //                      .AllowAnyHeader()
    //                      .AllowAnyMethod());
});
// ************************************
// CORS Yap�land�rmas� Sonu
// ************************************


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    // Swagger UI'yi root ("/") dizinine a�
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Notly API V1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

// ************************************
// CORS Middleware Kullan�m� Ba�lang�c�
// ************************************
// CORS middleware'ini UseAuthorization'dan �nce kullan�n
app.UseCors("AllowFrontendOrigin"); // Tan�mlad���m�z politikan�n ad�n� belirtiyoruz
// ************************************
// CORS Middleware Kullan�m� Sonu
// ************************************


app.UseAuthorization();

app.MapControllers();

app.Run();
