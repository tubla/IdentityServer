﻿using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Movies.Client.ApiServices;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IMovieApiService,MovieApiService>();

// OpenID Connect Configuration

builder.Services.AddAuthentication(options =>
                                        {
                                            options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                                            options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                                        })
                                        .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options => {

                                            options.Cookie.SameSite = SameSiteMode.Strict;
                                        })
                                        .AddOpenIdConnect(options =>
                                        {
                                            options.Authority = "https://localhost:5005";
                                            options.ClientId = "movies_mvc_client";
                                            options.ClientSecret = "secret";
                                            options.ResponseType = "code";

                                            options.Scope.Add("openid");
                                            options.Scope.Add("profile");

                                            options.SaveTokens = true;

                                            options.GetClaimsFromUserInfoEndpoint = true;

                                        });

// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
