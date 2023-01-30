using System.ComponentModel.DataAnnotations;

namespace WebApi.Entities
{
    public class Vessel
    {
        [Key]
        public string VesselNumber { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int YearOfConstruction { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public int NetWeight { get; set; }
        public int GrossWeight { get; set; }
        public string Info { get; set; } = string.Empty;
    }
}
