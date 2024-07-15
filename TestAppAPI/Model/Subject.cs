using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestAppAPI.Model
{
    public class Subject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SubjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } // Optional, if descriptions are needed

        //1.	Users are able to create only one Study Group for a single Subject
        //public ICollection<StudyGroup> StudyGroups { get; set; } // Navigation property for StudyGroups belonging to this subject
    }
}
