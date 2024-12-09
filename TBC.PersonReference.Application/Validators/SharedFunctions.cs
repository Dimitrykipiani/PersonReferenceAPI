namespace TBC.PersonReference.Application.Validators
{
    public static class SharedFunctions
    {
        public static bool BeAtLeast18YearsOld(DateTime birthDate)
        {
            var today = DateTime.UtcNow;
            var age = today.Year - birthDate.Year;
            if (birthDate > today.AddYears(-age)) age--;
            return age >= 18;
        }
    }
}
