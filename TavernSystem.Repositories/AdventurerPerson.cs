using TavernSystem.Models;

namespace TavernSystem.Repositories;

public class AdventurerPerson
{
    private int Id {get;set;}
    private string Nickname {get;set;}
    private string Race {get;set;}
    private string Experience {get;set;}
    private Person Person {get;set;}

    public AdventurerPerson(int id, string nickname, string race, string experience, Person person)
    {
        Id = id;
        Nickname = nickname;
        Race = race;
        Experience = experience;
        Person = person;
    }
}