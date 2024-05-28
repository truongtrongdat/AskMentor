namespace Libs.Repositories
{
    public interface ISkillRepository : IRepository<Skill>
    {
        public List<Skill> getSkillsList();
        public Skill getSkillById(int id);
    }
    public class SkillRepository : RepositoryBase<Skill>, ISkillRepository
    {
        public SkillRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Skill getSkillById(int id)
        {
            return _dbContext.Skills.FirstOrDefault(skill => skill.Id == id);
        }

        public List<Skill> getSkillsList()
        {
            return _dbContext.Skills.ToList();
        }
    }
}
