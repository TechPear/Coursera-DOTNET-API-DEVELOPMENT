//using Microsoft.OpenApi.Models;
using Microsoft.OpenApi;
using UserManagementAPI.Services;
using UserManagementAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

// ------------------------------------------------------
// Services
// ------------------------------------------------------

// Add controllers
builder.Services.AddControllers();

// Enable Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "User Management API",
        Version = "v1",
        Description = "API for managing users, roles, and authentication."
    });
    
    // // Commented due to a version conflicts with .net version and Swashbuckle
    // // // Add token-based authentication support
    // // options.AddSecurityDefinition("Token", new OpenApiSecurityScheme
    // // {
    // //     Name = "Authorization",
    // //     Type = SecuritySchemeType.ApiKey,
    // //     In = ParameterLocation.Header,
    // //     Description = "Enter your API token (e.g., my-secret-token)"
    // // });

    // // options.AddSecurityRequirement(new OpenApiSecurityRequirement
    // // {
    // //     {
    // //         new OpenApiSecurityScheme
    // //         {
    // //             Reference = new OpenApiReference
    // //             {
    // //                 Type = ReferenceType.SecurityScheme,
    // //                 Id = "Token"
    // //             }
    // //         },
    // //         Array.Empty<string>()
    // //     }
    // // });
});



//Ensures ASP.NET Core automatically returns 400 Bad Request when validation fails.
builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = false;
    });

// CORS (optional but recommended)
// builder.Services.AddCors(options =>
// {
//     options.AddPolicy("AllowAll", policy =>
//     {
//         policy.AllowAnyOrigin()
//               .AllowAnyMethod()
//               .AllowAnyHeader();
//     });
// });

// Add logging (already configured by default, but you can extend it here)
builder.Logging.AddConsole();

// ------------------------------------------------------
// Services DI
// ------------------------------------------------------
builder.Services.AddSingleton<UserService>();

// ------------------------------------------------------
// Build app
// ------------------------------------------------------

var app = builder.Build();

// Enable Swagger in development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseErrorHandling();
//app.UseSimpleAuthentication();
app.UseRequestLogging();

// Enable HTTPS redirection
// app.UseHttpsRedirection();

// Enable CORS
//app.UseCors("AllowAll");

// Authorization middleware (even if not used yet)
app.UseAuthorization();

// Map controllers
app.MapControllers();

app.Run();