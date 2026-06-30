namespace CollaborativeCodingClient.Models.Packets.Room
{
    public class RoomMemberResponse
    {
        public int UserID { get; set; }
        public string Username { get; set; } = "";
        public string Role { get; set; } = "";
        public bool IsOnline { get; set; }
    }
}
