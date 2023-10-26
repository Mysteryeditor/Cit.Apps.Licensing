namespace Cit.Apps.Shared.Result
{
    public class Result<T>
    {
        public async static Task<ResultModel<T>> ReturnResult(int statuscode, string message, T Data)
        {
            return new ResultModel<T>()
            {
                Statuscode = statuscode,
                Message = message,
                Data = Data
            };
        }
    }
}