using SpenderTracker.Data.Interface;

namespace SpenderTracker.Core.Interfaces;

public interface IBaseService<TEntity, TDto>
    where TEntity : IEntity<TDto>, new()
    where TDto : IDto
{
    /// <summary>
    /// Gets entity by id. CancellationToken used to cancel the operation.
    /// </summary>
    /// <param name="id">The primary key of the entity.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>The entity. If null, the entity was not found.</returns>
    Task<TDto?> GetById(int id, CancellationToken ct);

    /// <summary>
    /// Gets all entities. CancellationToken used to cancel the operation.
    /// </summary>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>The entities.</returns>
    Task<List<TDto>> GetAll(CancellationToken ct);

    /// <summary>
    /// Creates a new entity using its related dto.
    /// </summary>
    /// <param name="dto">A dto that represents the entity to create.</param>
    /// <returns>A new dto that represents that created entity. If null, the insertion has failed.</returns>
    Task<TDto?> Insert(TDto dto);

    /// <summary>
    /// Updates an entity using its related dto.
    /// </summary>
    /// <param name="dto">A dto that represents the entity to update.</param>
    /// <returns>The success of the operation.</returns>
    Task<bool> Update(TDto dto);

    /// <summary>
    /// Deletes an entity by id.
    /// </summary>
    /// <param name="id">The primary key of the entity.</param>
    /// <returns>The success of the operation.</returns>
    Task<bool> Delete(int id);

    /// <summary>
    /// Checks if an entity exists by id. CancellationToken used to cancel the operation.
    /// </summary>
    /// <param name="id">The primary key of the entity</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>The success of the operation.</returns>
    Task<bool> DoesExist(int id, CancellationToken ct);
}
