using System;

namespace ClothesShop.Helpers
{
    public class PagingModel1
    {
        public int currentpage { get; set; }
        public int countpage { get; set; }
        public Func<int?, string> generetUrl { get; set; }
    }
}
