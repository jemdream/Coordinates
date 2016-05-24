using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using Windows.Devices.SerialCommunication;
using Coordinates.Services.Connection;

namespace Coordinates.Services
{
    public class SerialPortConnectionService : BaseConnectionService
    {
        public SerialPortConnectionService()
        {
            //var serialPort = new SerialDevice();
        }

        protected override async Task<bool> OnConnectingAsync()
        {
            // source of mocked data; 
            // TODO: create mocked source

            await Task.Delay(2000);

            CreateObservable()
                .SelectMany(async _ => await Disconnect())
                .Subscribe(b => { });

            return true;
        }

        protected override async Task<bool> OnDisconnectingAsync()
        {
            return await Task.FromResult(true);
        }
        /// <summary>
        /// Observable with 
        /// </summary>
        private static IObservable<IEnumerable<int>> CreateObservable()
        {
            return Observable.Create<IEnumerable<int>>(
                o =>
                {
                    return AsyncJob(new Random())
                        .ToObservable()
                        .Subscribe(o.OnNext);

                    //return Observable.FromAsync(AsyncJob(new Random()))
                    //.Subscribe();

                }).Publish().RefCount();
        }

        /// <summary>
        /// Getting Data from service
        /// </summary>
        private static async Task<IEnumerable<int>> AsyncJob(Random rand)
        {
            await Task.Delay(3000);

            return Enumerable.Repeat(rand.Next(), 1);
        }
    }
}
