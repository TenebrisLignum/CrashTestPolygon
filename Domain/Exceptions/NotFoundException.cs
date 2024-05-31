namespace Domain.Exceptions
{
    public class NotFoundException(string message) 
        : Exception(string.Format(Consts.NotFoundException, message))
    {
    }
}
