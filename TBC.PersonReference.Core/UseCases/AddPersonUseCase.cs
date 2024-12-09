using TBC.PersonReference.Application;
using TBC.PersonReference.Core.Entities;

namespace TBC.PersonReference.Core.UseCases
{
    public class AddPersonUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddPersonUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Person> ExecuteAsync(Person person)
        {
            await _unitOfWork.PersonRepository.AddAsync(person);
            await _unitOfWork.CommitAsync();
            return person;
        }
    }
}
