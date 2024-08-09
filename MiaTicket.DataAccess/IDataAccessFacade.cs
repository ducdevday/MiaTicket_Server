﻿using MiaTicket.Data;
using MiaTicket.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.DataAccess
{
    public interface IDataAccessFacade
    {
        public IBannerData BannerData { get; }
        public ICategoryData CategoryData { get; }
        public IEventData EventData { get; }
        public IOrderData OrderData { get; }
        public IShowTimeData ShowTimeData { get; }
        public ITicketData TicketData { get; }
        public IUserData UserData { get; }
        public IVoucherData VoucherData { get; }
        public IVoucherFixedAmountData VoucherFixedAmountData { get; }
        public IVoucherPercentage VoucherPercentage {  get; }
    }

    public class DataAccessFacade : IDataAccessFacade
    {
        private readonly MiaTicketDBContext _context = new MiaTicketDBContext();

        public DataAccessFacade()
        {

        }

        //public DataAccessFacade(MiaTicketDBContext context)
        //{
        //    _context = context;
        //}

        private BannerData _bannerData;
        public IBannerData BannerData {
            get {
                _bannerData ??= new BannerData(_context);
                return _bannerData;
            }

        }

        private CategoryData _categoryData;

        public ICategoryData CategoryData {
            get {
                _categoryData ??= new CategoryData(_context);
                return _categoryData;
            }
        }
        private EventData _eventData;
        public IEventData EventData {
            get
            {
                _eventData ??= new EventData(_context);
                return _eventData;
            }
        }
        private OrderData _orderData;
        public IOrderData OrderData {
            get {
                _orderData ??= new OrderData(_context);
                return _orderData;
            }
        }
        private ShowTimeData _showTimeData;
        public IShowTimeData ShowTimeData {
            get
            {
                _showTimeData ??= new ShowTimeData(_context);
                return _showTimeData;
            }
        
        }
        private TicketData _ticketData;
        public ITicketData TicketData {
            get {
                _ticketData ??= new TicketData(_context);
                return _ticketData;
            
            }
        }
        private UserData _userData;
        public IUserData UserData
        {
            get {
                _userData ??= new UserData(_context);
                return _userData;
            }
        }

        public VoucherData _voucherData;
        public IVoucherData VoucherData
        {
            get {
                _voucherData ??= new VoucherData(_context);
                return _voucherData;
            }
        }

        public VoucherFixedAmountData _voucherFixedAmountData;
        
        public IVoucherFixedAmountData VoucherFixedAmountData {
            get { 
                _voucherFixedAmountData ??= new VoucherFixedAmountData(_context);
                return _voucherFixedAmountData;
            }        
        }

        public VoucherPercentage _voucherPercentage;
        public IVoucherPercentage VoucherPercentage {
            get {
                _voucherPercentage ??= new VoucherPercentage(_context);
                return _voucherPercentage;
            }   
        }
    }
}
