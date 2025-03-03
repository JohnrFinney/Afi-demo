namespace afi_demo.Classes;

public class CustomException : Exception
{
    public int statusCode { get; set; } = 0;
    public IDictionary<string, string> ValidationErrors { get; set; } = new Dictionary<string, string>();
    public string? detail { get; set; } = null;

    public CustomException()
    {
    }

    public CustomException(string message)
        : base(message)
    {
    }

    public CustomException(string message, IDictionary<string, string> ValidationErrors, int statusCode = 500)
        : base(message)
    {
        if (ValidationErrors.Count > 0)
        {
            foreach (var item in ValidationErrors)
            {
                this.Data.Add(item.Key, item.Value);
            }
        }

        if (statusCode != 0)
            this.statusCode = statusCode;
    }

    public CustomException(string message, string? detail = null, int? statusCode = 0)
        : base(message)
    {
        if (!String.IsNullOrEmpty(detail))
            this.detail = detail;

        if (statusCode != 0)
            this.statusCode = statusCode.Value;
    }

    public CustomException(string message, string? detail)
        : base(message)
    {
        if (!String.IsNullOrEmpty(detail))
            this.detail = detail;
    }

    public CustomException(string message, int? statusCode)
        : base(message)
    {
        if (statusCode != 0)
            this.statusCode = statusCode.Value;
    }

    public CustomException(string message, Exception inner)
        : base(message, inner)
    {
    }

    public CustomException(string message, Exception inner, int statusCode = 500)
        : base(message, inner)
    {
        this.statusCode = statusCode;
    }
}
