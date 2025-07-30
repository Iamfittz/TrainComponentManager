using Microsoft.EntityFrameworkCore;
using Train_Management_App.Data;

namespace Train_Management_App.Services;

public class TrainComponentService : ITrainComponentService {
    private readonly AppDbContext _context;

    public TrainComponentService(AppDbContext context) {
        _context = context;
    }
    public async Task<IEnumerable<TrainComponent>> GetAllAsync() {
        return await _context.TrainComponents.ToListAsync();
    }
    public async Task<TrainComponent?> GetByIdAsync(int id) {
        return await _context.TrainComponents.FindAsync(id);
    }
    public async Task<TrainComponent> CreateAsync(TrainComponent component) {
        if (component.CanAssignQuantity && (!component.QuantityAssignment.HasValue || component.QuantityAssignment <= 0))
            throw new ArgumentException("QuantityAssignment must be a positive integer.");

        if (!component.CanAssignQuantity)
            component.QuantityAssignment = null;

        _context.TrainComponents.Add(component);
        await _context.SaveChangesAsync();

        return component;
    }
    public async Task<bool> UpdateAsync(int id, TrainComponent component) {
        if (id != component.Id)
            throw new ArgumentException("ID mismatch");

        var existing = await _context.TrainComponents.FindAsync(id);
        if (existing == null)
            return false;

        if (component.CanAssignQuantity && (!component.QuantityAssignment.HasValue || component.QuantityAssignment <= 0))
            throw new ArgumentException("QuantityAssignment must be a positive integer.");

        if (!component.CanAssignQuantity)
            component.QuantityAssignment = null;

        existing.Name = component.Name;
        existing.UniqueNumber = component.UniqueNumber;
        existing.CanAssignQuantity = component.CanAssignQuantity;
        existing.QuantityAssignment = component.QuantityAssignment;

        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<bool> DeleteAsync(int id) {
        var component = await _context.TrainComponents.FindAsync(id);
        if (component == null)
            return false;

        _context.TrainComponents.Remove(component);
        await _context.SaveChangesAsync();
        return true;
    }
}
