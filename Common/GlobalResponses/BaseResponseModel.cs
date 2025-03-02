namespace Common.GlobalResponses;

public class BaseResponseModel
{


    public bool IsSuccess { get; set; }

    public List<string> Errors { get; set; }
    public BaseResponseModel(List<string> errors)
    {
        Errors = errors;
    }

    public BaseResponseModel()
    {
        IsSuccess = true;
        Errors = null;
    }

}
