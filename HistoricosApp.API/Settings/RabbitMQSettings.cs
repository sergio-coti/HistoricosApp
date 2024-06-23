namespace HistoricosApp.API.Settings
{
    public class RabbitMQSettings
    {
        public string? Hostname { get; set; }
        public int Port { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Queue { get; set; }
    }
}
