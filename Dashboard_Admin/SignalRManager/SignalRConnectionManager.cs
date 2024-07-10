using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.AspNetCore.SignalR.Client;

namespace Dashboard_Admin.SignalRManager
{
    public class SignalRConnectionManager
    {
        private HubConnection _connection;

        public SignalRConnectionManager(string url)
        {
            var connectionUrl = url;

            _connection = new HubConnectionBuilder()
                .WithUrl(connectionUrl)
                .Build();

            _connection.Closed += async (error) =>
            {
                MessageBox.Show("Disconnected from SignalR hub. Attempting to reconnect...");
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await StartConnectionAsync();
            };
        }

        public async Task StartConnectionAsync()
        {
            if (_connection.State == HubConnectionState.Disconnected)
            {
                try
                {
                    await _connection.StartAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Connection failed: {ex.Message}");
                }
            }
        }

        public async void LoadOrder(string username)
        {
            try
            {
                if (_connection.State == HubConnectionState.Connected)
                {
                    await _connection.InvokeAsync("LoadOrder", username);
                }
                else
                {
                    MessageBox.Show("SignalR connection is not established.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to notify: {ex.Message}");
            }
        }
    }
}
