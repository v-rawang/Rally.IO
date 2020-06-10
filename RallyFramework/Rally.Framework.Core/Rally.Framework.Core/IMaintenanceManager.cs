using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rally.Framework.Core.DomainModel;

namespace Rally.Framework.Core
{
    public interface IMaintenanceManager
    {
        string NewOrder(MaintenanceOrder Order);

        string UpdateOrder(string OrderID, MaintenanceOrder Order);

        MaintenanceOrder GetOrder(string OrderID);

        MaintenanceOrder GetOrder<T>(string OrderID, out T ExtraData, Func<object,T> ExtensionFunction);

        IList<MaintenanceOrder> GetInstrumentMaintenanceOrders(string InstrumentID);

        IList<MaintenanceOrder> GetInstrumentMaintenanceOrders<T>(string InstrumentID, out T ExtraData, Func<object,T> ExtensionFunction);

        IList<MaintenanceOrderLineItem> GetMaintenanceOrderLineItems(string OrderID);

        IList<MaintenanceAttachment> GetMaintenanceOrderAttachments(string OrderID);

        IList<MaintenanceAttachment> GetMaintenanceOrderAttachments<T>(string OrderID, out T ExtraData, Func<object,T> ExtensionFunction);

        string AddInstrumentFault(InstrumentFault Fault);

        List<InstrumentFault> GetInstrumentFaults(string InstrumentID);

        InstrumentFault GetFault(string FaultID);

        string AddtMaintenanceOrderLineItem(MaintenanceOrderLineItem OrderLineItem);

        MaintenanceOrderLineItem UpdatetMaintenanceOrderLineItem(string LineItemID, MaintenanceOrderLineItem OrderLineItem);

        string AddMaintenanceOrderAttachment(MaintenanceAttachment Attachment);

        string GenerateMaintenanceOrderID(int Increment);

        string GenerateMaintenanceOrderLineItemID(int Increment);

        string GenerateMaintenanceOrderAttachmentID(int Increment);

        string GenerateInstrumentFaultID(int Increment);

        System.Data.DataTable GetModuleDataTable();

        System.Data.DataTable GetModuleOrderLineltemDataTable(string strId);

        System.Data.DataTable GetCXModuleDataTable(string strTime1, string strTime2, string strDanHao, string strXuLieHao, string strJieGuo);

        string DeleteMaintenanceOrder(string ID);

        string DeleteMaintenanceOrderLineitems(string ID);

        string DeleteMaintenanceOrderLineitems1(string ID);

        string AddMaintenanceOrderAttachment1(string strWeiXiuId, string strTime, string strFID);

        string DeleteMaintenanceOrderAttachment(string ID);
    }
}
