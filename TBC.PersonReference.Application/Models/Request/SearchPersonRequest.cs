namespace TBC.PersonReference.Application.Models.Request
{
    public class SearchPersonRequest
    {
        public bool IsQuickSearch { get; set; } = true;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? IdentityNumber { get; set; }
        public string? City { get; set; }
        public DateTime? BirthDateFrom { get; set; }
        public DateTime? BirthDateTo { get; set; }
        public string? PhoneNumber { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
