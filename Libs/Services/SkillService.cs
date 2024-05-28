namespace Libs.Services
{
    public class SkillService
    {
        private ApplicationDbContext dbContext;
        private ISkillRepository skillRepository;

        public SkillService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.skillRepository = new SkillRepository(this.dbContext);
        }

        public void Save()
        {
            this.dbContext.SaveChanges();
        }

        public List<Skill> getSkillsList()
        {
            return skillRepository.getSkillsList();
        }
        public Skill getSkillById(int id)
        {
            return this.skillRepository.getSkillById(id);
        }
        public void insertSkill(Skill skill)
        {
            dbContext.Skills.Add(skill);
            Save();
        }

        public void updateSkill(Skill skill)
        {
            skillRepository.Update(skill);
            Save();
        }
        public void deleteSkill(int id)
        {
            Skill skill = skillRepository.getSkillById(id);
            if (skill != null)
            {
                skillRepository.Delete(skill);
                Save();
            }
        }
    }
}
