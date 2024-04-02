using Demo.HL7MessageParser.Common;
using Demo.HL7MessageParser.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Demo.WCFSoapServices.ClientConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //https://www.c-sharpcorner.com/UploadFile/788083/how-to-implement-message-contract-in-wcf/
            ConsumerPreparationService();

            //ConsumerDrugMasterService();
        }
        private static void ConsumerPreparationService()
        {
            using (ChannelFactory<IPreparationService> channelFactory = new ChannelFactory<IPreparationService>("PreparationService"))
            {
                var proxy = channelFactory.CreateChannel();
                using (proxy as IDisposable)
                {
                    var request = new GetPreparationRequest { Arg0 = new Arg0 { ItemCode = "DEMO02" } };
                    var result = proxy.getPreparation(request);

                }

            }
        }
        /*
<s:Envelope xmlns:a="http://www.w3.org/2005/08/addressing" xmlns:s="http://www.w3.org/2003/05/soap-envelope">
  <s:Header>
    <a:Action s:mustUnderstand="1">http://biz.dms.pms.model.ha.org.hk/getPreparation</a:Action>
    <a:MessageID>urn:uuid:8d92723a-a086-49f6-82e8-f79df43d5c4a</a:MessageID>
    <a:ReplyTo>
      <a:Address>http://www.w3.org/2005/08/addressing/anonymous</a:Address>
    </a:ReplyTo>
  </s:Header>
  <s:Body>
    <getPreparation xmlns="http://biz.dms.pms.model.ha.org.hk/">
      <arg0 xmlns:i="http://www.w3.org/2001/XMLSchema-instance" xmlns="">
        <CostIncluded>false</CostIncluded>
        <DispHospCode i:nil="true" />
        <DispWorkstore i:nil="true" />
        <DrugScope i:nil="true" />
        <FormCode i:nil="true" />
        <HqFlag>false</HqFlag>
        <ItemCode>DEMO02</ItemCode>
        <PasSpecialty i:nil="true" />
        <PasSubSpecialty i:nil="true" />
        <SaltProperty i:nil="true" />
        <SpecialtyType i:nil="true" />
        <TrueDisplayname i:nil="true" />
      </arg0>
    </getPreparation>
  </s:Body>
</s:Envelope>
         */
        /*
<s:Envelope xmlns:s="http://www.w3.org/2003/05/soap-envelope" xmlns:a="http://www.w3.org/2005/08/addressing" xmlns:u="http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd">
  <s:Header>
    <a:Action s:mustUnderstand="1" u:Id="_2">http://biz.dms.pms.model.ha.org.hk/IPreparationService/getPreparationResponse</a:Action>
    <h:WorkContext u:Id="_3" xmlns:h="http://oracle.com/weblogic/soap/workarea/" xmlns:u="http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd">rO0ABXdVABx3ZWJsb2dpYy5hcHAuUE1TX0RNU19TVkNfQVBQAAAA1gAAACN3ZWJsb2dpYy53b3JrYXJlYS5TdHJpbmdXb3JrQ29udGV4dAAIMS4xMC40LjYAAA==</h:WorkContext>
    <a:RelatesTo u:Id="_5">urn:uuid:3274f86e-cc91-4bc2-8028-47b64e2b418e</a:RelatesTo>
    <o:Security s:mustUnderstand="1" xmlns:o="http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd">
      <u:Timestamp u:Id="uuid-368fe21a-5fbb-408f-b11b-75d3a58a8057-7">
        <u:Created>2024-04-02T05:11:51.325Z</u:Created>
        <u:Expires>2024-04-02T05:16:51.325Z</u:Expires>
      </u:Timestamp>
      <c:DerivedKeyToken u:Id="uuid-368fe21a-5fbb-408f-b11b-75d3a58a8057-5" xmlns:c="http://schemas.xmlsoap.org/ws/2005/02/sc">
        <o:SecurityTokenReference>
          <o:Reference URI="urn:uuid:1694fc12-b3f2-469e-8d21-3823ff1d369a" ValueType="http://schemas.xmlsoap.org/ws/2005/02/sc/sct" />
        </o:SecurityTokenReference>
        <c:Offset>0</c:Offset>
        <c:Length>24</c:Length>
        <c:Nonce>vpbZ6/MY3B9AN4WDSF1Fhg==</c:Nonce>
      </c:DerivedKeyToken>
      <c:DerivedKeyToken u:Id="uuid-368fe21a-5fbb-408f-b11b-75d3a58a8057-6" xmlns:c="http://schemas.xmlsoap.org/ws/2005/02/sc">
        <o:SecurityTokenReference>
          <o:Reference URI="urn:uuid:1694fc12-b3f2-469e-8d21-3823ff1d369a" ValueType="http://schemas.xmlsoap.org/ws/2005/02/sc/sct" />
        </o:SecurityTokenReference>
        <c:Nonce>c7tZxVjKh/ynAV89kqCkhA==</c:Nonce>
      </c:DerivedKeyToken>
      <e:ReferenceList xmlns:e="http://www.w3.org/2001/04/xmlenc#">
        <e:DataReference URI="#_1" />
        <e:DataReference URI="#_4" />
        <e:DataReference URI="#_6" />
      </e:ReferenceList>
      <e:EncryptedData Id="_6" Type="http://www.w3.org/2001/04/xmlenc#Element" xmlns:e="http://www.w3.org/2001/04/xmlenc#">
        <e:EncryptionMethod Algorithm="http://www.w3.org/2001/04/xmlenc#aes256-cbc" />
        <KeyInfo xmlns="http://www.w3.org/2000/09/xmldsig#">
          <o:SecurityTokenReference>
            <o:Reference ValueType="http://schemas.xmlsoap.org/ws/2005/02/sc/dk" URI="#uuid-368fe21a-5fbb-408f-b11b-75d3a58a8057-6" />
          </o:SecurityTokenReference>
        </KeyInfo>
        <e:CipherData>
          <e:CipherValue>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</e:CipherValue>
        </e:CipherData>
      </e:EncryptedData>
    </o:Security>
  </s:Header>
  <s:Body u:Id="_0">
    <getPreparationResponse xmlns="http://biz.dms.pms.model.ha.org.hk/">
      <return xmlns="" xmlns:i="http://www.w3.org/2001/XMLSchema-instance">
        <AverageUnitPrice>14.25</AverageUnitPrice>
        <BaseUnit>UNIT</BaseUnit>
        <CorpDrugPrice>14.25</CorpDrugPrice>
        <DangerousDrug>N</DangerousDrug>
        <DrugMds>
          <DuplicateFlag>false</DuplicateFlag>
          <GcnSeqno>16491</GcnSeqno>
          <GroupGcnSeqno>0</GroupGcnSeqno>
          <GroupMoeCheckFlag>N</GroupMoeCheckFlag>
          <GroupRoutedGeneric>6292938</GroupRoutedGeneric>
          <GroupRouteformGeneric>3740</GroupRouteformGeneric>
          <GroupSingleIngred>Y</GroupSingleIngred>
          <HiclSeqno>1482</HiclSeqno>
          <MoeCheckFlag>Y</MoeCheckFlag>
          <RoutedGeneric>6292938</RoutedGeneric>
          <RouteformGeneric>3740</RouteformGeneric>
        </DrugMds>
        <FormCode>MIN</FormCode>
        <FormRank>2305</FormRank>
        <FullRouteDesc i:nil="true" />
        <GroupBaseUnit>UNIT</GroupBaseUnit>
        <IsCommonDose>false</IsCommonDose>
        <ItemCode>DEMO02</ItemCode>
        <MoeDesc>EYE MINIMS</MoeDesc>
        <MoeRouteFormDesc>EYE MINIMS</MoeRouteFormDesc>
        <PmsFmStatus>F</PmsFmStatus>
        <Ppmi>N</Ppmi>
        <PreparationDesc>1% 0.5 ML</PreparationDesc>
        <PrivateDrugPatTypeCharge>
          <AddedCharge>0.0</AddedCharge>
          <HandleCharge>105.0</HandleCharge>
          <MarkupFactor>1.01</MarkupFactor>
        </PrivateDrugPatTypeCharge>
        <PublicCdp>14.3925</PublicCdp>
        <PublicDrugPatTypeCharge>
          <AddedCharge>0.0</AddedCharge>
          <HandleCharge>105.0</HandleCharge>
          <MarkupFactor>1.01</MarkupFactor>
        </PublicDrugPatTypeCharge>
        <RouteDesc>EYE</RouteDesc>
        <RouteFormSortSeq>999</RouteFormSortSeq>
        <RouteSortSeq>999</RouteSortSeq>
        <SaltProperty>HCL</SaltProperty>
        <SfiCategory>N</SfiCategory>
        <SpecRestrict>*</SpecRestrict>
        <Strength>1%</Strength>
        <StrengthUnit>%</StrengthUnit>
        <StrengthValue>1.0</StrengthValue>
        <TrueDisplayname>AMETHOCAINE</TrueDisplayname>
        <VolumeUnit>ML</VolumeUnit>
        <VolumeValue>0.5</VolumeValue>
      </return>
    </getPreparationResponse>
  </s:Body>
</s:Envelope>
         */
        private static void ConsumerDrugMasterService()
        {
            using (ChannelFactory<IDrugMasterService> channelFactory = new ChannelFactory<IDrugMasterService>("DrugMasterService"))
            {
                var drugMasterServiceProxy = channelFactory.CreateChannel();
                using (drugMasterServiceProxy as IDisposable)
                {
                    var str = @"<biz:getDrugMdsPropertyHq xmlns:biz=""http://biz.dms.pms.model.ha.org.hk/"">
  <arg0>
    <itemCode>DEMO01</itemCode>
  </arg0 >
</biz:getDrugMdsPropertyHq>";

                    using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(str)))
                    {
                        var argElement = XDocument.Load(ms).Descendants("arg0").First();

                        var arg0 = XmlHelper.XmlDeserialize<Arg>(argElement.ToString());

                        var request = new GetDrugMdsPropertyHqRequest { Arg0 = arg0 };

                        var result = drugMasterServiceProxy.getDrugMdsPropertyHq(request);

                    }

                }
            }
        }
    }
}
