using System.Collections.Generic;
using System.Linq;
using trailAPI.Data;
using trailAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace trailAPI.Services
{
    public class ExplorationServices
    {
        private readonly ApplicationDbContext _dbContext;

        public ExplorationServices(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddNewExploration(Explorations exploration)
        {
            _dbContext.Explorations.Add(exploration);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Explorations> GetAllExplorations()  // Changed return type
        {
            return _dbContext.Explorations.ToList();
        }

        public void AddExploration(Explorations exploration)  // Changed from AddNewExploration to AddExploration
        {
            _dbContext.Explorations.Add(exploration);
            _dbContext.SaveChanges();
        }

        public Explorations GetExplorationById(int id)  // Changed return type
        {
            return _dbContext.Explorations
                .FirstOrDefault(e => e.ExplorationID == id)!;
        }
    }
}