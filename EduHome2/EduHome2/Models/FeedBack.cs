using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace EduHome2.Models
{
    public class FeedBack
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
        public string Position { get; set; }
        public bool IsDeactive { get; set; }

        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
