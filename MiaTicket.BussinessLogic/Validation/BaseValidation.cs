using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiaTicket.BussinessLogic.Validation
{
    public abstract class BaseValidation<T> where T : class
    {
        private protected string _message;
        private protected T _value;

        public bool IsValid => string.IsNullOrEmpty(_message);
        public string Message => _message;

        public BaseValidation(T input)
        {
            _message = string.Empty;
            _value = input;
        }


        public abstract void Validate();
    }
}
