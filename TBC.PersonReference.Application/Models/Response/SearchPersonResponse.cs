namespace TBC.PersonReference.Application.Models.Response
{
    public class SearchPersonResponse
    {
        public IEnumerable<PersonResponse> Persons { get; set; } = new List<PersonResponse>();
        public int TotalCount { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
