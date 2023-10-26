using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using NLog.Web;
using Persistence.DataContext;
using Persistence.Repositories;
using System;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");
try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Logging.ClearProviders();
    builder.Host.UseNLog();


    //Add Database Context
    builder.Services.AddDbContext<TimesheetContext>(options =>
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            options.UseSqlServer(connectionString);
        });

    //To implement Auto Mapper
    builder.Services.AddAutoMapper(typeof(Program));

    //Inject repositories 
    builder.Services.AddScoped<IDesignationRepository, DesignationRepository>();
    builder.Services.AddScoped<IClientRepository, ClientRepository>();
    builder.Services.AddScoped<ITaskTypeRepository, TaskTypeRepository>();
    builder.Services.AddScoped<IClientContactRepository, ClientContactRepository>();
    builder.Services.AddScoped<ITeamMemberRepository, TeamMemberRepository>();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseCors(c => c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}

catch (Exception exception)
{
    logger.Error(exception, "Stopped program because of exception");
    throw (exception);
}
finally
{
    NLog.LogManager.Shutdown();
}

