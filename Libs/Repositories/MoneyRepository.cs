namespace Libs.Repositories
{
    public interface IMoneyRepository : IRepository<Money>
    {
        public List<Money> getMoneysList();
        public Money getMoneyById(int id);
    }
    public class MoneyRepository : RepositoryBase<Money>, IMoneyRepository
    {
        public MoneyRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
        public List<Money> getMoneysList()
        {
            return _dbContext.Moneys.ToList();
        }

        public Money getMoneyById(int id)
        {
            return _dbContext.Moneys.FirstOrDefault(money => money.Id == id);
        }
    }
}
