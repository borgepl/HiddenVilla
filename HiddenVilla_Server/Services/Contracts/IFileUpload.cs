using Microsoft.AspNetCore.Components.Forms;

namespace HiddenVilla_Server.Services.Contracts
{
    public interface IFileUpload
    {
        Task<string> UploadFile(IBrowserFile file);

        bool DeleteFile(string fileName);
    }
}
