using TBC.PersonReference.Application;
using TBC.PersonReference.Core.Exceptions;

namespace TBC.PersonReference.Core.UseCases
{
    public class DeletePersonUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeletePersonUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> DeletePersonAsync(int id)
        {
            var existingPerson = await _unitOfWork.PersonRepository.GetByIdAsync(id);

            if (existingPerson == null)
                throw new NotFoundException($"Person with Id - {id} could not be found");

            _unitOfWork.PersonRepository.Delete(existingPerson);
            await _unitOfWork.CommitAsync();

            return existingPerson.Id;
        }
    }
}
