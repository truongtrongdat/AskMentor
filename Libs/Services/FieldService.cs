namespace Libs.Services
{
    public class FieldService
    {
        private ApplicationDbContext dbContext;
        private IFieldRepository fieldRepository;

        public FieldService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.fieldRepository = new FieldRepository(this.dbContext);
        }

        public void Save()
        {
            this.dbContext.SaveChanges();
        }

        public List<Field> getFieldsList()
        {
            return fieldRepository.getFieldsList();
        }
        public Field getFieldById(int id)
        {
            return this.fieldRepository.getFieldById(id);
        }
        public void insertField(Field field)
        {
            dbContext.Fields.Add(field);
            Save();
        }

        public void updateField(Field field)
        {
            fieldRepository.Update(field);
            Save();
        }
        public void deleteField(int id)
        {
            Field field = fieldRepository.getFieldById(id);
            if (field != null)
            {
                fieldRepository.Delete(field);
                Save();
            }
        }
    }
}
