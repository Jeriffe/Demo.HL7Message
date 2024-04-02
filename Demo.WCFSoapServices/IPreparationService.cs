using Demo.HL7MessageParser.Models;
using System.ServiceModel;

namespace Demo.WCFSoapServices
{
    [ServiceContract(Namespace = "http://biz.dms.pms.model.ha.org.hk/")]
    public interface IPreparationService
    {
        [OperationContract(Action = "http://biz.dms.pms.model.ha.org.hk/getPreparation")]
        GetPreparationResponse getPreparation(GetPreparationRequest request);
    }
}
