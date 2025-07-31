using System.ComponentModel.DataAnnotations;

namespace Train_Management_App.Data {
    public class TrainComponent {
        public int Id { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string? Name { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 6)]
        public string? UniqueNumber { get; set; }
        public bool CanAssignQuantity { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be > 0")]
        public int? QuantityAssignment { get; set; }
    }
}
