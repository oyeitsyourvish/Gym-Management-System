namespace GymManagementSystem.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }

        public int MemberId { get; set; }
        public Member Member { get; set; }

        public decimal Amount { get; set; }

        public DateTime PaymentDate { get; set; } = DateTime.Now;

        public string PaymentMethod { get; set; }
    }
}
