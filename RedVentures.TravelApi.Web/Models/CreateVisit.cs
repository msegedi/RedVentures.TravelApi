using System.ComponentModel.DataAnnotations;

namespace RedVentures.TravelApi.Web.Models
{
    public class CreateVisit
    {
        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }
    }
}
