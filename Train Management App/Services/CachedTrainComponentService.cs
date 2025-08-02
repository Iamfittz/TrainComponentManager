using Microsoft.Extensions.Caching.Memory;
using Train_Management_App.Data;

namespace Train_Management_App.Services;

public sealed class CachedTrainComponentService : ITrainComponentService {
    private readonly ITrainComponentService _inner;
    private readonly IMemoryCache _cache;
    private static readonly MemoryCacheEntryOptions _defaultOptions =
        new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(60));

    public CachedTrainComponentService(ITrainComponentService inner, IMemoryCache cache) {
        _inner = inner;
        _cache = cache;
    }

    private static string Key(string prefix, params object?[] args) =>
        $"{prefix}:{string.Join(":", args)}";

    public Task<IEnumerable<TrainComponent>> GetAllAsync() =>
        _cache.GetOrCreateAsync(Key(nameof(GetAllAsync)),
            _ => _inner.GetAllAsync(), _defaultOptions);

    public Task<TrainComponent?> GetByIdAsync(int id) =>
        _cache.GetOrCreateAsync(Key(nameof(GetByIdAsync), id),
            _ => _inner.GetByIdAsync(id), _defaultOptions);

    public Task<IEnumerable<TrainComponent>> SearchAsync(string name, string uniqueNumber) =>
        _cache.GetOrCreateAsync(Key(nameof(SearchAsync), name ?? string.Empty, uniqueNumber ?? string.Empty),
            _ => _inner.SearchAsync(name, uniqueNumber), _defaultOptions);

    // Мутации всегда пробивают кэш и сбрасывают его
    public async Task<TrainComponent> CreateAsync(TrainComponent component) {
        var result = await _inner.CreateAsync(component);
        _cache.Remove(Key(nameof(GetAllAsync)));
        return result;
    }

    public async Task<bool> UpdateAsync(int id, TrainComponent component) {
        var ok = await _inner.UpdateAsync(id, component);
        if (ok) {
            _cache.Remove(Key(nameof(GetAllAsync)));
            _cache.Remove(Key(nameof(GetByIdAsync), id));
            _cache.Remove(Key(nameof(SearchAsync), component.Name ?? string.Empty, component.UniqueNumber ?? string.Empty));
        }
        return ok;
    }

    public async Task<bool> DeleteAsync(int id) {
        var ok = await _inner.DeleteAsync(id);
        if (ok) {
            _cache.Remove(Key(nameof(GetAllAsync)));
            _cache.Remove(Key(nameof(GetByIdAsync), id));
        }
        return ok;
    }
}
