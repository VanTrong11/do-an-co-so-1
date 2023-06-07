using System;

namespace ClothesShop.Helpers
{
    public class PagingModel
    {
        public int currentpage { get; set; }
        public int countpage { get; set; }
        public Func<int?,string? , string? ,string> generetUrl { get; set; }
    }
}
