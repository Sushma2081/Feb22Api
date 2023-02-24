using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Feb22Api
{
    public class Subject
    {
        public Subject()
        {
            Marks = new HashSet<Marks>();

        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int SubjecttId { get; set; }
        public string? SubjectName { get; set; }

        [JsonIgnore]
        public virtual ICollection<Marks> Marks { get; set; }
    }
}
