using EnglishApp.ApplicationCore.Enums;
using System.Collections.ObjectModel;

namespace EnglishApp.ApplicationCore.Entities
{
    public class Customer
    {
        public Customer()
        {
            CourseComments = new Collection<CourseComment>();
            CustomerPaymentRecords = new Collection<CustomerPaymentRecord>();
            CourseCustomerDetails = new Collection<CourseCustomerDetail>();
            CustomerProgresses = new Collection<CustomerProgress>();
            ShoppingCarts = new Collection<ShoppingCart>();
        }
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? LastLogin { get; set; }
        public string? UserId { get; set; }
        public string? Avartar { get; set; }
        public GenderEnum Gender { get; set; }
        public LevelEnum Level { get; set; }
        public string? Description { get; set; }

        #region Navigational Property

        public virtual ICollection<CourseComment> CourseComments { get; set; }
        public virtual ICollection<CustomerPaymentRecord> CustomerPaymentRecords { get; set; }

        public virtual ICollection<CourseCustomerDetail> CourseCustomerDetails { get; set; }
        public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; }

        public virtual ICollection<CustomerProgress> CustomerProgresses { get; set; }
        //public virtual ICollection<CustomerPaymentRecord> CustomerPaymentRecords { get; set; }
        #endregion

    }
}
