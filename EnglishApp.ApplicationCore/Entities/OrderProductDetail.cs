using EnglishApp.ApplicationCore.Enums;
using System.ComponentModel;

namespace EnglishApp.ApplicationCore.Entities
{
    public class OrderProductDetail
    {

        [Description("Mua sản phẩm nào – Id của sản phẩm được mua (tùy theo dự án, nó có thể là SongId, VideoId")]
        public int CourseId { get; set; }
        public Course Cousre { get; set; }

        [Description("Thuộc về Đơn hàng nào")]
        public int OrderId { get; set; }
        public Order Order { get; set; }

        [Description("Giá thành 1 sản phẩm")]
        public decimal CoursePrice { get; set; }

        [Description("Tổng giá trị chi tiết của đơn hàng")]
        public decimal? PriceDiscounted { get; set; }

        [Description("Trạng thái của từng đơn hàng")]
        public OrderStatusEnum Status { get; set; }

        [Description("Thứ tự trong 1 đơn hàng tổng")]
        public int Position { get; set; }

    }
}
