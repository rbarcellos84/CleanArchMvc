namespace CleanArchMvc.Domain.Validation
{
    public class DomainExceptionValidation : Exception
    {
        public DomainExceptionValidation(String error) : base(error) { }

        public static void When(bool hasError, string error)
        {
            if (hasError)
            {
                throw new DomainExceptionValidation(error);
            }
        }
    }
}
