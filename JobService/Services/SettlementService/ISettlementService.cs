using JobService.Models;

namespace JobService.Services.SettlementService
{
    public interface ISettlementService
    {
        public List<Region> GetRegionsList();

        public List<Settlement> GetRegionSettlementsList(int regionId);
    }
}
