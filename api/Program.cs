var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer",
    setting=>{
        setting.Authority="https://localhost:7067/";
        setting.Audience="Api_1";
        setting.RequireHttpsMetadata=false;
        setting.TokenValidationParameters=new Microsoft.IdentityModel.Tokens.TokenValidationParameters(){
            ValidateAudience=true
        };
     
    }
    ); 
    // builder.Services.AddAuthentication("Bearer")
    //     .AddIdentityServerAuthentication(
    //         "Bearer",options=>{
    //             options.ApiName="Api_1";
    //             options.Authority="https://localhost:7067/";
    //         }
    //     );

// builder.Services.AddAuthorization(
//     option=>option.AddPolicy
//     (
//         "ApiScope",
//         policy=>
//         {
//             policy.RequireAuthenticatedUser();
//             policy.RequireClaim("scope","read");
//         }
//     )
// );

builder.Services.AddCors(
    option=>option.AddDefaultPolicy(
        o=>
        {
            o.AllowAnyOrigin();
            o.AllowAnyHeader();
            o.AllowAnyMethod();
        }
    )
);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
