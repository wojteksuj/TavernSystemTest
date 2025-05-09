using System.Data;
using Microsoft.Data.SqlClient;
using TavernSystem.Models;
using TavernSystem.Repositories;

namespace TavernSystem.Application;

public class AdventurerService : IAdventurerService
{
    private string _connectionString;

    public AdventurerService(string connectionString)
    {
        _connectionString = connectionString;
    }
    public IEnumerable<AdventurerIdNickname> GetAllAdventurers()
    {
        List<AdventurerIdNickname> adventurerIdNicknames = [];
        const string queryString = "SELECT Id, Nickname FROM Adventurer";

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            SqlCommand command = new SqlCommand(queryString, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            try
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var adventurerRow = new AdventurerIdNickname()
                        {
                            Id = reader.GetInt32(0),
                            Nickname = reader.GetString(1),
                        };
                        adventurerIdNicknames.Add(adventurerRow);
                    }
                }
            }
            finally
            {
                reader.Close();
            }
        }
        return adventurerIdNicknames;
    }

    public AdventurerPerson? GetAdventurerById(int adventurerId)
    {
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();

        const string baseQuery = "SELECT a.Id, a.Nickname, r.Race, e.Experience, p.Id, p.FirstName, p.MiddleName, p.LastName, p.HasBounty FROM Adventurer a JOIN Race r ON a.RaceId = r.Id JOIN ExperienceLevel e ON a.ExperienceId = e.Id JOIN Person p ON p.Id = a.PersonId WHERE a.Id = @adventurerId";
        
        using SqlCommand baseCommand = new SqlCommand(baseQuery, connection);
        baseCommand.Parameters.AddWithValue("@adventurerId", adventurerId);

        using SqlDataReader baseReader = baseCommand.ExecuteReader();
        if (!baseReader.Read())
            return null;

        int id = baseReader.GetInt32(0);
        string nickname = baseReader.GetString(1);
        string race = baseReader.GetString(2);
        string experience = baseReader.GetString(3);
        string idPerson = baseReader.GetString(4);
        string firstName = baseReader.GetString(5);
        string middleName = baseReader.GetString(6);
        string lastName = baseReader.GetString(7);
        bool hasBounty = baseReader.GetBoolean(8);
        
        Person personData = new Person(idPerson, firstName, middleName, lastName, hasBounty);

        AdventurerPerson? adventurerPerson = new AdventurerPerson(id, nickname, race, experience, personData);
        
        baseReader.Close();
        
        return adventurerPerson;
    }



}


    
    
