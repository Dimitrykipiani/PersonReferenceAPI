using System;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using TBC.PersonReference.Application.FileService;
using TBC.PersonReference.Application.Models.Request;
using TBC.PersonReference.Application.Models.Response;
using TBC.PersonReference.Core;
using TBC.PersonReference.Core.Entities;
using TBC.PersonReference.Core.Models;
using TBC.PersonReference.Core.UseCases;

namespace TBC.PersonReference.Application.Services.PersonService
{
    public class PersonService : IPersonService
    {
        private readonly AddPersonUseCase _addPersonUseCase;
        private readonly GetPersonByIdUseCase _getPersonByIdUseCase;
        private readonly UpdatePersonUseCase _updatePersonByIdUseCase;
        private readonly DeletePersonUseCase _deletePersonUseCase;
        private readonly SearchPersonUseCase _searchPersonUseCase;
        private readonly UploadPersonImageUseCase _uploadPersonImageUseCase;
        private readonly AddRelatedPersonUseCase _addRelatedPersonUseCase;
        private readonly RemoveRelatedPersonUseCase _removeRelatedPersonUseCase;
        private readonly GetRelatedPersonReportUseCase _getRelatedPersonReportUseCase;
        private readonly IFileService _fileService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public PersonService(AddPersonUseCase addPersonUseCase,
                            GetPersonByIdUseCase getPersonByIdUseCase,
                            UpdatePersonUseCase updatePersonByIdUseCase,
                            DeletePersonUseCase deletePersonUseCase,
                            SearchPersonUseCase searchPersonUseCase,
                            UploadPersonImageUseCase uploadPersonImageUseCase,
                            AddRelatedPersonUseCase addRelatedPersonUseCase,
                            RemoveRelatedPersonUseCase removeRelatedPersonUseCase,
                            GetRelatedPersonReportUseCase getRelatedPersonReportUseCase,
                            IFileService fileService,
                            IHttpContextAccessor httpContextAccessor,
                            IMapper mapper)
        {
            _addPersonUseCase = addPersonUseCase;
            _getPersonByIdUseCase = getPersonByIdUseCase;
            _updatePersonByIdUseCase = updatePersonByIdUseCase;
            _deletePersonUseCase = deletePersonUseCase;
            _searchPersonUseCase = searchPersonUseCase;
            _uploadPersonImageUseCase = uploadPersonImageUseCase;
            _addRelatedPersonUseCase = addRelatedPersonUseCase;
            _removeRelatedPersonUseCase = removeRelatedPersonUseCase;
            _getRelatedPersonReportUseCase = getRelatedPersonReportUseCase;
            _fileService = fileService;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<AddPersonResponse> AddPersonAsync(AddPersonRequest request)
        {
            var personEntity = new Person
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Sex = request.Sex,
                PrivateNumber = request.PrivateNumber,
                BirthDate = request.BirthDate,
                City = request.City,
                Image = "images/nbd.png",
                PhoneNumbers = request.PhoneNumbers.Select(p => new PhoneNumber
                {
                    NumberType = p.NumberType,
                    Number = p.Number
                }).ToList()
            };

            var addedPerson = await _addPersonUseCase.ExecuteAsync(personEntity);

            return new AddPersonResponse()
            {
                Id = addedPerson.Id,
                FullName = $"{personEntity.FirstName} {personEntity.LastName}"
            };
        }

        public async Task<(bool Success, string? ErrorMessage)> AddRelatedPersonAsync(int personId, int relatedPersonId, RelationType relationType)
        {
            return await _addRelatedPersonUseCase.ExecuteAsync(personId, relatedPersonId, relationType);
        }

        public async Task<int> DeletePersonAsync(int id)
        {
            await _deletePersonUseCase.DeletePersonAsync(id);

            return id;
        }

        public async Task<PersonResponse> GetPersonByIdAsync(int id)
        {
            var person = await _getPersonByIdUseCase.ExecuteAsync(id);

            var baseUrl = $"{_httpContextAccessor.HttpContext!.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";
            var imageUrl = !string.IsNullOrEmpty(person.Image)
                ? $"{baseUrl}/images/{person.Image}"
                : null;

            var response = _mapper.Map<PersonResponse>(person);
            response.Image = imageUrl;

            return _mapper.Map<PersonResponse>(response);
        }

        public async Task<IEnumerable<RelatedPersonReportDto>> GetRelatedPersonReportAsync(int personId)
        {
            var result = await _getRelatedPersonReportUseCase.ExecuteAsync(personId);

            var reportDto = result.Select(r => new RelatedPersonReportDto
            {
                RelationType = r.RelationType,
                Count = r.Count
            });

            return reportDto;
        }

        public async Task<(bool Success, string? ErrorMessage)> RemoveRelatedPersonAsync(int personId, int relatedPersonId)
        {
            return await _removeRelatedPersonUseCase.ExecuteAsync(personId, relatedPersonId);
        }

        public async Task<SearchPersonResponse> SearchPersonsAsync(SearchPersonRequest request)
        {
            var specification = _mapper.Map<PersonSearchSpecification>(request);

            var (persons, totalCount) = await _searchPersonUseCase.ExecuteAsync(specification);

            return new SearchPersonResponse
            {
                Persons = persons.Select(p => _mapper.Map<PersonResponse>(p)),
                TotalCount = totalCount,
                CurrentPage = request.Page,
                TotalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize)
            };
        }

        public async Task<UpdatePersonResponse> UpdatePersonAsync(UpdatePersonRequest request, int id)
        {
            var personEntity = _mapper.Map<Person>(request);

            var result = await _updatePersonByIdUseCase.ExecuteAsync(personEntity, id);

            return new UpdatePersonResponse()
            {
                Id = result.Id,
                FullName = $"{result.FirstName} {result.LastName}"
            };
        }

        public async Task<(bool Success, string? FilePath, string? ErrorMessage)> UploadImageAsync(int personId, IFormFile file)
        {
            var fileName = await _fileService.SaveFileAsync(file, "wwwroot/images");
            return await _uploadPersonImageUseCase.ExecuteAsync(personId, fileName);
        }
    }
}
