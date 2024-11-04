using AidManager.API.Authentication.Domain.Model.Commands;
using AidManager.API.Collaborate.Domain.Model.Entities;
using AidManager.API.UserProfile.Domain.Model.Commands;

namespace AidManager.API.Authentication.Domain.Model.Entities;

public class User
{
    public int Id { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public int Age { get; private set; }
    public string Email { get; private set; }
    public string Phone { get; private set; }
    public string Password { get; private set; }
    public string ProfileImg { get; private set; }
    public int CompanyId { get; set; }
    public int Role { get; private set; }
    public string CompanyName {get; set;}
    
    public User(CreateUserCommand command)
    {
        
        this.FirstName = command.FirstName;
        this.LastName = command.LastName;
        this.Age = command.Age;
        this.Email = command.Email;
        this.Phone = command.Phone;
        this.Password = command.Password;
        this.ProfileImg = command.ProfileImg;
        this.Role = command.Role;
        this.CompanyName = command.CompanyName;
    }
   
    private User(){}
    
    public void updateProfile(UpdateUserCommand command)
    {
        this.FirstName = command.FirstName;
        this.LastName = command.LastName;
        this.Age = command.Age;
        this.Phone = command.Phone;
        this.ProfileImg = command.ProfileImg;
        this.Email = command.Email;
        this.Password = command.Password;
    }
    
    public void updateImage(PatchImageCommand command)
    {
        this.ProfileImg = command.Image;
    }

}