using System.Text;
using Mediator.API.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddDbContextFactory<LinkDbContext>(optionsBuilder => { optionsBuilder.UseNpgsql(builder.Configuration["ConnectionStrings:string"]); });

builder.Services.AddTransient<IPasswordHasher<User>, PasswordHasher<User>>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Secret"])),
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero
        };
    });

var app = builder.Build();
app.MapPost("/login", async (string email, string password, LinkDbContext dbContext, IPasswordHasher<User> passwordHasher) =>
{
    var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
    if (user == null || passwordHasher.VerifyHashedPassword(user, user.Password, password) != PasswordVerificationResult.Success)
    {
        return Results.Unauthorized();
    }
    
    var token = GenerateJwtToken(user);

    object GenerateJwtToken(User user1)
    {
        throw new NotImplementedException();
    }

    return Results.Ok(new { Token = token });
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

app.Run();