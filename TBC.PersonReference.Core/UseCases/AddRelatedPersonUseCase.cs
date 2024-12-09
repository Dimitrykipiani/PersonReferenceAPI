using TBC.PersonReference.Application;
using TBC.PersonReference.Core.Entities;
using TBC.PersonReference.Core.Exceptions;

namespace TBC.PersonReference.Core.UseCases
{
    public class AddRelatedPersonUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddRelatedPersonUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<(bool Success, string? ErrorMessage)> ExecuteAsync(int personId, int relatedPersonId, RelationType relationType)
        {
            var person = await _unitOfWork.PersonRepository.GetByIdAsync(personId);
            var relatedPerson = await _unitOfWork.PersonRepository.GetByIdAsync(relatedPersonId);

            if (person == null || relatedPerson == null)
                throw new NotFoundException($"One or both persons do not exist. IDs - {personId} {relatedPersonId}");

            var existingRelation = await _unitOfWork.PersonRepository
                .FindRelationAsync(personId, relatedPersonId);

            if (existingRelation != null)
                throw new ValidationException("Relationship already exists");

            var relation = new PersonRelation
            {
                PersonId = personId,
                RelatedPersonId = relatedPersonId,
                RelationType = relationType
            };

            await _unitOfWork.PersonRepository.AddRelationAsync(relation);
            await _unitOfWork.CommitAsync();

            return (true, null);
        }
    }
}
