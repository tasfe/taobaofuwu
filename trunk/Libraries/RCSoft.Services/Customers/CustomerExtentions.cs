using System;
using System.Linq;
using RCSoft.Core.Domain.Customers;

namespace RCSoft.Services.Customers
{
    public static class CustomerExtentions
    {
        public static bool IsUserLogin(this Customer customer)
        {
            if (customer == null)
                return false;
            else
                return true;
        }
    }
}
