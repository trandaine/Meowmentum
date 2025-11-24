namespace EnglishApp.BusinessLogic.DTOs.ShoppingCarts
{
    public class ShoppingCartDTO
    {
        public ShoppingCartDTO(string userId)
        {
            UserId = userId;
        }
        public string UserId { get; set; } = string.Empty;
        public List<CartItemDTO> Items { get; set; } = new();
        public int TotalQuantity { get; set; }
        public decimal TotalAmount { get; set; }


        /// <summary>
        /// Thêm món hàng vào giỏ hàng CartItemDTO. Mỗi lần hàm này gọi thì sẽ +1 số lượng món hàng vào giỏ hàng
        /// </summary>
        /// <param name="productId">Id của sản phầm</param>
        /// <param name="name">Tên của sản phẩm</param>
        /// <param name="price">giá tiền của sản phẩm</param>
        /// <param name="quantity">số lượng sản phẩm mua (Mặc định là 1)</param>
        /// <param name="image">hình ảnh sản phẩm</param>
        public void AddItem(int productId, string name, decimal price, int quantity = 1, string image = "")
        {
            var existedProduct = Items.SingleOrDefault(i => i.Id == productId);
            if (existedProduct != null)
                existedProduct.Quantity += quantity;
            else
                Items.Add(new CartItemDTO { Id = productId, Name = name, Price = price, Quantity = quantity, Image = image });
            TotalQuantity += quantity;
        }


        /// <summary>
        /// Cập nhật số lượng món hàng trong giỏ hàng để hiển thị số lượng hàng mua
        /// </summary>
        /// <param name="productId">Id của sản phầm</param>
        /// <param name="newQuantity">Giá trị số lượng mới của sản phẩm được thêm vào (+1)</param>
        public void UpdateQuantity(int productId, int newQuantity)
        {
            var existedProduct = Items.SingleOrDefault(i => i.Id == productId);
            if (existedProduct != null)
            {
                if (newQuantity <= 0)
                    Items.Remove(existedProduct);
                else
                    existedProduct.Quantity = newQuantity;
                TotalQuantity += newQuantity;
            }
        }


        /// <summary>
        /// Xóa món hàng khỏi giỏ hàng
        /// </summary>
        /// <param name="productId">Id của sản phẩm cần xóa</param>
        public void RemoveItem(int productId)
        {
            Items.RemoveAll(i => i.Id == productId);
        }



        /// <summary>
        /// Bỏ luôn tất cả món hàng trong giỏ hàng
        /// </summary>
        public void Clear() => Items.Clear();
    }
}
