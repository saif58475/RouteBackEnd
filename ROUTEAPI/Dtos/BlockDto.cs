namespace ROUTEAPI.Dtos
{
    public class BlockDto
    {
        [Required]
        public string Name{ get; set; }
        [Required]
        public int DistrictId{ get; set; }
    }
}
