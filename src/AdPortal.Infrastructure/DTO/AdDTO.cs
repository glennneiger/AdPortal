using System;
using System.ComponentModel.DataAnnotations;

namespace AdPortal.Infrastructure.DTO
{
    public class AdDTO
    {
        public Guid Id{get; set;}
       

        [Required]
        [MinLength(5)]
        [MaxLength(25)]
        public string Name {get;set;}

        [Required]
        [MinLength(50)]
        public string Content {get;set;}
        
         [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime AddDate {get; set;}

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime ExpiryDate {get; set;} 
        public string UserId {get; set;}
        public string UserName {get;set;}
        public CategoryDTO Category{get;set;}
        public Guid CategoryId{get;set;}
    }
}