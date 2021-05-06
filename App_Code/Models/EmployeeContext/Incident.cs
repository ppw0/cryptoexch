using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cryptoexch.Models
{
    [Table("Incident")]
    public partial class Incident
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        public DateTimeOffset Timestamp { get; set; }

        [Required]
        [StringLength(30)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        [StringLength(8)]
        public string Status { get; set; }

        [StringLength(128)]
        public string AssignedTo { get; set; }

        public virtual Employee Employee { get; set; }
    }
}