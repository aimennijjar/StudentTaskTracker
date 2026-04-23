using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudentTaskTrackerMVC.Data;
using StudentTaskTrackerMVC.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// SQLite database for Identity
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=auth.db"));

// Identity + Roles
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<ApplicationDbContext>();

// API service
builder.Services.AddHttpClient<ApiService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5112/");
});

var app = builder.Build();

// Create roles automatically + create default users
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    string[] roles = { "Admin", "User", "Viewer" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    // Admin user
    string adminEmail = "admin@test.com";
    string adminPassword = "Admin123!";

    var adminUser = await userManager.FindByEmailAsync(adminEmail);

    if (adminUser == null)
    {
        adminUser = new IdentityUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(adminUser, adminPassword);

        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }
    else if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
    {
        await userManager.AddToRoleAsync(adminUser, "Admin");
    }

    // Normal user
    string userEmail = "user@test.com";
    string userPassword = "User123!";

    var normalUser = await userManager.FindByEmailAsync(userEmail);

    if (normalUser == null)
    {
        normalUser = new IdentityUser
        {
            UserName = userEmail,
            Email = userEmail,
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(normalUser, userPassword);

        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(normalUser, "User");
        }
    }
    else if (!await userManager.IsInRoleAsync(normalUser, "User"))
    {
        await userManager.AddToRoleAsync(normalUser, "User");
    }

    // Viewer user
    string viewerEmail = "viewer@test.com";
    string viewerPassword = "Viewer123!";

    var viewerUser = await userManager.FindByEmailAsync(viewerEmail);

    if (viewerUser == null)
    {
        viewerUser = new IdentityUser
        {
            UserName = viewerEmail,
            Email = viewerEmail,
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(viewerUser, viewerPassword);

        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(viewerUser, "Viewer");
        }
    }
    else if (!await userManager.IsInRoleAsync(viewerUser, "Viewer"))
    {
        await userManager.AddToRoleAsync(viewerUser, "Viewer");
    }

    // Give "User" role to any account that has no role yet
foreach (var existingUser in userManager.Users.ToList())
{
    var userRoles = await userManager.GetRolesAsync(existingUser);

    if (!userRoles.Any())
    {
        await userManager.AddToRoleAsync(existingUser, "User");
    }
}
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages();

app.Run();