using TavernSystem.Repositories;

namespace TavernSystem.Application;

public interface IAdventurerService
{
    public IEnumerable<AdventurerIdNickname> GetAllAdventurers();
    public AdventurerPerson? GetAdventurerById(int id);
}