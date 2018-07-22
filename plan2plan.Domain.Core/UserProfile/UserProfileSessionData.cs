using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace plan2plan.Domain.Core.UserProfile
{
    public class UserProfileSessionData //: User
    {
        public Guid ID { get; set; }
        public int EmailID { get; set; }
        public int UserTypeID { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public DateTime dateTime { get; set; }
        public string SurName { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public string Address { get; set; }

        public virtual Email Email { get; set; }
        public virtual UserType UserType { get; set; }
        public UserProfileSessionData()
        {

        }
        public UserProfileSessionData(User user)
        {
            this.ID = user.ID;
            this.Password = user.Password;
            this.Name = user.Name;
            this.EmailID = user.EmailID;
            this.Email = user.Email;
            this.dateTime = user.dateTime;
            this.UserType = user.UserType;
            this.UserTypeID = user.UserTypeID;

            this.SurName = user.SurName;
            this.Phone = user.Phone;
            this.City = user.City;
            this.Address = user.Address;
        }
    }
}
