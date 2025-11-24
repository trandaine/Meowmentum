using System.Collections.ObjectModel;
using System.ComponentModel;

namespace EnglishApp.ApplicationCore.Entities
{
    public class Currency
    {
        public Currency()
        {
            CustomerPaymentRecords = new Collection<CustomerPaymentRecord>();
            //Currencies = new Collection<Currency>()
            Courses = new Collection<Course>();
        }
        public int Id { get; set; }
        [Description("Mã đồng tiền theo chuẩn ISO 4217")]
        public string Code { get; set; } = string.Empty;
        [Description("Tên đồng tiền theo chuẩn ISO 4217")]
        public string Name { get; set; } = string.Empty;
        [Description("Ký hiệu đồng tiền" +
                     "vd: $ cho đồng USD, € cho đồng EUR, ¥ cho đồng YEN")]
        public string? Symbol { get; set; }
        [Description("Dùng để biểu diẽn số thập phân của đồng tiền" +
                     "vd: USD, EUR (Thường hiển thị hai số thập phân phía sau)" +
                     "Đồng YEN của Nhật không có hiện thị hai số thập phân phía sau")]
        public int DecimalPlaces { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public int Position { get; set; }

        #region Navigational Property

        public virtual ICollection<CustomerPaymentRecord> CustomerPaymentRecords { get; set; }
        //public virtual ICollection<Currency> Currencies { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
        #endregion
    }
}
