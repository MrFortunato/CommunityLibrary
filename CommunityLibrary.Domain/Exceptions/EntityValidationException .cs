namespace CommunityLibrary.Domain.Exceptions
{
    public class EntityValidationException:Exception
    {
        protected EntityValidationException (string message) : base(message){ }

        public static void Validate(bool hasError, string message)
        {
            if (hasError)
                throw new EntityValidationException (message);
        }   
    }

   
}
