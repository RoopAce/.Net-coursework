using System;
using System.ComponentModel.DataAnnotations;

namespace MauiApptt.Data
{
    public class StockItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Please provide the item name.")]
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public string ApprovedBy { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string TakenBy { get; set; }
    }
}
