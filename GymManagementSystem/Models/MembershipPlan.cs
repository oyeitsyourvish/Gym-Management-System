using System.ComponentModel.DataAnnotations;

namespace GymManagementSystem.Models
{
    public class MembershipPlan
    {
        public int MembershipPlanId { get; set; }
        [Required]
        public string PlanName { get; set; }
        public int DurationInMonths { get; set; }
        public decimal Price { get; set; }
        public ICollection<Member> Members { get; set; }
    }
}
