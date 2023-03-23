using Microsoft.AspNetCore.Identity;

namespace ComputerExam.Models
{
    public class Employee : IdentityUser
    {
        public int TeacherId { get; set; }
    }
}
