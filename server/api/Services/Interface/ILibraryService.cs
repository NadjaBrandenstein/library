namespace api.Services;

public interface ILibraryService<TDto, TCreateDto, TUpdateDto>
{
    Task<List<TDto>> GetAll();
    Task<TDto> Create(TCreateDto dto);
    Task<TDto?> Update(TUpdateDto dto);
    Task<TDto?> Delete(string id);
    Task<TDto?> GetById(string id);
}