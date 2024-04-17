namespace ApiCatalogoClientes.Data.Request
{
    public class CreateUpdateClientRequest
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BirthDate { get; set; }
        public int IdTypeDocument { get; set; }
        public string DocumentNumber { get; set; }
        public string CvFileName { get; set; }
        public string CvBase64 { get; set; }
        public string PhotoFileName { get; set; }
        public string PhotoBase64 { get; set; }
    }
}
