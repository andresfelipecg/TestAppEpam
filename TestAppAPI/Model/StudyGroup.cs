using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using TestAppAPI.Model;




public class StudyGroup
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int StudyGroupId { get; set; }

    [Required]
    [StringLength(30, MinimumLength = 5)]
    public string Name { get; set; }

    public DateTime CreateDate { get; set; }

    [Required]
    public int SubjectId { get; set; }

    [Required]
    public int UserId { get; set; }

    // Navigation property to Subject
    [ForeignKey("SubjectId")]
    public Subject Subject { get; set; }

    [JsonIgnore]
    public ICollection<StudyGroupUser> StudyGroupUsers { get; set; } = new HashSet<StudyGroupUser>();
}