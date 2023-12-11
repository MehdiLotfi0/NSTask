using System.ComponentModel.DataAnnotations;

namespace NSTask.Models
{
    public partial class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string Name { get; set; }
        public DateTime? ProductDate { get; set; }
        public string? ManufacturePhone { get; set; }
        public string? ManufactureEmail { get; set; }
        public string? IsAvailable { get; set; }
        public virtual ICollection<Login> Logins { get; set; } = new List<Login>();

    }
}
