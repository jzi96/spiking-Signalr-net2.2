using Microsoft.AspNetCore.SignalR;
using spiking.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace spiking.Hubs
{
    public class PositionsHub : Microsoft.AspNetCore.SignalR.Hub<IPositionHubClient>
    {
        private Random rnd = new Random();
        private System.Threading.Timer timer;

        public IHubContext<PositionsHub> CustomContext { get; }

        //static System.Collections.Concurrent.ConcurrentDictionary<string, HubCallerContext> contextCache = new System.Collections.Concurrent.ConcurrentDictionary<string, HubCallerContext>();
        private void UpdatePosition(object state)
        {
            UpdatePosition();
        }

        static PositionsHub()
        {
        }
        public PositionsHub(IHubContext<PositionsHub> context)
        {
            this.CustomContext = context;
            timer = new Timer(UpdatePosition, null, 2000, 50);
        }
        public override Task OnConnectedAsync()
        {
            //contextCache.TryAdd(this.Context.ConnectionId, this.Context);
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            //HubCallerContext ctx;
            //contextCache.Remove(this.Context.ConnectionId, out ctx);
            timer.Change(Timeout.Infinite, Timeout.Infinite);
            timer.Dispose();
            return base.OnDisconnectedAsync(exception);
        }
        public async Task UpdatePosition()
        {
            //if (CustomContext.Count == 0)
            //    return;
            DateTime offset = DateTime.UtcNow;
            offset = new DateTime(offset.Year, offset.Month, offset.Day, offset.Hour, offset.Minute - (offset.Minute % 15), 0, DateTimeKind.Utc);
            int cnt = 0;
            var pos = new Position()
            {
                Grid = "Test",
                Values = new KeyValuePair<DateTime, double>[]{
                new KeyValuePair<DateTime, double> ( offset.AddMinutes(15*cnt++), rnd.Next(-30000, 50000) )
                , new KeyValuePair<DateTime, double> ( offset.AddMinutes(15*cnt++), rnd.Next(-30000, 50000) )
                , new KeyValuePair<DateTime, double> ( offset.AddMinutes(15*cnt++), rnd.Next(-30000, 50000) )
                , new KeyValuePair<DateTime, double> ( offset.AddMinutes(15*cnt++), rnd.Next(-30000, 50000) )
                , new KeyValuePair<DateTime, double> ( offset.AddMinutes(15*cnt++), rnd.Next(-30000, 50000) )
                , new KeyValuePair<DateTime, double> ( offset.AddMinutes(15*cnt++), rnd.Next(-30000, 50000) )
                , new KeyValuePair<DateTime, double> ( offset.AddMinutes(15*cnt++), rnd.Next(-30000, 50000) )
                , new KeyValuePair<DateTime, double> ( offset.AddMinutes(15*cnt++), rnd.Next(-30000, 50000) )
                , new KeyValuePair<DateTime, double> ( offset.AddMinutes(15*cnt++), rnd.Next(-30000, 50000) )
                , new KeyValuePair<DateTime, double> ( offset.AddMinutes(15*cnt++), rnd.Next(-30000, 50000) )
                , new KeyValuePair<DateTime, double> ( offset.AddMinutes(15*cnt++), rnd.Next(-30000, 50000) )
                , new KeyValuePair<DateTime, double> ( offset.AddMinutes(15*cnt++), rnd.Next(-30000, 50000) )
                , new KeyValuePair<DateTime, double> ( offset.AddMinutes(15*cnt++), rnd.Next(-30000, 50000) )
                , new KeyValuePair<DateTime, double> ( offset.AddMinutes(15*cnt++), rnd.Next(-30000, 50000) )
                , new KeyValuePair<DateTime, double> ( offset.AddMinutes(15*cnt++), rnd.Next(-30000, 50000) )
                , new KeyValuePair<DateTime, double> ( offset.AddMinutes(15*cnt++), rnd.Next(-30000, 50000) )
                , new KeyValuePair<DateTime, double> ( offset.AddMinutes(15*cnt++), rnd.Next(-30000, 50000) )
                , new KeyValuePair<DateTime, double> ( offset.AddMinutes(15*cnt++), rnd.Next(-30000, 50000) )
                , new KeyValuePair<DateTime, double> ( offset.AddMinutes(15*cnt++), rnd.Next(-30000, 50000) )
                , new KeyValuePair<DateTime, double> ( offset.AddMinutes(15*cnt++), rnd.Next(-30000, 50000) )
                , new KeyValuePair<DateTime, double> ( offset.AddMinutes(15*cnt++), rnd.Next(-30000, 50000) )
            }
            };
            try
            {
                await CustomContext.Clients.All.SendAsync(nameof(IPositionHubClient.OnPositionUpdated), pos);
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }

    public interface IPositionHubClient
    {
        Task OnPositionUpdated(Position gridPos);
    }
}