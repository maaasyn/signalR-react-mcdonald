namespace signalR_api.Models
{
    public class Device
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool CanSendMessages { get; set; }

        public readonly int Display = 0;
        public readonly int Pos = 1;
        public readonly int Kitchen = 2;
    }
}