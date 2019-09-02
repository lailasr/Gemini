using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;

using SnmpSharpNet;

namespace Gemini.Utility.SNMP
{
    public class SNMPTarget
    {
        public IPAddress Address { get; private set; }
        public UdpTarget Target { get; private set; }
        public Pdu Pdu { get; set; }
        public Dictionary<string,string> Database { get; private set; }
        
        public SNMPTarget(string clientAddress)
        {
            Address = IPAddress.Parse(clientAddress);
            Target = new UdpTarget(Address, 161, 2000, 1);
            Pdu = new Pdu(PduType.Get);
            Database = new Dictionary<string, string>();
        }
        
        public void InsertObject(string labelName, string objectId)
        {
            if(!Database.ContainsKey(labelName))
            {
                Database.Add(labelName, objectId);
                Pdu.VbList.Add(objectId);
            }
        }

        public void UpdateVbList()
        {
            Pdu.VbList.Clear();
            foreach(KeyValuePair<string,string> k in Database)
            {
                Pdu.VbList.Add(k.Value);
            }
        }

        public SnmpV2Packet GetUpdate()
        {
			return (SnmpV2Packet)Target.Request(Pdu, Core.SNMPParameters);
        }
		
		//Necessario para LoadDatabase(filepath)
		public class Oid
		{
			public string objectId { get; set; }
		}

		/* Metodo criado para carregar lista de OIDs a serem monitorados a partir de arquivo .csv
		Formato do .csv (deve conter o cabeçalho exatamente com estes nomes):
		  labelName,objectId
		  batteryTemperaturesValue,.1.3.6.1.4.1.12148.10.10.7.5.0
		  rectifiersCurrentValue,.1.3.6.1.4.1.12148.10.5.2.5.0
		  ...
		Chama UpdateVbList() para atualizar o Pdu. 
		*/
		public void LoadDatabase(string filepath)
		{
			using (var textreader = new StreamReader(filepath))
			using (var reader = new CsvReader(textreader))
			{
				 while (reader.Read())
				{
					string labelName = reader.GetField<string>("labelName");
					Oid oid = reader.GetRecord<Oid>();
					this.InsertObject(labelName,oid.objectId);
				}
			}
			this.UpdateVbList();
		}
	}
}
