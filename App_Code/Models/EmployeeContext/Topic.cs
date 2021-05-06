using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cryptoexch.Models
{
    [Table("Topic")]
    public partial class Topic
    {
        public int ID { get; set; }

        [Required]
        [StringLength(128)]
        public string CreatedBy { get; set; }

        public DateTimeOffset CreatedTimestamp { get; set; }

        [Required]
        [StringLength(50)]
        public string Category { get; set; }

        [Required]
        [StringLength(30)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [StringLength(128)]
        public string EditedBy { get; set; }

        public DateTimeOffset? EditedTimestamp { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Employee Employee1 { get; set; }
    }
}