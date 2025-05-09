using TavernSystem.Repositories;

namespace TavernSystem.Application;

public interface IAdventurerService
{
    public IEnumerable<AdventurerIdNickname> GetAllAdventurers();
    public AdventurerPerson? GetAdventurerById(int id);
    public bool addAdventurer(int id, string nickname, int idRace, int idExperience, int idPerson);
}