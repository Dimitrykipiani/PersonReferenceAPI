using TBC.PersonReference.Core;
using TBC.PersonReference.Core.Entities;

namespace TBC.PersonReference.Infrastructure.Data
{
    public static class DbInitializer
    {
        public static void Seed(ApplicationDbContext context)
        {
            if (!context.Persons.Any())
            {
                context.Persons.AddRange(
                    new Person
                    {
                        FirstName = "John",
                        LastName = "Doe",
                        Sex = Sex.Male,
                        PrivateNumber = "12345678901",
                        BirthDate = new DateTime(1990, 1, 1),
                        City = "Tbilisi",
                        Image = "images/john_doe.png",
                        PhoneNumbers = new List<PhoneNumber>
                        {
                            new PhoneNumber
                            {
                                NumberType = NumberType.Mobile,
                                Number = "555-1234"
                            }
                        },
                        RelatedPersons = new List<PersonRelation>
                        {
                            new PersonRelation
                            {
                                RelationType = RelationType.Acquaintance,
                                RelatedPersonId = 2
                            }
                        }
                    },
                    new Person
                    {
                        FirstName = "Ray",
                        LastName = "Charles",
                        Sex = Sex.Male,
                        PrivateNumber = "12345678903",
                        BirthDate = new DateTime(1994, 1, 1),
                        City = "New Orleans",
                        Image = "images/ray_charles.png",
                        PhoneNumbers = new List<PhoneNumber>
                        {
                            new PhoneNumber
                            {
                                NumberType = NumberType.Mobile,
                                Number = "555-1235"
                            }
                        },
                        RelatedPersons = new List<PersonRelation>
                        {
                            new PersonRelation
                            {
                                RelationType = RelationType.Acquaintance,
                                RelatedPersonId = 1
                            }
                        }
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
