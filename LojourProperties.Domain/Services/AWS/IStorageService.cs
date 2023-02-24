using LojourProperties.Domain.Dtos.AwsDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojourProperties.Domain.Services.AWS
{
   public interface IStorageService
    {
        Task<S3ResponseDto> UploadFileAsync(S3Object obj, AwsCredentials awsCredentialsValues);
        Task<string> DownloadFileAsync(string file, string bucketName, AwsCredentials awsCredentialsValues);
    }
}
