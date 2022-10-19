using absolwent.Models;

namespace absolwent.Services
{
    public interface IPoolService
    {
        void StartPool();
        void StartPool(User user, PoolSettings pool);
    }
}