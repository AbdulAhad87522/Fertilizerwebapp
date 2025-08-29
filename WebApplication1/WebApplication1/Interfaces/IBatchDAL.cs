using WebApplication1.Models;

namespace WebApplication1.Interfaces
{
    public interface IBatchDAL
    {
        bool AddBatchWithDetails(Batch batch, List<BatchDetail> details);
        bool BatchExists(string batchName);
    }
}
