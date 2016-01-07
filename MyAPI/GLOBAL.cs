using MyAPI.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAPI
{
    public static class GLOBAL
    {
        public static TransplayContext db = new TransplayContext();
    }
}
