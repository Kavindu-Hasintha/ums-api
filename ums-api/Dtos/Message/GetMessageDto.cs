namespace ums_api.Dtos.Message
{
    public class GetMessageDto
    {
        public long Id { get; set; }

        public string SenderUsername { get; set; }

        public string ReceiverUsername { get; set; }

        public string Text { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
