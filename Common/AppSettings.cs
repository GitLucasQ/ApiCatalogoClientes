namespace ApiCatalogoClientes.Common
{
    public class AppSettings
    {
        public S3 S3 { get; set; } = new();
    }

    public class S3
    {
        public string Key { get; set; }
        public string Secret { get; set; }
        public string BucketName { get; set; }
        public string Path { get; set; }
    }
}
