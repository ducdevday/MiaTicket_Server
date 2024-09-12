using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Request
{
    public class SearchEventRequest : BaseApiRequest
    {
        private string _keyword = string.Empty;
        private string _location = string.Empty;
        private string _categories = string.Empty;
        private string _priceRanges = string.Empty;
        private int _sortBy = 0;
        public string Keyword
        {
            get => _keyword;
            set => _keyword = value;
        }
        public string Location
        {
            get => _location;
            set => _location = value;
        }
        public string Categories
        {
            get => _categories;
            set => _categories = value;
        }
        public string PriceRanges
        {
            get => _priceRanges;
            set => _priceRanges = value;
        }
        public int SortBy
        {
            get => _sortBy;
            set => _sortBy = value;
        }
    }
}
