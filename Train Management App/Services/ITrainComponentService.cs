using Train_Management_App.Data;

namespace Train_Management_App.Services;

public interface ITrainComponentService {
    Task<IEnumerable<TrainComponent>> GetAllAsync();
    Task<TrainComponent?> GetByIdAsync(int id);
    Task<TrainComponent> CreateAsync(TrainComponent component);
    Task<bool> UpdateAsync(int id, TrainComponent component);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<TrainComponent>> SearchAsync(string name, string uniqueNumber);

}
