using LojourProperties.Domain.Dtos.AwsDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LojourProperties.Domain.Services.ImageResize
{
    public interface IFileImageResize
    {
        Task<MemoryStream> UploadFileAsync(string url, int xheight, int xwidth);

    }
}
