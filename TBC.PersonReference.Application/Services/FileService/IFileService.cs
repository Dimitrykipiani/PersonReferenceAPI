using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace TBC.PersonReference.Application.FileService
{
    public interface IFileService
    {
        Task<string> SaveFileAsync(IFormFile formFile, string tragetFolder);
    }
}
