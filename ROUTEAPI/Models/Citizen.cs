namespace ROUTEAPI.Models
{
    public class Citizen
    {
        [Key]
        public int Id{ get; set; }
        [Required]
        [MaxLength(50)]
        public string Name{ get; set; }
        [Required]
        public DateTime Age{ get; set; }
        [Required]
        public string Image{ get; set; }
        [Required]
        public int CityId{ get; set; }
        [Required]
        public City City{ get; set; }
    }
}
