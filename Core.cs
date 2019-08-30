using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lextm.SharpSnmpLib;

using Automatak.DNP3.Adapter;
using Automatak.DNP3.Interface;
using SnmpSharpNet;

namespace Gemini
{
    public static class Core
    {
        public static IDNP3Manager DNP3Manager { get; set; }
        public static AgentParameters SNMPParameters { get; private set; }
        
        public static void Start()
        {
            SnmpSharpNet.OctetString community = new SnmpSharpNet.OctetString("cr");
            SNMPParameters = new AgentParameters(community);
            SNMPParameters.Version = SnmpVersion.Ver2;
            if (SNMPParameters != null)
            {
                Console.WriteLine("Parâmetros iniciados com sucesso");
            }

            DNP3Manager = DNP3ManagerFactory.CreateManager(4, new PrintingLogAdapter());
        }

        public static AgentParameters GetAgentParameters()
        {
            if(SNMPParameters != null)
            {
                return SNMPParameters;
            }
            else
            {
                throw new NullReferenceException("Parâmetro de SNMP Nulo");
            }
        }
    }
}
