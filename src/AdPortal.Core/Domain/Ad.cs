using System;
using System.Collections.Generic;

namespace AdPortal.Core.Domain
{
    public enum Status
    {
        WaitingForAccept = 0,
        Accepted = 1
    }
    public class Ad
    {
        public Ad(Guid userId, Category category, string name, string content, DateTime expiryDate, Status status)
        {
            this.Id = Guid.NewGuid();
            this.AddDate = DateTime.Now;
            this.Name = name;
            this.Content = content;
            this.ExpiryDate = expiryDate;
            this.UserID = userId;
            this.Category = category;
            this.Status = status;
        }

        public Guid Id { get; set; }
        public Guid UserID {get;set;}
        public string UserName {get;set;}
        public Category Category {get;set;}
        public string Name { get; set; }
        public string Content { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public Status Status {get; set;}
        public void EditAd(string name, string content, Category category)
        {
            Name = name;
            Content = content;
            Category = category;
        }
    }
}