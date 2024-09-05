using MiaTicket.DataAccess.Model;

namespace MiaTicket.BussinessLogic.Model
{
    public class BasePagedResponse<T>
    {
        public List<T> Items { get; set; }

        public PaginationDto Pagination { get; set; }

        public BasePagedResponse(int currentPage, int currentSize)
        {
            Items = new List<T>();
            Pagination = new PaginationDto()
            {
                CurrentPage = currentPage,
                CurrentSize = currentSize
            };
        }
    }
}
