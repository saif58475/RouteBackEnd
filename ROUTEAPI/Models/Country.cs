namespace ROUTEAPI.Models
{
    public class Country
    {
        [Key]
        public int Id{ get; set; }
        [Required]
        [MaxLength(50)]
        public string Name{ get; set; }
        [Required]
        public int PhoneCode{ get; set; }
        [Required]
        public string Image{ get; set; }

        public ICollection<City> Cities{ get; set; }

    }
}
