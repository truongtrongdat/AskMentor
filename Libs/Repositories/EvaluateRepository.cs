namespace Libs.Repositories
{
    public interface IEvaluateRepository : IRepository<Evaluate>
    {
        public List<Evaluate> getEvaluatesList();
        public Evaluate getEvaluateById(int id);
        Task<bool> RatingAsync(string userId, int rating, string mentorId, string content);
    }
    public class EvaluateRepository : RepositoryBase<Evaluate>, IEvaluateRepository
    {
        public EvaluateRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
        public List<Evaluate> getEvaluatesList()
        {
            return _dbContext.Evaluates.ToList();
        }

        public Evaluate getEvaluateById(int id)
        {
            return _dbContext.Evaluates.FirstOrDefault(evaluate => evaluate.Id == id);
        }

        public async Task<bool> RatingAsync(string userId, int rating, string mentorId, string content)
        {
            var evaluateE = await _dbContext.Evaluates.FirstOrDefaultAsync(m => m.UserId == userId && m.MentorId == mentorId);
            if (evaluateE != null)
            {
                evaluateE.Rating = rating;
                evaluateE.Content = content;
                _dbContext.Evaluates.Update(evaluateE);
            }
            else
            {
                evaluateE = new Evaluate
                {
                    Rating = rating,
                    MentorId = mentorId,
                    UserId = userId,
                    Content = content
                };
                _dbContext.Evaluates.Add(evaluateE);
            }
            var iQueryable = _dbContext.Evaluates.Where(m => m.MentorId == mentorId);
            var totalEvalate = await iQueryable.CountAsync();
            var totalRating = await iQueryable.SumAsync(m => m.Rating);
            if (totalEvalate > 0)
            {
                var percent = Convert.ToDouble((totalEvalate / totalRating)/5) * 100;
                if (percent < 30)
                {
                    var userE = await _dbContext.ApplicationUser.FirstOrDefaultAsync(m => m.Id == mentorId);
                    userE!.IsLock = true;
                }
            }

            return await _dbContext.SaveChangesAsync() >= 0;
        }
    }
}
