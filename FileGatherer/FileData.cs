using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FileGatherer
{
    public class FileData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; } 
        public string Path { get; set; }
        public string FileName { get; set; }
        public DateTime CreatedDate { get; set; }

        public FileData()
        {
            CreatedDate = DateTime.Now;
        }
    }
}
