using EnglishApp.ApplicationCore.Enums;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace EnglishApp.ApplicationCore.Entities
{
    public class Course
    {
        public Course()
        {
            Lessons = new Collection<Lesson>();
            CourseComments = new Collection<CourseComment>();
            CustomerPaymentRecords = new Collection<CustomerPaymentRecord>();
            CourseCustomerDetails = new Collection<CourseCustomerDetail>();
            OrderProductDetails = new Collection<OrderProductDetail>();
            CartItems = new Collection<CartItem>();
        }
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public LevelEnum Level { get; set; }
        [Description("Dung de luu tru ten file hinh anh hoac video hien thi tren khoa hoc")]
        public string? Thumbnail { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public int Position { get; set; }
        public int? CurrencyId { get; set; }
        public virtual Currency Currency { get; set; }

        #region Navigational Property
        //public int CourseCommentId { get; set; }
        //public virtual CourseComment CourseComment { get; set; }
        //public int CustomerPaymentRecordId { get; set; }
        //public virtual CustomerPaymentRecord CustomerPaymentRecord { get; set; }
        public virtual ICollection<Lesson> Lessons { get; set; }
        public virtual ICollection<CourseComment> CourseComments { get; set; }
        public virtual ICollection<CustomerPaymentRecord> CustomerPaymentRecords { get; set; }
        public virtual ICollection<CourseCustomerDetail> CourseCustomerDetails { get; set; }
        public virtual ICollection<OrderProductDetail> OrderProductDetails { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }

        #endregion
    }
}
