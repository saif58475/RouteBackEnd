namespace ROUTEAPI.Models
{
    public class City
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name{ get; set; }
        [Required]
        public int CountryId{ get; set; }
        public Country Country{ get; set; }

        public ICollection<District> Districts{ get; set; }
    }
}
