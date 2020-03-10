using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.interfaces
{
    public interface IProfileEdit
    {
        public void SaveUserProperties(string firstName, string lastName, string address, string userId);
    }
}
