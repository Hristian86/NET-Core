using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MBshop.Service.interfaces
{
    public interface ILogService
    {
        Task LoggedUser(string userName);
    }
}
