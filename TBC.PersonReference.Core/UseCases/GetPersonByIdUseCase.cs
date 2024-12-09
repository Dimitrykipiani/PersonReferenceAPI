using TBC.PersonReference.Application;
using TBC.PersonReference.Core.Entities;
using TBC.PersonReference.Core.Exceptions;

namespace TBC.PersonReference.Core.UseCases
{
    public class GetPersonByIdUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPersonByIdUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Person> ExecuteAsync(int id)
        {
            var person = await _unitOfWork.PersonRepository.GetByIdAsync(id);

            if (person == null)
                throw new NotFoundException($"Person with Id - {id} could not be found");

            return person;
        }
    }
}
