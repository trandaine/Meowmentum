using EnglishApp.BusinessLogic.BaseClasses;
using EnglishApp.Infrastructure.Constants;
using Microsoft.AspNetCore.Http;

namespace EnglishApp.Infrastructure.Helpers
{
    public class MediaHelper
    {
        public readonly string _wwwrootPath;

        public MediaHelper(string wwwrootPath)
        {
            _wwwrootPath = wwwrootPath;
        }

        public async Task<StatusCode> SaveMedia(IFormFile? mediaFile, string folderUrl)
        {
            var statusCode = new StatusCode();
            try
            {
                if (mediaFile != null && mediaFile.Length > 0)
                {
                    //// Tạo thư mục lưu ảnh (nếu chưa có)
                    //string uploadsFolder = Path.Combine(_wwwrootPath, "media\\course_images");
                    string solutionRoot = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;
                    string uploadsFolder = Path.Combine(solutionRoot, "SharedMedia", "media", folderUrl);

                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    // Tạo tên file duy nhất
                    var fileExtension = Path.GetExtension(mediaFile.FileName);

                    if (fileExtension == null || !AppConstants.FILE_EXTENSION.Contains(fileExtension))
                    {
                        //return BadRequest("Chỉ chấp nhận file ảnh định dạng png, jpeg, jpg, tiff");
                        statusCode.SetBadRequest("Chỉ chấp nhận file ảnh định dạng png, jpeg, jpg, tiff");
                        return statusCode;
                    }

                    string uniqueFileName = Guid.NewGuid().ToString() + fileExtension;

                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await mediaFile.CopyToAsync(fileStream);
                    }
                    //device.ImageName = uniqueFileName;

                    statusCode.SetSuccess("Lưu file thành công");
                    statusCode.StringReturn = uniqueFileName;
                }

            }
            catch (Exception ex)
            {
                statusCode.SetInternalError(ex.Message);
            }
            return statusCode;
        }
    }
}
