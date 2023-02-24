using System.Text.Json.Serialization;

namespace Feb22Api
{
    public class Marks
    {
        public int StudentId { get; set; }

        public int SubjectId { get; set; }

        public int marks { get; set; }

      
        public virtual Student Student { get; set; }

        
        public virtual Subject Subject { get; set; }


    }
}
