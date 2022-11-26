using UserManager.Models;

namespace UserManager;

public static class DbMock
{
    public static List<User> users = new List<User>
    {
        new User
        {
            Id = 1,
            Email = "mail1@mail.com",
            CategoryId = 1,
            FirstName = "Salome",
            LastName = "Chkeidze",
            PersonalNumber = "01011091701",
            StatusId = 1,
            DateOfBirth = new DateTime(year: 1995, month: 1, day: 29)
        },
        new User
        {
            Id = 2,
            Email = "mail2@mail.com",
            CategoryId = 2,
            FirstName = "Someone",
            LastName = "Else",
            PersonalNumber = "01011091505",
            StatusId = 3,
            DateOfBirth = new DateTime(year: 1991, month: 3, day: 29)
        },
    };

    public static List<Category> categories = new List<Category>
    {
        new Category
        {
            Id = 1,
            Name = "Admin"
        },
        new Category
        {
            Id = 2,
            Name = "User"
        },
        new Category
        {
            Id = 3,
            Name = "Three"
        },
        new Category
        {
            Id = 4,
            Name = "Four"
        },
        new Category
        {
            Id = 5,
            Name = "Five"
        },
        new Category
        {
            Id = 6,
            Name = "Six"
        },
        new Category
        {
            Id = 7,
            Name = "Seven"
        },
        new Category
        {
            Id = 8,
            Name = "Eight"
        },
        new Category
        {
            Id = 9,
            Name = "Nine"
        },
        new Category
        {
            Id = 10,
            Name = "Ten"
        }
    };

    public static List<Status> statuses = new List<Status>()
    {
        new Status
        {
            Id = 1,
            Name = "Active"
        },
        new Status
        {
            Id = 2,
            Name = "Locked"
        },
        new Status
        {
            Id = 3,
            Name = "Three"
        },
        new Status
        {
            Id = 4,
            Name = "Four"
        },
        new Status
        {
            Id = 5,
            Name = "Five"
        },
        new Status
        {
            Id = 6,
            Name = "Six"
        },
        new Status
        {
            Id = 7,
            Name = "Seven"
        },
        new Status
        {
            Id = 8,
            Name = "Eight"
        },
        new Status
        {
            Id = 9,
            Name = "Nine"
        },
        new Status
        {
            Id = 10,
            Name = "Ten"
        }
    };
}