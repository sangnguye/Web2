using WebAPI.Data;
using WebAPI.Models.Domain;
using WebAPI.Models.DTO;

namespace WebAPI.Repositories
{
    public class SQLPublisherRepository : IPublisherRepository
    {
        private readonly AppDbContext _dbContext;

        public SQLPublisherRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<PublisherDTO> GetAllPublishers()
        {
            return _dbContext.Publishers
                .Select(p => new PublisherDTO
                {
                    Id = p.Id,
                    Name = p.Name
                })
                .ToList();
        }

        public PublisherNoIdDTO GetPublisherById(int id)
        {
            var publisher = _dbContext.Publishers.FirstOrDefault(p => p.Id == id);
            if (publisher == null) return null;

            return new PublisherNoIdDTO { Name = publisher.Name };
        }

        public AddPublisherRequestDTO AddPublisher(AddPublisherRequestDTO dto)
        {
            var publisher = new Publisher { Name = dto.Name };
            _dbContext.Publishers.Add(publisher);
            _dbContext.SaveChanges();
            return dto;
        }

        public PublisherNoIdDTO UpdatePublisherById(int id, PublisherNoIdDTO dto)
        {
            var publisher = _dbContext.Publishers.FirstOrDefault(p => p.Id == id);
            if (publisher == null) return null;

            publisher.Name = dto.Name;
            _dbContext.SaveChanges();

            return dto;
        }

        public Publisher? DeletePublisherById(int id)
        {
            var publisher = _dbContext.Publishers.FirstOrDefault(p => p.Id == id);
            if (publisher == null) return null;

            _dbContext.Publishers.Remove(publisher);
            _dbContext.SaveChanges();

            return publisher;
        }
    }
}
