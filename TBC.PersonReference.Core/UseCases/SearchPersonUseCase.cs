using TBC.PersonReference.Application;
using TBC.PersonReference.Core.Entities;
using TBC.PersonReference.Core.Models;

namespace TBC.PersonReference.Core.UseCases
{

    public class SearchPersonUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public SearchPersonUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<(IEnumerable<Person>, int)> ExecuteAsync(PersonSearchSpecification specification)
        {
            return await _unitOfWork.PersonRepository.SearchPersonsAsync(specification);
        }
    }

}
