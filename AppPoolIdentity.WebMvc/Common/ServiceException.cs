using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppPoolIdentity.WebMvc.Common
{
    public class ServiceException : ApplicationException
    {
        public ServiceException():base()
        {
                
        }

        public ServiceException(string message) : base(message)
        {

        }
    }
}