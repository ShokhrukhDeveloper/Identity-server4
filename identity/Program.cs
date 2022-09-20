using identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddIdentityServer().
    AddInMemoryApiResources(Config.GetApis()).
    AddInMemoryApiScopes(Config.GetScopes()).
    AddInMemoryClients(Config.GetClients()).
    AddDeveloperSigningCredential();

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCors();

app.UseAuthorization();
app.UseIdentityServer();

app.MapDefaultControllerRoute();

app.Run();
