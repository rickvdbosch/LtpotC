using System.IO;
using System.Threading.Tasks;

using Azure.Storage.Blobs;

namespace Rickvdbosch.Talks.LtpotC.Common.Azure.Storage
{
    public class BlobStorageRepository
    {
        #region Fields

        private string _connectionString;

        #endregion

        #region Constructors

        public BlobStorageRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        #endregion

        public async Task AddFileAsync(string containerName, string filename, Stream file)
        {
            var blobClient = new BlobClient(_connectionString, containerName, filename);

            await blobClient.UploadAsync(file);
        }
    }
}