using DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Businness.Abstract;
using Businness.Concrete;
using DataAccess.Abstract;
using DataAccess.Ef;
using Core.Utilities.Security.JWT; 

using Microsoft.AspNetCore.Authentication.JwtBearer; 
using Microsoft.IdentityModel.Tokens;            
using System.Text;                                 

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<NotlyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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

builder.Services.AddScoped<ITokenHandler, Core.Utilities.Security.JWT.TokenHandler>();
builder.Services.AddScoped<IAuthService, AuthManager>();

builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false; 
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Token:Issuer"],
        ValidAudience = builder.Configuration["Token:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"]))
    };
});

builder.Services.AddAuthorization();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontendOrigin",
        policy => policy.WithOrigins("http://localhost:5173", "http://localhost:5174") 
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Notly API V1");
      
    });
}

app.UseHttpsRedirection();


app.UseStaticFiles();

app.UseRouting(); 

app.UseCors("AllowFrontendOrigin"); 


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
