namespace GymManagementSystem.Models
{
    public class Attendance
    {
        public int AttendanceId { get; set; }
        public int MemberId { get; set; }
        public Member Member { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public DateTime CheckInTime { get; set; } = DateTime.Now;
    }
}
