using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hustex_backend.Models
{
    [Table("File")]
    public class File
    {
        [Key]
        public int FileId { get; set; }

        [Required]
        [StringLength(20)]
        public string FileName { get; set; }

        public int ProjectId { get; set; }

        [ForeignKey("ProjectId")]
        public Project Project { get; set; }

        [Required]
        public string DataType { get; set; }
    }
}