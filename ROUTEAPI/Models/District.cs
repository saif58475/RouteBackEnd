namespace ROUTEAPI.Models
{
    public class District
    {
        [Key]
        public int Id{ get; set; }
        [Required]
        [MaxLength(50)]
        public string Name{ get; set; }
        [Required]
        public int CityId{ get; set; }
        public City City{ get; set; }

        public ICollection<Block> Blocks{ get; set; }
    }
}
