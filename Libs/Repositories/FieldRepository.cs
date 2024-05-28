namespace Libs.Repositories
{
    public interface IFieldRepository : IRepository<Field>
    {
        public List<Field> getFieldsList();
        public Field getFieldById(int id);
    }
    public class FieldRepository : RepositoryBase<Field>, IFieldRepository
    {
        public FieldRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Field getFieldById(int id)
        {
            return _dbContext.Fields.FirstOrDefault(field => field.Id == id);
        }

        public List<Field> getFieldsList()
        {
            return _dbContext.Fields.ToList();
        }
    }
}
