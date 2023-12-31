﻿using Cit.Apps.Licensing.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cit.Apps.Licensing.Domain.Common
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public int ModifiedBy { get; set; }

        public DateTime ModifiedAt { get; set; }

        [ForeignKey("CreatedBy")]
        public virtual ICollection<User> CreatedByUser { get; set; }

        [ForeignKey("ModifiedBy")]
        public virtual ICollection<User> ModifiedByUser { get; set; }
    }
}
