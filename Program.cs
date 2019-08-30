using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using SnmpSharpNet;

using Lextm.SharpSnmpLib;
using Lextm.SharpSnmpLib.Mib;

using Automatak.DNP3.Adapter;
using Automatak.DNP3.Interface;

using Gemini.Utility.DNP3;
using Gemini.Utility.SNMP;

namespace Gemini
{
    class Program
    {
        static void Main(string[] args)
        {
            Core.Start();

            SNMPTarget retAbelSantana = new SNMPTarget("10.7.5.150");
            retAbelSantana.InsertObject("batteryTemperaturesValue", ".1.3.6.1.4.1.12148.10.10.7.5.0");
            SnmpV2Packet result = retAbelSantana.GetUpdate();
            IPEndpoint myEndPoint = new IPEndpoint("0.0.0.0", 20500);
            RTU rtu = new RTU("Teste DNP3", myEndPoint, 2, 1);
            ChangeSet changeSet;
            int currentValue;

            while (true)
            {
                if (result != null)
                {
                    changeSet = new ChangeSet();
                    if (result.Pdu.ErrorStatus != 0)
                    {
                        Console.WriteLine("Error in SNMP reply. Error {0} index {1}",
                            result.Pdu.ErrorStatus,
                            result.Pdu.ErrorIndex);
                    }
                    else
                    {
                        currentValue = Convert.ToInt32(result.Pdu.VbList[0].Value.ToString());
                        changeSet.Update(new Analog(currentValue, 1, DateTime.Now), 0);
                        Console.WriteLine("Atualizando valor da analógica para " + currentValue);
                        rtu.Outstation.Load(changeSet);
                    }
                }
                else
                {
                    Console.WriteLine("Erro SNMP Nulo");
                }
                Thread.Sleep(15000);
            }
        }
    }
}
