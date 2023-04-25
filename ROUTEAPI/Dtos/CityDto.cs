namespace ROUTEAPI.Dtos
{
    public class CityDto
    {
        [Required]
        public string Name{ get; set; }
        [Required]
        public int CountryId{ get; set; }

    }
}
