using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymManagementSystem.Models
{
    public class Member
    {
        public int MemberId { get; set; }
        [Required]
        public string FullName { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime JoinDate { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;
        public DateTime ExpiryDate { get; set; }
        [NotMapped]
        public bool IsExpired => DateTime.Now > ExpiryDate;



        // Foreign Keys
        public int MembershipPlanId { get; set; }
        public MembershipPlan? MembershipPlan { get; set; }

        public int TrainerId { get; set; }
        public Trainer? Trainer { get; set; }


        // Navigation
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
        public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

    }
}
