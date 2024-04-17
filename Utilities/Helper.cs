using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using ApiCatalogoClientes.Common;
using ApiCatalogoClientes.Domain.Entities;
using Microsoft.Extensions.Options;

namespace ApiCatalogoClientes.Utilities
{
    public class Helper
    {
        private readonly IOptions<AppSettings> _appSettings;

        public Helper(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings;
        }

        public async Task UploadFileToS3(string fileName, string fileBase64)
        {
            try
            {
                using var clientS3 = new AmazonS3Client(_appSettings.Value.S3.Key, _appSettings.Value.S3.Secret, RegionEndpoint.USEast2);
                using var memoryStream = new MemoryStream();
                byte[] convert = Convert.FromBase64String(fileBase64);
                memoryStream.Write(convert);

                var uploadRequest = new TransferUtilityUploadRequest
                {
                    InputStream = memoryStream,
                    Key = fileName,
                    BucketName = _appSettings.Value.S3.BucketName,
                    CannedACL = S3CannedACL.PublicRead,
                };

                var fileTransferUtility = new TransferUtility(clientS3);
                await fileTransferUtility.UploadAsync(uploadRequest);
            }
            catch (AmazonS3Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
