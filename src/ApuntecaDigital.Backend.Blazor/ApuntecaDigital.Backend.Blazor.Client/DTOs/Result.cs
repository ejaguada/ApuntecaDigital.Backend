namespace ApuntecaDigital.Backend.Blazor.Client.DTOs;

public class Result<T> where T : class
{
    public int ErrorCode { get; set; }
    public string ErrorMessage { get; set; } = string.Empty;
    public T Data { get; set; } = null!;
}
