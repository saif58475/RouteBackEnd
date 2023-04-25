namespace ROUTEAPI.Models
{
    public class Block
    {
        [Key]
        public int Id{ get; set; }
        [Required]
        [MaxLength(50)]
        public string Name{ get; set; }
        [Required]
        public int districtId{ get; set; }
        public District District{ get; set; }
    }
}
