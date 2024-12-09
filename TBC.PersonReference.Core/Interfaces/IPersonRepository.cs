using TBC.PersonReference.Core.Entities;
using TBC.PersonReference.Core.Models;

namespace TBC.PersonReference.Core.Interfaces
{
    public interface IPersonRepository
    {
        Task AddAsync(Person person);
        Task<Person?> GetByIdAsync(int id);
        Task<(IEnumerable<Person>, int)> SearchPersonsAsync(PersonSearchSpecification specification);
        void Update(Person person);
        void Delete(Person person);
        Task AddRelationAsync(PersonRelation relation);
        Task RemoveRelationAsync(PersonRelation relation);
        Task<PersonRelation?> FindRelationAsync(int personId, int relatedPersonId);
        Task<IEnumerable<RelatedPersonReport>> GetRelatedPersonReportAsync(int personId);
    }
}
