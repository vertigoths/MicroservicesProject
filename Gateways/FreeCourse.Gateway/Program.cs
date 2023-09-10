using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication().
	AddJwtBearer("GatewayAuthenticationSchema", options =>
	{
		options.Authority = builder.Configuration["IdentityServerURL"];
		options.Audience = "resource_gateway";
		options.RequireHttpsMetadata = false;
	});

builder.Services.AddOcelot();

builder.Configuration
	.SetBasePath(builder.Environment.ContentRootPath)
	.AddJsonFile("appsettings.json", true, true)
	.AddJsonFile($"configuration.{builder.Environment.EnvironmentName}.json")
	.AddEnvironmentVariables();

var app = builder.Build();

await app.UseOcelot();

app.MapGet("/", () => "Hello World!");

app.Run();