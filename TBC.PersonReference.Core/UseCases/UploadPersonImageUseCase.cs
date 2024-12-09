using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBC.PersonReference.Application;
using TBC.PersonReference.Core.Exceptions;

namespace TBC.PersonReference.Core.UseCases
{
    public class UploadPersonImageUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public UploadPersonImageUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<(bool Success, string? FilePath, string? ErrorMessage)> ExecuteAsync(int personId, string fileName)
        {
            var person = await _unitOfWork.PersonRepository.GetByIdAsync(personId);
            if (person == null)
                throw new NotFoundException($"Person with id - {personId} could not be found");

            person.Image = fileName;
            _unitOfWork.PersonRepository.Update(person);
            await _unitOfWork.CommitAsync();

            return (true, fileName, null);
        }
    }
}
