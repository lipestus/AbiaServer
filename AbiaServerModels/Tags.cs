using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbiaServerModels
{
    public class Tags
    {
        public enum MessageTypes : ushort
        {
            CreateAccountRequest = 1,
            CreateAccountResponse = 2,
            LoginRequest = 3,
            LoginResponse = 4
        }
    }
}
