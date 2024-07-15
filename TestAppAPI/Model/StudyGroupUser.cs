using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestAppAPI.Model
{
    public class StudyGroupUser
    {
        [Key]
        [ForeignKey("StudyGroup")]
        public int StudyGroupId { get; set; }
        public StudyGroup StudyGroup { get; set; }

        [Key]
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
