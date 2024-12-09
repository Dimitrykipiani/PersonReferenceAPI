using Microsoft.AspNetCore.Http;
using TBC.PersonReference.Application.Models.Request;
using TBC.PersonReference.Application.Models.Response;
using TBC.PersonReference.Core;

namespace TBC.PersonReference.Application.Services.PersonService
{
    public interface IPersonService
    {
        public Task<AddPersonResponse> AddPersonAsync(AddPersonRequest request);
        public Task<PersonResponse> GetPersonByIdAsync(int id);
        public Task<SearchPersonResponse> SearchPersonsAsync(SearchPersonRequest request);
        public Task<UpdatePersonResponse> UpdatePersonAsync(UpdatePersonRequest request, int id);
        public Task<int> DeletePersonAsync(int id);
        Task<(bool Success, string? FilePath, string? ErrorMessage)> UploadImageAsync(int personId, IFormFile file);
        Task<(bool Success, string? ErrorMessage)> AddRelatedPersonAsync(int personId, int relatedPersonId, RelationType relationType);
        Task<(bool Success, string? ErrorMessage)> RemoveRelatedPersonAsync(int personId, int relatedPersonId);
        Task<IEnumerable<RelatedPersonReportDto>> GetRelatedPersonReportAsync(int personId);
    }
}
