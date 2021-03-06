﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EC_Endpoint_Client.BrokerServiceStreamed;
using EC_Endpoint_Client.Classes;
using EC_Endpoint_Client.Classes.Shipments.ServiceEngine.BrokerService;

namespace EC_Endpoint_Client.Functionality.EndPoints.ServiceEngine
{
    class BrokerServiceStreamedEndPointFunction : EndPointFunctionalityBase
    {
        private BrokerServiceExternalECStreamedClient GenerateProxy(BaseShipment shipment, bool setInspectorBehavior = true)
        {
            return
                base.GenerateProxy<BrokerServiceExternalECStreamedClient, IBrokerServiceExternalECStreamed>(
                    shipment.EndpointName, shipment.Certificate, setInspectorBehavior);
        }

        public void Test(BaseShipment shipment)
        {
            var client = GenerateProxy(shipment);
            OperationContext = "BrokerServiceStreamedTest";
            client.Test();
        }

        public UploadFileStreamedResult UploadFileStreamed(UploadFileStreamedShipment shipment)
        {
            var client = GenerateProxy(shipment, false);

            UploadFileStreamedResult receipt = new UploadFileStreamedResult();
            using (FileStream fs = new FileStream(shipment.FilePath, FileMode.Open))
            {
                OperationContext = "UploadFileStreamed";
                client.UploadFileStreamedEC(shipment.FileName, shipment.Password, shipment.FileReference, shipment.Reportee, shipment.Username, fs,
                    out receipt._ParentReceiptID, out receipt._ReceiptHistory, out receipt._ReceiptID, out receipt._ReceiptStatusCode, out receipt._ReceiptText, out receipt._ReceiptTypeName);
            }

            return receipt;
        }

        public byte[] DownloadFileStreamed(DownloadFileStreamedShipment shipment)
        {
            var client = GenerateProxy(shipment);
            OperationContext = "DownloadFileStreamed";
            return client.DownloadFileStreamedEC(shipment.Username, shipment.Password, shipment.FileReference, shipment.Reportee);
        }
    }
}
