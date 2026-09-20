using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SpendTracerApi.Models
{
    public class SpendingModels
    {
        [Key]
        public int SpendingId { get; set; }
        public string SpendingName { get; set; }
        public string SpendingDescription { get; set;}
        public double SpendingValue { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateTime { get; set; }
    }
}
