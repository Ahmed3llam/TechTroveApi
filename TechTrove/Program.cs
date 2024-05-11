using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;
using TechTrove.Models;
using TechTrove.UnitOfWork;
internal class Program
{
    private static void Main(string[] args)
    {
        string txt = "";

        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        });
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(
                o =>
                {
                    o.SwaggerDoc("v1", new OpenApiInfo()
                    {
                        Title = "TechTrove Api",
                        Description = " Api To Manage TechTrove",
                        Version = "v1",
                        TermsOfService = new Uri("http://tempuri.org/terms"),
                        Contact = new OpenApiContact
                        {
                            Name = "Allam",
                            Email = "ahmedmohamedallam230@gmail.com",

                        },
                    });
                    o.IncludeXmlComments("D:\\iti\\9m\\Api\\ApiDay2\\ApiDay2\\xmlfile");
                    o.EnableAnnotations();
                }
            );
        builder.Services.AddDbContext<TechTroveContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("Db"));
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });
        builder.Services.AddIdentity<User, IdentityRole>(
                    options =>
                    {
                        options.User.RequireUniqueEmail = true;
                        options.Password.RequireNonAlphanumeric = false;
                        options.Password.RequireDigit = false;
                        options.Password.RequireUppercase = false;
                        options.Password.RequireLowercase = false;
                        options.Password.RequiredLength = 8;
                    }).AddEntityFrameworkStores<TechTroveContext>();
        builder.Services.AddCors(options =>
        {
            options.AddPolicy(txt,
            builder =>
            {
                builder.AllowAnyOrigin();
                builder.AllowAnyMethod();
                builder.AllowAnyHeader();
            });
        });

        builder.Services.AddScoped<Unit>();

        builder.Services.AddAuthentication(option => option.DefaultAuthenticateScheme = "schema")
                    .AddJwtBearer("schema",
                    op =>
                    {
                        string key = "Iti Pd And Bi 44 Menoufia Web Api And Angular Courses";
                        var secertkey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));

                        op.TokenValidationParameters = new TokenValidationParameters()
                        {
                            IssuerSigningKey = secertkey,
                            ValidateIssuer = false,
                            ValidateAudience = false
                        };
                    }
                    );
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
 app.UseCors(txt);

        app.UseAuthorization();

       
        app.MapControllers();

        app.Run();
    }
}