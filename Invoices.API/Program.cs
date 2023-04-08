using Invoices.API.BL.Helper;
using Invoices.API.BL.Interface;
using Invoices.API.BL.Repository;
using Invoices.API.DAL.Extend;
using Invoices.BL.Interface;
using Invoices.BL.Mapper;
using Invoices.BL.Repository;
using Invoices.DAL.Database;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Products.BL.Repository;
using Sections.BL.Repository;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true)
    // JSON Formate
    .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

builder.Services.Configure<Jwt>(builder.Configuration.GetSection("Jwt"));

// Database Codiguration
builder.Services.AddDbContextPool<InvoiceContext>(opt =>
                opt.UseSqlServer(builder.Configuration.GetConnectionString("InvoiceConnection")));
// Auto Mapper
builder.Services.AddAutoMapper(x => x.AddProfile(new DomainProfile()));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<InvoiceContext>();

// Dependency Injection
builder.Services.AddScoped<IInvoiceRep, InvoiceRep>();
builder.Services.AddScoped<ISectionRep, SectionRep>();
builder.Services.AddScoped<IProductRep, ProductRep>();
builder.Services.AddScoped<IInvoiceDetailsRep, InvoiceDetailsRep>();
builder.Services.AddScoped<IInvoiceAttachmentsRep, InvoiceAttachmentsRep>();
builder.Services.AddScoped<IUserRep, UserRep>();
builder.Services.AddScoped<IRoleRep, RoleRep>();
builder.Services.AddScoped<IAuthRep, AuthRep>();

// --- The AddAuthenication method in the Program.cs file is used to configure JWT authentication at the time when the application starts. --
// -- It specifies the authentication scheme as JwtBearer ----
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    // ------ AddJwtBearer method helps configure token parameters. --------
    .AddJwtBearer(o =>
    {
        // ----- The Issuer, Audience, and Key values are read from the appsettings.json config file.
        // ----- The TokenValidationParameters instance is used to indicate if the Issuer, Audience, Key, and Lifetime information should be validated or not.
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey
            (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true
        };
    });

builder.Services.AddSwaggerGen();

builder.Services.AddCors();
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});


app.UseCors(options => options
.AllowAnyOrigin()
.AllowAnyMethod()
.AllowAnyHeader());
// Configure the HTTP request pipeline.

app.UseAuthentication();
app.UseAuthorization();

app.MapDefaultControllerRoute();

app.Run();
