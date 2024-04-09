using AllupProjectMVC.Business.Interfaces;
using AllupProjectMVC.Data;
using AllupProjectMVC.Exceptions;
using AllupProjectMVC.Exceptions.CategoryExceptions;
using AllupProjectMVC.Exceptions.FeatureExceptions;
using AllupProjectMVC.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace AllupProjectMVC.Business.Implementations
{
    public class FeatureService : IFeatureService
    {
        public readonly AllupDbContext _context;
        public FeatureService(AllupDbContext context)
        {
            _context = context;
        }
        public async Task CreateFeature(Feature feature)
        {
            if (_context.Features.Any(x => x.Title.ToLower() == feature.Title.ToLower()))
                throw new NameAlreadyExistException("Title", "Feature title is already exist!");
            await _context.Features.AddAsync(feature);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFeature(int Id)
        {
            var data = await _context.Features.FindAsync(Id);
            if (data is null) throw new FeatureNotFoundException("Feature not Found");

            _context.Remove(data);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Feature>> GetAllAsync(Expression<Func<Feature, bool>>? expression = null, params string[] includes)
        {
            var query = _context.Features.AsQueryable();
            query = _getIncludes(query, includes);
            return expression is not null
                ? await query.Where(expression).ToListAsync()
                : await query.ToListAsync();
        }

        public async Task<Feature> GetByIdAsync(int Id)
        {
            var data = await _context.Features.FindAsync(Id);
            if (data is null) throw new FeatureNotFoundException();
            return data;
        }


        public async Task UpdateFeature(Feature feature)
        {
            var existData = await _context.Features.FindAsync(feature.Id);
            if (existData is null) throw new FeatureNotFoundException("Feature not found");
            if (_context.Features.Any(x => x.Title.ToLower() == feature.Title.ToLower())
                && existData.Title != feature.Title)
                throw new NameAlreadyExistException("Title", "Feature title is already exist!");
            existData.Title = feature.Title;
            await _context.SaveChangesAsync();
        }
        private IQueryable<Feature> _getIncludes(IQueryable<Feature> query, params string[] includes)
        {
            if (includes is not null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return query;
        }
    }
}
