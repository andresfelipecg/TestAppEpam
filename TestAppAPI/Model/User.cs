using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TestAppAPI.Model;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UserId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }

    public ICollection<StudyGroupUser> StudyGroupUsers { get; set; } = new HashSet<StudyGroupUser>();
}