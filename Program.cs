using eticketing.Application.Security;
using eticketing.Application.Services;
using eticketing.Http.Middlewares;
using eticketing.Infrastructure.Database;
using eticketing.Infrastructure.Job;
using eticketing.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Quartz;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder
    .Configuration.SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Services.AddDbContext<ETicketingDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"))
);
builder.Services.AddTransient<JwtService>();
builder.Services.AddTransient<AuthMiddleware>();
builder.Services.AddTransient<EventService>();
builder.Services.AddTransient<EventRepository>();
builder.Services.AddTransient<CustomerService>();
builder.Services.AddTransient<CustomerRepository>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// quartz
builder.Services.AddQuartz(opt =>
{
    Console.WriteLine("cron interval => " + builder.Configuration["CronJobs:Example"]!);
    opt.AddJob<ExampleJob>(JobKey.Create(nameof(ExampleJob)));
    opt.AddTrigger(trigger =>
    {
        trigger
            .ForJob(nameof(ExampleJob))
            .WithIdentity(nameof(ExampleJob))
            .WithCronSchedule(builder.Configuration["CronJobs:Example"]!)
            .StartNow();
    });
});

builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

var app = builder.Build();

app.UseExceptionHandler();

app.UseMiddleware<AuthMiddleware>();

app.MapControllers();

app.Run();
