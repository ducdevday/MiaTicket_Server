using AutoMapper;
using MiaTicket.BussinessLogic.Model;
using MiaTicket.BussinessLogic.Request;
using MiaTicket.BussinessLogic.Response;
using MiaTicket.CloudinaryStorage;
using MiaTicket.CloudinaryStorage.Model;
using MiaTicket.DataAccess;
using System.Net;

namespace MiaTicket.BussinessLogic.Business
{
    public interface IBannerBusiness
    {
        Task<CreateBannerResponse> CreateBanner(CreateBannerRequest request);
        Task<GetBannersDiscoveryResponse> GetBannersDiscovery();
    }

    public class BannerBusiness : IBannerBusiness
    {
        private readonly IDataAccessFacade _context;
        private readonly ICloudinaryService _cloudinary;
        private readonly IMapper _mapper;

        public BannerBusiness(IDataAccessFacade context, ICloudinaryService cloudinary, IMapper mapper)
        {
            _context = context;
            _cloudinary = cloudinary;
            _mapper = mapper;
        }

        public async Task<CreateBannerResponse> CreateBanner(CreateBannerRequest request)
        {
            var isExistEvent = await _context.EventData.IsExistEvent(request.EventId);
            if (!isExistEvent) return new CreateBannerResponse(HttpStatusCode.Conflict, "Event is not exist", false);

            string? videoUrl = await _cloudinary.UploadFileAsync(request.VideoFile, FileType.VIDEO);
            if (videoUrl == null)
            {
                return new CreateBannerResponse(HttpStatusCode.Conflict, "Video Upload Failed", false);
            }

            var banner = await _context.BannerData.CreateBanner(request.EventId, videoUrl);
            if (banner == null)
            {
                return new CreateBannerResponse(HttpStatusCode.Conflict, "Create Banner Failed", false);
            }
            await _context.Commit();
            return new CreateBannerResponse(HttpStatusCode.OK, "Create Banner Success", true);
        }

        public async Task<GetBannersDiscoveryResponse> GetBannersDiscovery()
        {
            var banners = await _context.BannerData.GetBannerList();
            if (banners == null) {
                return new GetBannersDiscoveryResponse(HttpStatusCode.Conflict, "Get Banners Discovery Failed", []);
            }

            var dataResponse = _mapper.Map<List<BannerDiscoveryDto>>(banners);
            await _context.Commit();
            return new GetBannersDiscoveryResponse(HttpStatusCode.OK, "Get Banners Discovery Succeed", dataResponse);
        }
    }
}
