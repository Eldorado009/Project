using AllupProjectMVC.Models;
using System.Linq.Expressions;

namespace AllupProjectMVC.Business.Interfaces
{
    public interface IFeatureService
    {
        Task<List<Feature>> GetAllAsync(Expression<Func<Feature, bool>>? expression = null, params string[] includes);
        Task<Feature> GetByIdAsync(int Id);
        Task CreateFeature(Feature feature);
        Task UpdateFeature(Feature feature);
        Task DeleteFeature(int Id);
    }
}
