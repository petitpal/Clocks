using Clocks.App.Button;
using Clocks.App.ClockRunner;
using Clocks.App.Factories;
using Clocks.Classes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Clocks.App
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Displaying current time. Press Ctrl+C to end.");

            bool runningOnPi = true;

            using var host = CreateHostBuilder(args, runningOnPi).Build();
            using var serviceScope = host.Services.CreateScope();

            var clockRunner = serviceScope.ServiceProvider.GetService<IClockController>();
            clockRunner.Clock1 = new StandardTime();
            clockRunner.Clock2 = new MetricTime();

            clockRunner.RunClock();
            //var clock = Task.Factory.StartNew(() => _clockRunner.RunClockAsync());
            //clock.Wait(Timeout.Infinite);
        }

        static IHostBuilder CreateHostBuilder(string[] args, bool runningOnPi)
        {
            var host = Host.CreateDefaultBuilder(args);

            // default
            host.ConfigureServices((_, services) =>
                    services.AddSingleton<IControllerFactory, PiControllerFactory>()
                            .AddTransient<IClockController, ClockController>()
            );

            // emulation
            if (runningOnPi) {
                host.ConfigureServices((_, services) =>
                    services.AddSingleton<IDeviceFactory, PiDeviceFactory>()
                );
            }
            else
            {
                host.ConfigureServices((_, services) =>
                    services.AddSingleton<IDeviceFactory, ConsoleDeviceFactory>()
                );
            }

            return host;
        }

    }
}