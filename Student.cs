using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Feb22Api;

public class Student
{
    public Student() 
    {
        Marks = new HashSet<Marks>();

    }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int StudentId { get; set; }
    [Required]
    public string? StudentName { get; set; }

    [JsonIgnore]
    public virtual ICollection<Marks> Marks { get; set; }
}
