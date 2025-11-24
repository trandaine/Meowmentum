namespace EnglishApp.ApplicationCore.Entities
{
    public class CourseCustomerDetail
    {
        public CourseCustomerDetail()
        {

        }
        //public int Id { get; set; }
        public int CustomerId { get; set; }
        public decimal Amount { get; set; }
        public virtual Customer Customer { get; set; }
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }
        public DateTime DateCreated { get; set; }
        public int Position { get; set; }
    }
}
