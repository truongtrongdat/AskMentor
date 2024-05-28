namespace Libs.Services
{
    public class MoneyService
    {
        private ApplicationDbContext dbContext;
        private IMoneyRepository MoneyRepository;

        public MoneyService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.MoneyRepository = new MoneyRepository(dbContext);
        }

        public void Save()
        {
            this.dbContext.SaveChanges();
        }

        public List<Money> getMoneysList()
        {
            return MoneyRepository.getMoneysList();
        }
        public Money getMoneyById(int id)
        {
            return this.MoneyRepository.getMoneyById(id);
        }
        public void insertMoney(Money money)
        {
            dbContext.Moneys.Add(money);
            Save();
        }
        public void updateMoney(Money money)
        {
            MoneyRepository.Update(money);
            Save();
        }
        public void deleteMoney(int id)
        {
            Money money = MoneyRepository.getMoneyById(id);
            if (money != null)
            {
                MoneyRepository.Delete(money);
                Save();
            }
        }
    }
}
