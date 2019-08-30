using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gemini.ServiceManager
{
    interface IPoint<T>
    {
        T Value { get; set; }
    }
}
