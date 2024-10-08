using AidManager.API.Authentication.Domain.Model.Commands;

namespace AidManager.API.Authentication.Domain.Model.Aggregates;

public class Company
{
    public int Id { get; private set; }
     public string CompanyName { get; private set; }
     public string Country { get; private set; }
     public string Email {get; private set; }
     public int ManagerId { get; private set; }
     
     public string TeamRegisterCode { get; private set; }

     private Company() { }
     public Company(CreateCompanyCommand command)
     {
        CompanyName = command.CompanyName;
        Country = command.Country;
        Email = command.Email;
        ManagerId = command.UserId;
        TeamRegisterCode = GenerateRegisterCode();
     }

     private string GenerateRegisterCode()
     {
         return Guid.NewGuid().ToString();
     }
}