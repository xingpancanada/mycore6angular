using System.ComponentModel.DataAnnotations;

namespace Backend.Entities
{
    public class Address
    {
        public int Id { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string Street { get; set; }
        
        public string City { get; set; }
        
        public string State { get; set; }
        
        public string ZipCode { get; set; }
        //public string Country { get; set; }
        
        [Required]
        public string AppUserId { get; set; }   ////one to one relationship
        
        public AppUser AppUser { get; set; }   ////one to one relationship
    }
}