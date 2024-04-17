namespace ApiCatalogoClientes.Data.Dto
{
    public class ClientDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string DocumentNumber { get; set; }
        public string FileNameCv { get; set; }
        public string PathCv { get; set; }
        public string FileNamePhoto { get; set; }
        public string PathPhoto { get; set; }
        public TypeDocumentDto TypeDocument { get; set; }
    }
}
