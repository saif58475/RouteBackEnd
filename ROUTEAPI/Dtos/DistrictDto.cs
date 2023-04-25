namespace ROUTEAPI.Dtos
{
    public class DistrictDto
    {
        [Required]
        public string Name{ get; set; }
        [Required]
        public int CityId{ get; set; }
    }
}
