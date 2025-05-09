namespace TavernSystem.Models;

public class Person
{
    private string Id {get;set;}
    private string FirstName {get;set;}
    private string MiddleName {get;set;}
    private string LastName {get;set;}
    private bool HasBounty {get;set;}

    public Person(string id, string firstName, string middleName, string lastName, bool hasBounty)
    {
        Id = id;
        FirstName = firstName;
        MiddleName = middleName;
        LastName = lastName;
        HasBounty = hasBounty;
    }
    
}