namespace ApiCatalogoClientes.Data.Response
{
    public class GeneralResponse
    {
        public int Status { get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; } = null;

        public GeneralResponse() { }

        public GeneralResponse(int status, string message, object data)
        {
            Status = status;
            Message = message;
            Data = data;
        }
    }
}
