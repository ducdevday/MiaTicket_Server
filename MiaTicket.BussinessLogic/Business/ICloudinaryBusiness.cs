using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using MiaTicket.BussinessLogic.Model;
using MiaTicket.Setting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Business
{
    public interface ICloudinaryBusiness
    {
        Task<string?> UploadFileAsync(IFormFile file, FileType fileType);
    }
    public class CloudinaryBusiness : ICloudinaryBusiness
    {
        private readonly EnviromentSetting _setting;

        private readonly Cloudinary _cloudinary;

        public CloudinaryBusiness(EnviromentSetting setting)
        {
            _setting = setting;
            _cloudinary = new Cloudinary(setting.GetCouldinaryUrl());
            _cloudinary.Api.Secure = true;
        }

        public async Task<string?> UploadFileAsync(IFormFile file, FileType fileType)
        {
            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var uploadParams = fileType switch
                {
                    //AVATAR_IMAGE,
                    //VIDEO,
                    //EVENT_LOGO_IMAGE,
                    //EVENT_BACKGROUND_IMAGE,
                    //ORGANIZER_LOGO_IMAGE,
                    //TICKET_IMAGE
                    FileType.AVATAR_IMAGE => new ImageUploadParams
                    {
                        File = new FileDescription(file.FileName, stream),
                        Transformation = new Transformation().Crop("fill").Gravity("face").Width(500).Height(500)
                    },
                    FileType.EVENT_LOGO_IMAGE => new ImageUploadParams
                    {
                        File = new FileDescription(file.FileName, stream),
                        Transformation = new Transformation().Crop("fill").Gravity("face").Width(720).Height(958)
                    },
                    FileType.EVENT_BACKGROUND_IMAGE => new ImageUploadParams
                    {
                        File = new FileDescription(file.FileName, stream),
                        Transformation = new Transformation().Crop("fill").Gravity("face").Width(1280).Height(720)
                    },
                    FileType.ORGANIZER_LOGO_IMAGE => new ImageUploadParams
                    {
                        File = new FileDescription(file.FileName, stream),
                        Transformation = new Transformation().Crop("fill").Gravity("face").Width(275).Height(275)
                    },
                    FileType.TICKET_IMAGE => new ImageUploadParams
                    {
                        File = new FileDescription(file.FileName, stream),
                        Transformation = new Transformation().Crop("fill").Gravity("auto").Width(320).Height(180)
                    },
                    FileType.VIDEO => new VideoUploadParams
                    {
                        File = new FileDescription(file.FileName, stream),
                        Transformation = new Transformation()
                    },
                };

                uploadResult = await _cloudinary.UploadAsync(uploadParams);
                return uploadResult.SecureUrl.AbsoluteUri;
            }

            return string.Empty;
        }
    }
}
