using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBC.PersonReference.Application;
using TBC.PersonReference.Core.Exceptions;

namespace TBC.PersonReference.Core.UseCases
{
    public class RemoveRelatedPersonUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public RemoveRelatedPersonUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<(bool Success, string? ErrorMessage)> ExecuteAsync(int personId, int relatedPersonId)
        {
            // Find the relationship
            var relation = await _unitOfWork.PersonRepository
                .FindRelationAsync(personId, relatedPersonId);

            if (relation == null)
                throw new NotFoundException("Relationship does not exist.");

            // Remove the relationship
            await _unitOfWork.PersonRepository.RemoveRelationAsync(relation);
            await _unitOfWork.CommitAsync();

            return (true, null);
        }
    }
}
