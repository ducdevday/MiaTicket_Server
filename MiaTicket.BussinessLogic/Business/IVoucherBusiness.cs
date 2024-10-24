using AutoMapper;
using Azure.Core;
using MiaTicket.BussinessLogic.Model;
using MiaTicket.BussinessLogic.Request;
using MiaTicket.BussinessLogic.Response;
using MiaTicket.BussinessLogic.Validation;
using MiaTicket.Data.Entity;
using MiaTicket.DataAccess;
using System.Net;

namespace MiaTicket.BussinessLogic.Business
{
    public interface IVoucherBusiness
    {
        Task<CreateVoucherResponse> CreateVoucher(Guid userId, CreateVoucherRequest request);
        Task<GetMyVouchersResponse> GetMyVouchers(Guid userId, int eventId, string keyword);
        Task<GetVouchersDiscoveryResponse> GetVouchersDiscovery(int eventId);
        Task<UpdateVoucherResponse> UpdateVoucher(Guid userId, int voucherId, UpdateVoucherRequest request);
        Task<DeleteVoucherResponse> DeleteVoucher(Guid userId, int voucherId);
        Task<SearchVoucherResponse> SearchVoucher(SearchVoucherRequest request);
    }

    public class VoucherBusiness : IVoucherBusiness
    {
        private readonly IDataAccessFacade _context;
        private readonly IMapper _mapper;

        public VoucherBusiness(IDataAccessFacade context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CreateVoucherResponse> CreateVoucher(Guid userId, CreateVoucherRequest request)
        {
            var validation = new CreateVoucherValidation(request);
            validation.Validate();
            if (!validation.IsValid) return new CreateVoucherResponse(HttpStatusCode.BadRequest, validation.Message, false);

            EventOrganizer? evtOrganizer = await _context.EventOrganizerData.GetEventById(request.EventId, userId);
            if(evtOrganizer == null) return new CreateVoucherResponse(HttpStatusCode.NotFound, "Not Found", false);

            bool isVoucherCodeExist = await _context.VoucherData.IsVoucherCodeExist(request.Code);
            if (isVoucherCodeExist) return new CreateVoucherResponse(HttpStatusCode.Conflict, "Code Is Already Exist", false);

            Voucher voucher = _mapper.Map<Voucher>(request);

            Voucher addedVoucher = await _context.VoucherData.CreateVoucher(voucher);
            await _context.Commit();
            return new CreateVoucherResponse(HttpStatusCode.OK, "Create Voucher Success", true);
        }

        public async Task<UpdateVoucherResponse> UpdateVoucher(Guid userId, int voucherId, UpdateVoucherRequest request)
        {
            var validation = new UpdateVoucherValidation(request);
            validation.Validate();
            if (!validation.IsValid) return new UpdateVoucherResponse(HttpStatusCode.BadRequest, validation.Message, false);

            EventOrganizer? evtOrganizer = await _context.EventOrganizerData.GetEventById(request.EventId, userId);
            if (evtOrganizer == null) return new UpdateVoucherResponse(HttpStatusCode.NotFound, "Not Found", false);

            Voucher? voucher = await _context.VoucherData.GetVoucherById(voucherId);
            if (voucher == null) return new UpdateVoucherResponse(HttpStatusCode.NotFound, "Voucher Not Found", false);

            bool isVoucherCodeExist = await _context.VoucherData.IsVoucherCodeExist(request.Code);
            if (isVoucherCodeExist && request.Code != voucher.Code) return new UpdateVoucherResponse(HttpStatusCode.Conflict, "Code Is Already Exist", false);

            _mapper.Map(request, voucher);

            await _context.VoucherData.UpdateVoucher(voucher);
            await _context.Commit();
            return new UpdateVoucherResponse(HttpStatusCode.OK, "Update Voucher Success", true);
        }

        public async Task<DeleteVoucherResponse> DeleteVoucher(Guid userId, int voucherId)
        {
            Voucher? voucher = await _context.VoucherData.GetVoucherById(voucherId);
            if (voucher == null) return new DeleteVoucherResponse(HttpStatusCode.NotFound, "Voucher Not Found", false);

            EventOrganizer? evtOrganizer = await _context.EventOrganizerData.GetEventById(voucher.Event.Id, userId);
            if (evtOrganizer == null) return new DeleteVoucherResponse(HttpStatusCode.NotFound, "Not Found", false);

            await _context.VoucherData.DeleteVoucher(voucher);
            await _context.Commit();
            return new DeleteVoucherResponse(HttpStatusCode.OK, "Delete Voucher Success", true);
        }

        public async Task<GetMyVouchersResponse> GetMyVouchers(Guid userId, int eventId, string keyword)
        {
            EventOrganizer? evtOrganizer = await _context.EventOrganizerData.GetEventById(eventId, userId);
            if (evtOrganizer == null) return new GetMyVouchersResponse(HttpStatusCode.NotFound, "Not Found", [], string.Empty);

            List<Voucher> vouchers = await _context.VoucherData.SearchVouchers(eventId, keyword, out string eventName);
            List<VoucherDto> vouchersDto = _mapper.Map<List<VoucherDto>>(vouchers);

            return new GetMyVouchersResponse(HttpStatusCode.OK, "Get Vouchers Success", vouchersDto, eventName);
        }

        public async Task<GetVouchersDiscoveryResponse> GetVouchersDiscovery(int eventId)
        {
            Event? evt = await _context.EventData.GetEventById(eventId);
            if (evt == null) return new GetVouchersDiscoveryResponse(HttpStatusCode.NotFound, "Event Not Found", []);

            List<Voucher> vouchers = await _context.VoucherData.GetVoucherByEventId(eventId);
            List<VoucherDiscoveryDto> voucherDiscoveries = _mapper.Map<List<VoucherDiscoveryDto>>(vouchers);

            return new GetVouchersDiscoveryResponse(HttpStatusCode.OK, "Get Vouchers Success", voucherDiscoveries);
        }

        public async Task<SearchVoucherResponse> SearchVoucher(SearchVoucherRequest request)
        {
            Voucher? voucher = await _context.VoucherData.FindVoucher(request.EventId, request.Code);
            if (voucher == null) return new SearchVoucherResponse(HttpStatusCode.NotFound, "Voucher Not Found", null);
            if (voucher.AppliedQuantity >= voucher.InitQuantity) return new SearchVoucherResponse(HttpStatusCode.Conflict, "Voucher Is Not Available", null);
            if (voucher.MinQuantityPerOrder != null && voucher.MinQuantityPerOrder > request.TotalTicketQuantityOfOrder) return new SearchVoucherResponse(HttpStatusCode.Conflict, $"Min Quantity Ticket To Use This Voucher Is {voucher.MinQuantityPerOrder}", null);
            if (voucher.MaxQuantityPerOrder != null && voucher.MaxQuantityPerOrder < request.TotalTicketQuantityOfOrder) return new SearchVoucherResponse(HttpStatusCode.Conflict, $"Max Quantity Ticket To Use This Voucher Is {voucher.MaxQuantityPerOrder}", null);

            SearchVoucherDto searchVoucherDto = _mapper.Map<SearchVoucherDto>(voucher);
            return new SearchVoucherResponse(HttpStatusCode.OK, "Get Vouchers Success", searchVoucherDto);
        }
    }
}
