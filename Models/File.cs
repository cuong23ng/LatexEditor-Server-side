using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hustex_backend.Models
{
    [Table("File")]
    public class File
    {
        [Required]
        public string FileName { get; set; }

        public int ProjectId { get; set; }

        public Project Project { get; set; }

        [Required]
        public string DataType { get; set; }
    }
}