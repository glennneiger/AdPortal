using System;

namespace AdPortal.Core.Domain
{
    public class Ad_Category
    {
        public Guid Id {get; set;}
        public Guid CategotyId {get; set;}
        public Guid AdId {get ; set;}
        public Category Category {get;set;}
        public Ad Ad {get;set;}
    }
}