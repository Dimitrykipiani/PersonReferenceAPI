using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TBC.PersonReference.Core;
using TBC.PersonReference.Core.Entities;
using TBC.PersonReference.Core.Interfaces;
using TBC.PersonReference.Core.Models;

namespace TBC.PersonReference.Infrastructure.Data.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly ApplicationDbContext _context;

        public PersonRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Person person)
        {
            await _context.Persons.AddAsync(person);
        }

        public async Task AddRelationAsync(PersonRelation relation)
        {
            await _context.PersonRelations.AddAsync(relation);
        }

        public void Delete(Person person)
        {
            _context.Persons.Remove(person);
        }

        public async Task<PersonRelation?> FindRelationAsync(int personId, int relatedPersonId)
        {
            var query = _context.PersonRelations
                .Where(pr => pr.PersonId == personId && pr.RelatedPersonId == relatedPersonId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Person?> GetByIdAsync(int id)
        {
            return await _context.Persons
                .Include(p => p.PhoneNumbers)
                .Include(p => p.RelatedPersons)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<RelatedPersonReport>> GetRelatedPersonReportAsync(int personId)
        {
            return await _context.PersonRelations
                    .Where(pr => pr.PersonId == personId)
                    .GroupBy(pr => pr.RelationType)
                    .Select(g => new RelatedPersonReport
                    {
                        RelationType = g.Key,
                        Count = g.Count()
                    })
                    .ToListAsync();
        }

        public async Task RemoveRelationAsync(PersonRelation relation)
        {
             _context.PersonRelations.Remove(relation);
        }

        public async Task<(IEnumerable<Person>, int)> SearchPersonsAsync(PersonSearchSpecification request)
        {
            var query = _context.Persons
                .Include(p => p.RelatedPersons)
                .AsQueryable();

            if (request.IsQuickSearch)
            {
                if (!string.IsNullOrEmpty(request.FirstName)
                    || !string.IsNullOrEmpty(request.LastName)
                    || !string.IsNullOrEmpty(request.IdentityNumber))
                {
                    query = query.Where(p => EF.Functions.Like(p.FirstName, $"%{request.FirstName}%") ||
                                             EF.Functions.Like(p.FirstName, $"%{request.LastName}%") ||
                                             EF.Functions.Like(p.FirstName, $"%{request.IdentityNumber}%"));
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(request.FirstName))
                    query = query.Where(p => p.FirstName == request.FirstName);
                if (!string.IsNullOrEmpty(request.LastName))
                    query = query.Where(p => p.LastName == request.LastName);
                if (!string.IsNullOrEmpty(request.IdentityNumber))
                    query = query.Where(p => p.PrivateNumber == request.IdentityNumber);
                if (!string.IsNullOrEmpty(request.City))
                    query = query.Where(p => p.City == request.City);
                if (request.BirthDateFrom.HasValue)
                    query = query.Where(p => p.BirthDate >= request.BirthDateFrom.Value);
                if (request.BirthDateTo.HasValue)
                    query = query.Where(p => p.BirthDate <= request.BirthDateTo.Value);
                if (!string.IsNullOrEmpty(request.PhoneNumber))
                    query = query.Where(p => p.PhoneNumbers.Any(ph => EF.Functions.Like(ph.Number, $"%{request.PhoneNumber}%")));
            }

            // Total count for pagination
            var totalCount = await query.CountAsync();

            // Pagination
            var persons = await query
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
                .ToListAsync();

            return (persons, totalCount);
        }

        public void Update(Person person)
        {
            _context.Persons.Update(person);
        }
    }
}
