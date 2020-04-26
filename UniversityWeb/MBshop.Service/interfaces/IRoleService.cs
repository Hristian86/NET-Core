using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MBshop.Service.OutputModels;

namespace MBshop.Service.interfaces
{
    public interface IRoleService
    {
        Task<string> AssignRole(string roleId, string userId, string userName, string userRole);

        List<OutputRoleAssignModel> GetRoles();
    }
}
