﻿using EC_Endpoint_Client.Classes.Shipments.ServiceEngine.LookUp;
using EC_Endpoint_Client.LookUp;
using EC_Endpoint_Client.Classes;

namespace EC_Endpoint_Client.Functionality.EndPoints.ServiceEngine
{
    class LookUpEndPointFunction : EndPointFunctionalityBase
    {
        private LookUpExternalECClient GenerateProxy(BaseShipment shipment)
        {
            return base.GenerateProxy<LookUpExternalECClient, ILookUpExternalEC>(shipment.EndpointName,
                shipment.Certificate);
        }

        public void Test(BaseShipment shipment)
        {
            var client = GenerateProxy(shipment);
            OperationContext = "LookUpTest";
            client.Test();
        }

        public string LookUp(LookUpShipment shipment)
        {
            var client = GenerateProxy(shipment);
            OperationContext = "LookUp";
            return client.ExecuteLookUp(shipment.Username, shipment.Password, shipment.LookUpRequest);
        }
    }
}
