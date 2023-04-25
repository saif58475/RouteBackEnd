namespace ROUTEAPI.Dtos
{
    public class ReturnCountryDto
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public int PhoneCode { get; set; }
        public string Image { get; set; }
        public ICollection<City> Cities { get; set; }
    }

   
}
