namespace Train_Management_App.Data {
    public static  class InitialData {
        public static  void Seed(AppDbContext db) {
            if (db.TrainComponents.Any()) return;   

            var items = new[] {
            new TrainComponent { Name = "Engine",        UniqueNumber = "ENG123", CanAssignQuantity = false },
            new TrainComponent { Name = "Passenger Car", UniqueNumber = "PAS456", CanAssignQuantity = false },
            new TrainComponent { Name = "Freight Car",   UniqueNumber = "FRT789", CanAssignQuantity = false },
            new TrainComponent { Name = "Wheel",         UniqueNumber = "WHL101", CanAssignQuantity = true,  QuantityAssignment = 100 },
            new TrainComponent { Name = "Seat",          UniqueNumber = "STS234", CanAssignQuantity = true,  QuantityAssignment = 200 },
            new TrainComponent { Name = "Window",        UniqueNumber = "WIN567", CanAssignQuantity = true,  QuantityAssignment = 150 },
            new TrainComponent { Name = "Door",          UniqueNumber = "DR123",  CanAssignQuantity = true,  QuantityAssignment = 50  },
            new TrainComponent { Name = "Control Panel", UniqueNumber = "CTL987", CanAssignQuantity = true,  QuantityAssignment = 25  },
            new TrainComponent { Name = "Light",         UniqueNumber = "LGT456", CanAssignQuantity = true,  QuantityAssignment = 300 },
            new TrainComponent { Name = "Brake",         UniqueNumber = "BRK789", CanAssignQuantity = true,  QuantityAssignment = 75  },
            new TrainComponent { Name = "Bolt",          UniqueNumber = "BLT321", CanAssignQuantity = true,  QuantityAssignment = 1000 },
            new TrainComponent { Name = "Nut",           UniqueNumber = "NUT654", CanAssignQuantity = true,  QuantityAssignment = 1000 },
            new TrainComponent { Name = "Engine Hood",   UniqueNumber = "EH789",  CanAssignQuantity = false },
            new TrainComponent { Name = "Axle",          UniqueNumber = "AX456",  CanAssignQuantity = false },
            new TrainComponent { Name = "Piston",        UniqueNumber = "PST789", CanAssignQuantity = false },
            new TrainComponent { Name = "Handrail",      UniqueNumber = "HND234", CanAssignQuantity = true,  QuantityAssignment = 120 },
            new TrainComponent { Name = "Step",          UniqueNumber = "STP567", CanAssignQuantity = true,  QuantityAssignment = 60  },
            new TrainComponent { Name = "Roof",          UniqueNumber = "RF123",  CanAssignQuantity = false },
            new TrainComponent { Name = "Air Conditioner",UniqueNumber = "AC789", CanAssignQuantity = false },
            new TrainComponent { Name = "Flooring",      UniqueNumber = "FLR456", CanAssignQuantity = false },
            new TrainComponent { Name = "Mirror",        UniqueNumber = "MRR789", CanAssignQuantity = true,  QuantityAssignment = 40  },
            new TrainComponent { Name = "Horn",          UniqueNumber = "HRN321", CanAssignQuantity = false },
            new TrainComponent { Name = "Coupler",       UniqueNumber = "CPL654", CanAssignQuantity = false },
            new TrainComponent { Name = "Hinge",         UniqueNumber = "HNG987", CanAssignQuantity = true,  QuantityAssignment = 500 },
            new TrainComponent { Name = "Ladder",        UniqueNumber = "LDR456", CanAssignQuantity = true,  QuantityAssignment = 30  },
            new TrainComponent { Name = "Paint",         UniqueNumber = "PNT789", CanAssignQuantity = false },
            new TrainComponent { Name = "Decal",         UniqueNumber = "DCL321", CanAssignQuantity = true,  QuantityAssignment = 200 },
            new TrainComponent { Name = "Gauge",         UniqueNumber = "GGS654", CanAssignQuantity = true,  QuantityAssignment = 45  },
            new TrainComponent { Name = "Battery",       UniqueNumber = "BTR987", CanAssignQuantity = false },
            new TrainComponent { Name = "Radiator",      UniqueNumber = "RDR456", CanAssignQuantity = false }
        };
            db.TrainComponents.AddRange(items);
            db.SaveChanges();
        }
    }
}
