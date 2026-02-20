using System.ComponentModel.DataAnnotations;

namespace GymManagementSystem.Models
{
    public class Trainer
    {
        public int TrainerId { get; set; }
        [Required]
        public string FullName { get; set; }
        public string Specialization { get; set; }
        public string PhoneNumber { get; set; }
        public decimal Salary { get; set; }
        public ICollection<Member> Members { get; set; } = new List<Member>();


    }
}
