﻿using BTLPhuongTienGiaoThong.Models;
using BTLPhuongTienGiaoThong.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// Cấu hình dịch vụ session
builder.Services.AddDistributedMemoryCache(); // Lưu trữ session trong bộ nhớ
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Thời gian hết hạn session
    options.Cookie.HttpOnly = true; // Cookie chỉ có thể được truy cập từ HTTP
    options.Cookie.IsEssential = true; // Cookie cần thiết cho ứng dụng
});

var connectionString = builder.Configuration.GetConnectionString("HireCarContext");
builder.Services.AddDbContext<HireCarContext>(x=>x.UseSqlServer(connectionString));
builder.Services.AddScoped<IHangXeRepository, HangXeRepository>();
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
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
