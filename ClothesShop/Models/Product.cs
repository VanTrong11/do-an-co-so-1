using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace ClothesShop.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Tên Sản Phẩm")]
        public string? Name { get; set; }
        [DisplayName("Mô Tả")]
        public string? Description { get; set; }
        [DisplayName("Giá Tiền")]
        [Column(Order = 3)]
        public decimal? Price { get; set; }

        [DisplayName("Giảm Giá")]
        [Column(Order = 3)]
        public decimal? Discount { get; set; }

        [DisplayName("Giới Tính")]
        [Column(TypeName = "bit")]
        public Boolean? Gender { get; set; }

        [DisplayName("Hình Ảnh")]
        public string? Image { get; set; }
        [DisplayName("Đánh Giá")]
        public int? Evaluate { get; set; }
        public int? CateId { get; set; }
        [ForeignKey("CateId")]
        public virtual Category Category { get; set; }
    }
}
