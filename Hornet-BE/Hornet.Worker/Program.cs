using Hornet.Api.Service;
using Quartz;


var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddScoped<ISpaceXService, SpaceXService>();
builder.Services.AddHttpClient();

builder.Services.AddQuartz(q =>
{
    var jobKey = new JobKey("SpaceXSyncJob");

    q.AddJob<SpaceXSyncJob>(opts => opts.WithIdentity(jobKey));

    q.AddTrigger(opts => opts
        .ForJob(jobKey)
        .WithIdentity("SpaceXSyncTrigger")
        .StartNow()
        .WithSimpleSchedule(x => x
            .WithInterval(TimeSpan.FromSeconds(10))
            .RepeatForever()
        )
    );
});

// Hosted service (IMPORTANT)
builder.Services.AddQuartzHostedService(opt =>
{
    opt.WaitForJobsToComplete = true;
});

var host = builder.Build();
host.Run();