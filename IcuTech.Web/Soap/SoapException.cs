namespace IcuTech.Web.Soap;

public class SoapException : Exception
{
    public SoapException()
    {
    }

    public SoapException(string message) : base(message)
    {
    }

    public SoapException(string message, Exception innerException) : base(message, innerException)
    {
    }
}