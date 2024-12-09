using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBC.PersonReference.Application;
using TBC.PersonReference.Core.Exceptions;
using TBC.PersonReference.Core.Models;

namespace TBC.PersonReference.Core.UseCases
{
    public class GetRelatedPersonReportUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetRelatedPersonReportUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<RelatedPersonReport>> ExecuteAsync(int personId)
        {
            var person = await _unitOfWork.PersonRepository.GetByIdAsync(personId);
            if (person == null)
                throw new NotFoundException($"Person with Id - {personId} does not exist");

            var report = await _unitOfWork.PersonRepository.GetRelatedPersonReportAsync(personId);
            return report;
        }
    }
}
