using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbiaServerModels
{
    public class Tags
    {
        public enum TagType
        {
            CREATE_USER = 0,
            USER_LOGIN = 1,
            MESSAGE = 4,
            TEST = 5
        }
    }
}
