global using System.ComponentModel.DataAnnotations;

namespace ROUTEAPI.Dtos
{
    public class UserDto
    {
        [MaxLength(50)]
        public string Name{ get; set; }
        [MaxLength(10)]
        public string Password{ get; set; }
    }
}
