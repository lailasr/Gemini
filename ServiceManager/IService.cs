using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gemini.ServiceManager
{
    interface IService
    {
        int Id { get; }
        string Label { get; }
        string Oid { get; }
        GeminiServices ServiceType { get; }
        
    }
}
