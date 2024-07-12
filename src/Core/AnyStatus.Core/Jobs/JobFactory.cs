using System;
using Quartz;
using Quartz.Spi;

namespace AnyStatus.Core.Jobs;

public class JobFactory : IJobFactory
{
    private readonly IServiceProvider _serviceProvider;

    public JobFactory(IServiceProvider serviceProvider) { _serviceProvider = serviceProvider; }

    public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler) => (IJob)_serviceProvider.GetService(typeof(IJob));

    public void ReturnJob(IJob job) { }
}