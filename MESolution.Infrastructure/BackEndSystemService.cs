using Azure.Core;
using MESolution.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

using MESolution.Core.Entities;

namespace MESolution.Infrastructure
{
    public class BackEndSystemService : IBackEndSystemService
    {
        public async Task sendRequestToBackEnd(FinancialAidApplication request, string directory)
        {
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add("ns0", "http://EAISolution.Request");
            string xml = "l";

            await File.WriteAllTextAsync(directory + "Request" + DateTime.Now.Ticks + ".xml",xml);

        }

    }
}
