using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

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

        [ForeignKey("UserId")]
        public User user { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime LastModified { get; set; }
    }
}