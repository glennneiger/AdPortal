using System;
using System.Collections.Generic;
using System.Linq;

namespace AdPortal.Core.Domain
{
    public class User
    {


        public User (Guid id, string email, string userName, string password,string salt, string role)
        {
            this.Salt = salt;
            this.Email = email;
            this.UserName = userName;
            this.Password = password;
            this.Id =  id;
            this.Role = role;
        }
        protected User()
        {}
        public Guid Id { get; set; }
        public string Salt { get; set; }
        public string Email { get; set; }
        public string Password {get; set;}
        public string UserName { get; set; }
        public string Role {get; set;}
        public DateTime BirthDate { get; set; }
        private ISet<Ad> _Ads = new HashSet<Ad>();
        public IEnumerable<Ad> Ads
        {
            get{return _Ads;}
            set{_Ads = new HashSet<Ad>(value);}
        }
        public void AddAd(Ad ad)
        {
            ad.UserName = this.UserName;
            _Ads.Add(ad);
        }
        public void RemoveAd(Guid adId)
        {
            var ad = Ads.SingleOrDefault(x=>x.Id == adId);
            if(ad == null)
            {
                throw new Exception("Selected ad does not exist.");
            }
            _Ads.Remove(ad);
        }


        public void Edit(string email, string username)
        {
            Email = email;
            UserName = username;
        }

        public void ChangePassword(string password)
        {
            Password = password;
        }

        public Ad GetAd(Guid adId)
        {
            var ad = _Ads.SingleOrDefault(x=>x.Id == adId);
            if(ad == null)
            {
                throw new Exception("Can not find selected ad.");
            }
            return ad;
        }
    }
}