namespace Business.Models.ResponseModels;

public class ResponseResult<T>
{
    public T? Data { get; set; }
    public bool IsSuccess { get; set; }
    public string? Message { get; set; } 

 
}
