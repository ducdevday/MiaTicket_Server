using MiaTicket.Data;
using MiaTicket.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.DataAccess.Data
{
    public interface IBannerData
    {
        Task<Banner> CreateBanner(int eventId, string videoUrl);
        Task<List<Banner>> GetBannerList();
    }


    public class BannerData : IBannerData
    {
        private readonly MiaTicketDBContext _context;

        public BannerData(MiaTicketDBContext context)
        {
            _context = context;
        }

        public Task<Banner> CreateBanner(int eventId, string videoUrl)
        {

            return Task.FromResult(_context.Banner.Add(new Banner() {
                EventId = eventId,
                VideoUrl = videoUrl
            }).Entity);
        }

        public Task<List<Banner>> GetBannerList()
        {
            var banners = _context.Banner.Include(b =>b.Event).ThenInclude(e => e.ShowTimes).ThenInclude(st => st.Tickets).ToList();
            return Task.FromResult(banners);
        }
    }
}
