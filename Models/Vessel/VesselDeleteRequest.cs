using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Vessel
{
    public class VesselDeleteRequest
    {
        [Required]
        public string VesselNumber { get; set; }
    }
}
