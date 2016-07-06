using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPIDemo.Models
{
    public class Coffee
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Volume { get; set; }
       
        [ForeignKey("Company")]
        public int CompanyID { get; set; }
        public Company Company { get; set; }
    }

    public class Company
    {
        [Key]
        public int Id { get; set; }
        public string CompanyName { get; set; }
    }
}