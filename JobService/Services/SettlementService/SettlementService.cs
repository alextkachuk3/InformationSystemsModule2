using JobService.Data;
using JobService.Models;

namespace JobService.Services.SettlementService
{
    public class SettlementService : ISettlementService
    {
        private readonly ApplicationDbContext _dbContext;

        public SettlementService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Settlement> GetRegionSettlementsList(int regionId)
        {
            return _dbContext.Settlements!.Where(s => s.Region!.Id.Equals(regionId)).ToList();
        }

        public List<Region> GetRegionsList()
        {
            return _dbContext.Regions!.ToList();
        }
    }
}
