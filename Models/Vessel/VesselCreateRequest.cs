using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Vessel
{
    public class VesselCreateRequest
    {
        [Required]
        public string VesselNumber { get; set; }
        [Required]
        public string Name { get; set; }
        public int YearOfConstruction { get; set; }
        [Required]
        public double Length { get; set; }
        [Required]
        public double Width { get; set; }
        public int GrossWeight { get; set; }
        public int NetWeight { get; set; }
        [Required]
        public string Info { get; set; }
    }
    


}
