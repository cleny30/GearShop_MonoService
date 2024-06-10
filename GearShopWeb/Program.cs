var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Thêm dịch vụ HTTP context accessor
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// Thêm dịch vụ bộ nhớ đệm phân tán (Distributed Memory Cache)
builder.Services.AddDistributedMemoryCache();

builder.Services.AddRazorPages();

// Thêm và cấu hình session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Thời gian hết hạn session
    options.Cookie.HttpOnly = true; // Chỉ cho phép truy cập session thông qua HTTP
    options.Cookie.IsEssential = true; // Đánh dấu cookie này là cần thiết
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Home}/{id?}");

app.Run();
