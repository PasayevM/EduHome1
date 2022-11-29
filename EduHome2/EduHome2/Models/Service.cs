using System.ComponentModel.DataAnnotations;

namespace EduHome2.Models
{
    public class Service
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Doldurulmasi Vacibdir")]
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsDeactive { get; set; }
    }
}
