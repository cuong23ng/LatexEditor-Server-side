using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hustex_backend.Models
{
    [Table("Project")]
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }

        [Required]
        [StringLength(20)]
        public string ProjectName { get; set; }

        public List<File> Files { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime LastModified { get; set; }
    }
}