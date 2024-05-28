using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libs.Entity
{
    public class ApplicationUser : IdentityUser
    {
        public string? Name { get; set; }
        public string? Avt { get; set; }
        public string? Gender { get; set; }
        public string? Certification { get; set; }
        public int? CategoryId { get; set; }
        public int? TopicId { get; set; }
        public string? University { get; set; }
        public string? Major { get; set; }
        public string? Address { get; set; }
        public int? FileId { get; set; }
        public bool IsLock { get; set; }
        [NotMapped]
        public IFormFile CertificationFile { get; set; }
        [NotMapped]
        public IFormFile AvatarFile { get; set; }
        [ForeignKey(nameof(TopicId))]
        public Topic Topic { get; set; }
        [ForeignKey(nameof(FileId))]
        public Field Field { get; set; }
    }
}
