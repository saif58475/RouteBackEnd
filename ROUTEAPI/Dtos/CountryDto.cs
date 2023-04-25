namespace ROUTEAPI.Dtos
{
    public class CountryDto
    {
        [Required]
        public string Name{ get; set; }
        [Required]
        public int PhoneCode{ get; set; }
        [Required]
        public IFormFile Image{ get; set; }
        
    }
}
