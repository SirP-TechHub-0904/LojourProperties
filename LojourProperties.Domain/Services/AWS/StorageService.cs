using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using LojourProperties.Domain.Dtos.AwsDtos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace LojourProperties.Domain.Services.AWS
{
    public class StorageService : IStorageService
    {
        private readonly IConfiguration _config;
        //private readonly IAmazonS3 _client;
        public StorageService(IConfiguration config/*, IAmazonS3 client*/)
        {
            _config = config;
            //_client = client;
        }

        public async Task<S3ResponseDto> UploadFileAsync(Domain.Dtos.AwsDtos.S3Object obj, AwsCredentials awsCredentialsValues)
        {
            //var awsCredentialsValues = _config.ReadS3Credentials();

            // Console.WriteLine($"Key: {awsCredentialsValues.AccessKey}, Secret: {awsCredentialsValues.SecretKey}");

            var credentials = new BasicAWSCredentials(awsCredentialsValues.AccessKey, awsCredentialsValues.SecretKey);

            var config = new AmazonS3Config()
            {
                RegionEndpoint = Amazon.RegionEndpoint.USEast1
            };

            var response = new S3ResponseDto();
            try
            {
                var uploadRequest = new TransferUtilityUploadRequest()
                {
                    InputStream = obj.InputStream,
                    Key = obj.Name,
                    BucketName = obj.BucketName,
                    CannedACL = S3CannedACL.NoACL
                };

                // initialise client
                using var client = new AmazonS3Client(credentials, config);

                // initialise the transfer/upload tools
                var transferUtility = new TransferUtility(client);

                // initiate the file upload
                await transferUtility.UploadAsync(uploadRequest);

                response.StatusCode = 201;
                response.Message = $"{obj.Name} has been uploaded sucessfully";
            }
            catch (AmazonS3Exception s3Ex)
            {
                response.StatusCode = (int)s3Ex.StatusCode;
                response.Message = s3Ex.Message;
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<string> DownloadFileAsync(string file, string bucketName, AwsCredentials awsCredentialsValues)
        {


            var credentials = new BasicAWSCredentials(awsCredentialsValues.AccessKey, awsCredentialsValues.SecretKey);

            var config = new AmazonS3Config()
            {
                RegionEndpoint = Amazon.RegionEndpoint.USEast1
            };

            //var response = new S3ResponseDto();
            Stream rs;
            var request = new GetObjectRequest
            {
                BucketName = bucketName,
                Key = file,
            };

            var preurl = new GetPreSignedUrlRequest
            {
                BucketName = bucketName,
                Key = file,
                Expires = DateTime.UtcNow.AddYears(3)
            };
            using var client = new AmazonS3Client(credentials, config);
             var url = client.GetPreSignedURL(preurl);

            return url;
        }

    }
}
