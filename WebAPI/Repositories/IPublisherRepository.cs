using WebAPI.Models.Domain;
using WebAPI.Models.DTO;

namespace WebAPI.Repositories
{
    public interface IPublisherRepository
    {
        List<PublisherDTO> GetAllPublishers();
        PublisherNoIdDTO GetPublisherById(int id);
        AddPublisherRequestDTO AddPublisher(AddPublisherRequestDTO addPublisherRequestDTO);
        PublisherNoIdDTO UpdatePublisherById(int id, PublisherNoIdDTO publisherNoIdDTO);
        Publisher? DeletePublisherById(int id);
        bool ExistsByName(string name, int? excludeId = null);
        bool ExistsById(int id);
    }
}
