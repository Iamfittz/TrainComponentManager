namespace Train_Management_App.Data {
    public class TrainComponent {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? UniqueNumber { get; set; }
        public bool CanAssignQuantity { get; set; }
        public int? QuantityAssignment { get; set; }
    }
}
