namespace ROUTEAPI.Dtos
{
    public class CitizenDto
    {
        public string Name{ get; set; }
        public DateTime DOB{ get; set; }
        public IFormFile Image{ get; set; }
        public int CityId{ get; set; }
    }
}
