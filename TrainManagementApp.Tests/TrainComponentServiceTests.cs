using Microsoft.EntityFrameworkCore;
using Train_Management_App.Data;
using Train_Management_App.Services;

namespace TrainManagementApp.Tests {
    public class TrainComponentServiceTests {
        private readonly TrainComponentService _service;
        private readonly AppDbContext _context;
        public TrainComponentServiceTests() {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;
            _context = new AppDbContext(options);
            _context.Database.EnsureDeleted(); 
            _context.Database.EnsureCreated();
            _service = new TrainComponentService(_context);
        }
        //  AAA approach
        [Fact]
        public async Task GetAllAsync_ReturnsAllComponents() {
            // Arrange
            var components = new Dictionary<string, (string UniqueNumber, bool CanAssignQuantity)> {
                ["Engine"] = ("ENG123", false),
                ["Passenger Car"] = ("PAS456", false),
                ["Freight Car"] = ("FRT789", false),
                ["Wheel"] = ("WHL101", true),
                ["Seat"] = ("STS234", true),
                ["Window"] = ("WIN567", true),
                ["Door"] = ("DR123", true),
                ["Control Panel"] = ("CTL987", true),
                ["Light"] = ("LGT456", true),
                ["Brake"] = ("BRK789", true),
                ["Bolt"] = ("BLT321", true),
                ["Nut"] = ("NUT654", true),
                ["Engine Hood"] = ("EH789", false),
                ["Axle"] = ("AX456", false),
                ["Piston"] = ("PST789", false),
                ["Handrail"] = ("HND234", true),
                ["Step"] = ("STP567", true),
                ["Roof"] = ("RF123", false),
                ["Air Conditioner"] = ("AC789", false),
                ["Flooring"] = ("FLR456", false),
                ["Mirror"] = ("MRR789", true),
                ["Horn"] = ("HRN321", false),
                ["Coupler"] = ("CPL654", false),
                ["Hinge"] = ("HNG987", true),
                ["Ladder"] = ("LDR456", true),
                ["Paint"] = ("PNT789", false),
                ["Decal"] = ("DCL321", true),
                ["Gauge"] = ("GGS654", true),
                ["Battery"] = ("BTR987", false),
                ["Radiator"] = ("RDR456", false)
            };

            foreach (var item in components) {
                await _service.CreateAsync(new TrainComponent {
                    Name = item.Key,
                    UniqueNumber = item.Value.UniqueNumber,
                    CanAssignQuantity = item.Value.CanAssignQuantity,
                    QuantityAssignment = item.Value.CanAssignQuantity ? 1 : null
                });
            }
            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.Equal(30, result.Count());
            Assert.Contains(result, c => c.Name == "Wheel");
            Assert.Contains(result, c => c.Name == "Engine");
        }
        [Fact]
        public async Task GetByIdAsync_ReturnsCorrectComponent() {
            // Arrange
            var component = await _service.CreateAsync(new TrainComponent {
                Name = "Brake",
                UniqueNumber = "BRK123",
                CanAssignQuantity = true,
                QuantityAssignment = 5
            });
            // Act
            var result = await _service.GetByIdAsync(component.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Brake", result.Name);
            Assert.Equal("BRK123", result.UniqueNumber);
            Assert.True(result.CanAssignQuantity);
        }
        [Fact]
        public async Task AddAsync_AddsComponentToDatabase() {
            // Arrange
            var newComponent = new TrainComponent {
                Name = "Signal Light",
                UniqueNumber = "SIG123",
                CanAssignQuantity = false
            };

            // Act
            await _service.CreateAsync(newComponent);
            var result = await _service.GetAllAsync();

            // Assert
            Assert.Single(result);
            var component = result.First();
            Assert.Equal("Signal Light", component.Name);
            Assert.Equal("SIG123", component.UniqueNumber);
            Assert.False(component.CanAssignQuantity);
        }
        [Fact]
        public async Task GetByIdAsync_ReturnsCorrectComponent_WhenExists() {
            // Arrange
            var component = new TrainComponent {
                Name = "Engine",
                UniqueNumber = "ENG999",
                CanAssignQuantity = true,
                QuantityAssignment = 10
            };
            _context.TrainComponents.Add(component);
            await _context.SaveChangesAsync();

            // Act
            var result = await _service.GetByIdAsync(component.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Engine", result!.Name);
            Assert.Equal("ENG999", result.UniqueNumber);
            Assert.True(result.CanAssignQuantity);
            Assert.Equal(10, result.QuantityAssignment);
        }
        [Fact]
        public async Task GetByIdAsync_ReturnsNull_WhenComponentDoesNotExist() {
            // Act
            var result = await _service.GetByIdAsync(999); // нет такого ID

            // Assert
            Assert.Null(result);
        }
        [Fact]
        public async Task CreateAsync_SetsQuantityToNull_WhenCanAssignFalse() {
            // Arrange
            var component = new TrainComponent {
                Name = "Label",
                UniqueNumber = "LBL999",
                CanAssignQuantity = false,
                QuantityAssignment = 10 // ❌ Должен быть обнулён
            };

            // Act
            var created = await _service.CreateAsync(component);

            // Assert
            Assert.False(created.CanAssignQuantity);
            Assert.Null(created.QuantityAssignment);
        }
        [Fact]
        public async Task UpdateAsync_ReturnsTrue_WhenSuccessfulUpdate() {
            // Arrange
            var original = await _service.CreateAsync(new TrainComponent {
                Name = "Old",
                UniqueNumber = "OLD123",
                CanAssignQuantity = true,
                QuantityAssignment = 5
            });

            var updated = new TrainComponent {
                Id = original.Id,
                Name = "Updated",
                UniqueNumber = "NEW456",
                CanAssignQuantity = false // Должно обнулить Quantity
            };

            // Act
            var result = await _service.UpdateAsync(original.Id, updated);
            var fromDb = await _service.GetByIdAsync(original.Id);

            // Assert
            Assert.True(result);
            Assert.Equal("Updated", fromDb!.Name);
            Assert.Null(fromDb.QuantityAssignment); // важный момент
        }
        [Fact]
        public async Task DeleteAsync_RemovesComponent_WhenExists() {
            // Arrange
            var component = await _service.CreateAsync(new TrainComponent {
                Name = "Temp",
                UniqueNumber = "TMP123",
                CanAssignQuantity = true,
                QuantityAssignment = 1
            });

            // Act
            var result = await _service.DeleteAsync(component.Id);
            var fromDb = await _service.GetByIdAsync(component.Id);

            // Assert
            Assert.True(result);
            Assert.Null(fromDb);
        }
        [Fact]
        public async Task SearchAsync_ReturnsMatchingComponents() {
            // Arrange
            await _service.CreateAsync(new TrainComponent { Name = "Wheel", UniqueNumber = "WHL001", CanAssignQuantity = true, QuantityAssignment = 10 });
            await _service.CreateAsync(new TrainComponent { Name = "Door", UniqueNumber = "DR002", CanAssignQuantity = false });

            // Act
            var resultByName = await _service.SearchAsync("Wheel", null);
            var resultByUnique = await _service.SearchAsync(null, "DR002");

            // Assert
            Assert.Single(resultByName);
            Assert.Equal("Wheel", resultByName.First().Name);

            Assert.Single(resultByUnique);
            Assert.Equal("DR002", resultByUnique.First().UniqueNumber);
        }
    }
}
