using Equipment.Model.AwsDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Equipment.Data.Services
{
    public interface IStorageService
    {
        Task<S3ResponseDto> UploadFileAsync(S3Object obj, AwsCredentials awsCredentialsValues);
    }
}
