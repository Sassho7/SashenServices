namespace SmartGarage.Exceptions;

public class DuplicateEntityExcetion : ApplicationException
{
    public DuplicateEntityExcetion(string message) : base(message)
    {

    }
}