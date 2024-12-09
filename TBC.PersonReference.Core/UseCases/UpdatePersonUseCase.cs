using TBC.PersonReference.Application;
using TBC.PersonReference.Core.Entities;
using TBC.PersonReference.Core.Exceptions;

namespace TBC.PersonReference.Core.UseCases
{
    public class UpdatePersonUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePersonUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Person> ExecuteAsync(Person person, int id)
        {
            var existingPerson = await _unitOfWork.PersonRepository.GetByIdAsync(id);

            if (existingPerson == null)
                throw new NotFoundException($"Person with Id - {id} could not be found");

            existingPerson.FirstName = person.FirstName;
            existingPerson.LastName = person.LastName;
            existingPerson.BirthDate = person.BirthDate;
            existingPerson.City = person.City;
            existingPerson.Image = person.Image;
            existingPerson.PhoneNumbers = person.PhoneNumbers;
            existingPerson.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.PersonRepository.Update(existingPerson);
            await _unitOfWork.CommitAsync();

            return existingPerson;
        }
    }
}
