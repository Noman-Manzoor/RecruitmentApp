using Domain.AutoMapper.Configuration;
using Domain.Entities;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Services.Implementation;
using Services.Interface;
using System;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(p => p.AddPolicy("RecruitmentApp", builder =>
{
     builder.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
}));


builder.Services.AddDbContext<DMRecruitmentContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(c => c.AddProfile<AutoMapperConfiguration>(), typeof(Program));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

builder.Services.AddScoped<IProfileManagementService, ProfileManagementService>();
builder.Services.AddScoped<IRecruiterService, RecruiterService>();
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<ILeadManagementService, LeadManagementService>();
builder.Services.AddScoped<IRecruitmentCompanyService, RecruitmentCompanyService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("RecruitmentApp");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
