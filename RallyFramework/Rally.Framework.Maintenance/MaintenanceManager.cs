using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rally.Lib.Persistence.Core;
using Rally.Framework.Core;
using Rally.Framework.Core.DomainModel;

namespace Rally.Framework.Maintenance
{
    public class MaintenanceManager : IMaintenanceManager
    {
        public MaintenanceManager(IDMLOperable DMLOperable, DBTypeEnum DBType)
        {
            this.dmlOperable = DMLOperable;
            this.dBType = DBType;
        }

        private IDMLOperable dmlOperable;
        private DBTypeEnum dBType;

        public static IMaintenanceManager NewInstance(IDMLOperable DMLOperable, DBTypeEnum DBType)
        {
            return new MaintenanceManager(DMLOperable, DBType);
        }

        public string AddInstrumentFault(InstrumentFault InstrumentFault)
        {
            //if (InstrumentFault == null || InstrumentFault.Fault == null|| string.IsNullOrEmpty(InstrumentFault.Fault.ID))
            //{
            //    throw new IDNullException("仪器故障ID不可为空！");
            //}

            string sqlCommandText = ModuleConfiguration.SQL_CMD_InsertInstrumentFault;

            this.dmlOperable.ExeSql(sqlCommandText, new Dictionary<string, object>() {
                //{ "@ID", long.Parse(InstrumentFault.Fault.ID)},
                { "@InstrumentID", InstrumentFault.Instrument.ID},
                { "@FaultTime", InstrumentFault.FaultTime},
                { "@FaultType", InstrumentFault.Fault.FaultType},
                { "@FaultCode", InstrumentFault.Fault.FaultCode},
                { "@FaultMessage", InstrumentFault.Fault.FaultMessage}
            });

            return GenerateInstrumentFaultID(0); //InstrumentFault.Fault.ID;
        }

        public InstrumentFault GetFault(string FaultID)
        {
            InstrumentFault instrumentFault = null;

            string sqlCommandText = ModuleConfiguration.SQL_CMD_SelectFaultById;

            var dbResult = this.dmlOperable.ExeReader(sqlCommandText, new Dictionary<string, object>() { { "@ID", long.Parse(FaultID)} });

            if (dbResult != null && dbResult.Count == 1)
            {
                instrumentFault = new InstrumentFault() {
                    FaultTime = (DateTime?)dbResult[0]["FaultTime"],
                    Fault = new Fault() {
                        ID = dbResult[0]["ID"].ToString(),
                        FaultType = dbResult[0]["FaultType"] == null ? 0 : (int?)dbResult[0]["FaultType"],
                        FaultCode = (string)dbResult[0]["FaultCode"],
                        FaultMessage = (string)dbResult[0]["FaultMessage"]},
                    Instrument = new Instrument() {ID = dbResult[0]["InstrumentID"].ToString()}
                };
            }

            return instrumentFault;
        }

        public List<InstrumentFault> GetInstrumentFaults(string InstrumentID)
        {
            List<InstrumentFault> instrumentFaults = null;

            string sqlCommandText = ModuleConfiguration.SQL_CMD_SelectInstrumentFault;

            var dbResult = this.dmlOperable.ExeReader(sqlCommandText, new Dictionary<string, object>() { { "@InstrumentID", long.Parse(InstrumentID) } });

            if (dbResult != null && dbResult.Count > 0)
            {
                instrumentFaults = new List<InstrumentFault>();

                for (int i = 0; i < dbResult.Count; i++)
                {
                    instrumentFaults.Add(new InstrumentFault()
                    {
                        FaultTime = (DateTime?)dbResult[i]["FaultTime"],
                        Fault = new Fault(){
                            ID = dbResult[i]["ID"].ToString(),
                            FaultType = dbResult[0]["FaultType"] == null ? 0 : (int?)dbResult[i]["FaultType"],
                            FaultCode = (string)dbResult[i]["FaultCode"],
                            FaultMessage = (string)dbResult[i]["FaultMessage"]},
                        Instrument = new Instrument() { ID = dbResult[i]["InstrumentID"].ToString()}
                    });
                }
            }

            return instrumentFaults;
        }

        public IList<MaintenanceOrder> GetInstrumentMaintenanceOrders(string InstrumentID)
        {
            List<MaintenanceOrder> maintenanceOrders = null;

            string sqlCommandText = ModuleConfiguration.SQL_CMD_SelectInstrumentMaintenanceOrder;

            var dbResult = this.dmlOperable.ExeReader(sqlCommandText, new Dictionary<string, object>() { { "@InstrumentID", long.Parse(InstrumentID) } });

            if (dbResult != null && dbResult.Count > 0)
            {
                maintenanceOrders = new List<MaintenanceOrder>();

                for (int i = 0; i < dbResult.Count; i++)
                {
                    maintenanceOrders.Add(new MaintenanceOrder()
                    {
                        ID = dbResult[i]["ID"].ToString(),
                        OrderRefID = (string)dbResult[i]["OrderRefID"],
                        OrderDate = (DateTime?)dbResult[i]["OrderDate"],
                        FaultTime = (DateTime?)dbResult[i]["FaultTime"],
                        Fault = new Fault()
                        {
                            //ID = dbResult[i]["ID"].ToString(),
                            FaultType = dbResult[0]["FaultType"] == null ? 0 : (int?)dbResult[i]["FaultType"],
                            FaultCode = (string)dbResult[i]["FaultCode"],
                            FaultMessage = (string)dbResult[i]["FaultDescription"]
                        },
                        Instrument = new Instrument() { ID = dbResult[i]["InstrumentID"].ToString() },
                        WarrantyStatus = dbResult[i]["WarrantyStatus"] == null ? 0 :(int?)dbResult[i]["WarrantyStatus"],
                        Cost = dbResult[i]["Cost"] == null ? 0 : (decimal?)dbResult[i]["Cost"],
                        Result = new Comment() {
                             Title = (string)dbResult[i]["RepairResult"],
                             Body = (string)dbResult[i]["RepairDescription"]},
                        Comment = new Comment() {
                            Title = (string)dbResult[i]["UserComment"],
                            Body = (string)dbResult[i]["CommentContent"]}

                    });
                }
            }

            return maintenanceOrders;
        }

        public IList<MaintenanceOrder> GetInstrumentMaintenanceOrders<T>(string InstrumentID, out T ExtraData, Func<object, T> ExtensionFunction)
        {
            List<MaintenanceOrder> maintenanceOrders = null;

            ExtraData = default(T);

            string sqlCommandText = ModuleConfiguration.SQL_CMD_SelectInstrumentMaintenanceOrder;

            var dbResult = this.dmlOperable.ExeReader(sqlCommandText, new Dictionary<string, object>() { { "@InstrumentID", long.Parse(InstrumentID) } });

            if (dbResult != null && dbResult.Count > 0)
            {
                maintenanceOrders = new List<MaintenanceOrder>();

                for (int i = 0; i < dbResult.Count; i++)
                {
                    maintenanceOrders.Add(new MaintenanceOrder()
                    {
                        ID = dbResult[i]["ID"].ToString(),
                        OrderRefID = (string)dbResult[i]["OrderRefID"],
                        OrderDate = (DateTime?)dbResult[i]["OrderDate"],
                        FaultTime = (DateTime?)dbResult[i]["FaultTime"],
                        Fault = new Fault()
                        {
                            //ID = dbResult[i]["ID"].ToString(),
                            FaultType = dbResult[0]["FaultType"] == null ? 0 : (int?)dbResult[i]["FaultType"],
                            FaultCode = (string)dbResult[i]["FaultCode"],
                            FaultMessage = (string)dbResult[i]["FaultDescription"]
                        },
                        Instrument = new Instrument() { ID = dbResult[i]["InstrumentID"].ToString() },
                        WarrantyStatus = dbResult[i]["WarrantyStatus"] == null ? 0 : (int?)dbResult[i]["WarrantyStatus"],
                        Cost = dbResult[i]["Cost"] == null ? 0 :(decimal?)dbResult[i]["Cost"],
                        Result = new Comment()
                        {
                            Title = (string)dbResult[i]["RepairResult"],
                            Body = (string)dbResult[i]["RepairDescription"]
                        },
                        Comment = new Comment()
                        {
                            Title = (string)dbResult[i]["UserComment"],
                            Body = (string)dbResult[i]["CommentContent"]
                        }

                    });
                }
            }

            if (ExtensionFunction != null)
            {
                ExtraData = ExtensionFunction(maintenanceOrders);
            }

            return maintenanceOrders;
        }

        public IList<MaintenanceOrderLineItem> GetMaintenanceOrderLineItems(string OrderID)
        {
            List<MaintenanceOrderLineItem> maintenanceOrderLineItems = null;

            string sqlCommandText = ModuleConfiguration.SQL_CMD_SelectInstrumentMaintenanceOrderLineItem;

            var dbResult = this.dmlOperable.ExeReader(sqlCommandText, new Dictionary<string, object>() { { "OrderID", long.Parse(OrderID) } });

            if (dbResult != null && dbResult.Count > 0)
            {
                maintenanceOrderLineItems = new List<MaintenanceOrderLineItem>();

                for (int i = 0; i < dbResult.Count; i++)
                {
                    maintenanceOrderLineItems.Add(new MaintenanceOrderLineItem()
                    {
                        ID = dbResult[i]["ID"].ToString(),
                        OrderID = dbResult[i]["OrderID"].ToString(),
                        FulfillmentDate = (DateTime?)dbResult[i]["FulfillmentDate"],
                        Notes = new Content() { Body = (string)dbResult[i]["Notes"] },
                        RepairResult = (string)dbResult[i]["RepairResult"]
                    });
                }
            }

            return maintenanceOrderLineItems;
        }

        public IList<MaintenanceAttachment> GetMaintenanceOrderAttachments(string OrderID)
        {
            List<MaintenanceAttachment> maintenanceAttachments   = null;

            string sqlCommandText = ModuleConfiguration.SQL_CMD_SelectMaintenanceOrderAttachment;

            var dbResult = this.dmlOperable.ExeReader(sqlCommandText, new Dictionary<string, object>() { { "OrderID", long.Parse(OrderID) } });

            if (dbResult != null && dbResult.Count > 0)
            {
                maintenanceAttachments = new List<MaintenanceAttachment>();

                for (int i = 0; i < dbResult.Count; i++)
                {
                    maintenanceAttachments.Add(new MaintenanceAttachment()
                    {
                        ID = dbResult[i]["ID"].ToString(),
                        OrderID = dbResult[i]["OrderID"].ToString(),
                        Attachment = new Attachment() {
                            ID = (string)dbResult[i]["FileID"],
                            CreationTime = (DateTime?)dbResult[i]["FileCreationTime"]}             
                    });
                }
            }

            return maintenanceAttachments;
        }

        public IList<MaintenanceAttachment> GetMaintenanceOrderAttachments<T>(string OrderID, out T ExtraData, Func<object, T> ExtensionFunction)
        {
            List<MaintenanceAttachment> maintenanceAttachments = null;

            ExtraData = default(T);

            string sqlCommandText = ModuleConfiguration.SQL_CMD_SelectMaintenanceOrderAttachment;

            var dbResult = this.dmlOperable.ExeReader(sqlCommandText, new Dictionary<string, object>() { { "OrderID", long.Parse(OrderID) } });

            if (dbResult != null && dbResult.Count > 0)
            {
                maintenanceAttachments = new List<MaintenanceAttachment>();

                for (int i = 0; i < dbResult.Count; i++)
                {
                    maintenanceAttachments.Add(new MaintenanceAttachment()
                    {
                        ID = dbResult[i]["ID"].ToString(),
                        OrderID = dbResult[i]["OrderID"].ToString(),
                        Attachment = new Attachment()
                        {
                            ID = (string)dbResult[i]["FileID"],
                            CreationTime = (DateTime?)dbResult[i]["FileCreationTime"]
                        }
                    });
                }
            }

            if (ExtensionFunction != null)
            {
               ExtraData =  ExtensionFunction(maintenanceAttachments);
            }

            return maintenanceAttachments;
        }

        public MaintenanceOrder GetOrder(string OrderID)
        {
            MaintenanceOrder maintenanceOrder = null;

            string sqlCommandText = ModuleConfiguration.SQL_CMD_SelectMaintenanceOrderById;

            var dbResult = this.dmlOperable.ExeReader(sqlCommandText, new Dictionary<string, object>() { { "@ID", long.Parse(OrderID) } });

            if (dbResult != null && dbResult.Count == 1)
            {
                maintenanceOrder = new MaintenanceOrder()
                {
                    ID = dbResult[0]["ID"].ToString(),
                    OrderRefID = (string)dbResult[0]["OrderRefID"],
                    OrderDate = (DateTime?)dbResult[0]["OrderDate"],
                    FaultTime = (DateTime?)dbResult[0]["FaultTime"],
                    Fault = new Fault(){
                        //FaultType = (int?)dbResult[0]["FaultType"],
                        FaultCode = (string)dbResult[0]["FaultCode"],
                        FaultMessage = (string)dbResult[0]["FaultDescription"]},
                    Instrument = new Instrument() { ID = dbResult[0]["InstrumentID"].ToString() },
                    WarrantyStatus = dbResult[0]["WarrantyStatus"] == null ? 0 :(int?)dbResult[0]["WarrantyStatus"],
                    Cost = dbResult[0]["Cost"] != null ? decimal.Parse(dbResult[0]["Cost"].ToString()) : 0,
                    Result = new Comment(){
                        Title = (string)dbResult[0]["RepairResult"],
                        Body = (string)dbResult[0]["RepairDescription"]},
                    Comment = new Comment(){
                        Title = (string)dbResult[0]["UserComment"],
                        Body = (string)dbResult[0]["CommentContent"]}
                };
            }

            return maintenanceOrder;
        }

        public MaintenanceOrder GetOrder<T>(string OrderID, out T ExtraData, Func<object, T> ExtensionFunction)
        {
            MaintenanceOrder maintenanceOrder = null;

            ExtraData = default(T);

            string sqlCommandText = ModuleConfiguration.SQL_CMD_SelectMaintenanceOrderById;

            var dbResult = this.dmlOperable.ExeReader(sqlCommandText, new Dictionary<string, object>() { { "@ID", long.Parse(OrderID) } });

            if (dbResult != null && dbResult.Count == 1)
            {
                maintenanceOrder = new MaintenanceOrder()
                {
                    ID = dbResult[0]["ID"].ToString(),
                    OrderRefID = (string)dbResult[0]["OrderRefID"],
                    OrderDate = (DateTime?)dbResult[0]["OrderDate"],
                    FaultTime = (DateTime?)dbResult[0]["FaultTime"],
                    Fault = new Fault(){
                        //FaultType = (int?)dbResult[0]["FaultType"],
                        FaultCode = (string)dbResult[0]["FaultCode"],
                        FaultMessage = (string)dbResult[0]["FaultDescription"]},
                    Instrument = new Instrument() { ID = dbResult[0]["InstrumentID"].ToString() },
                    WarrantyStatus = dbResult[0]["WarrantyStatus"] == null ? 0 : (int?)dbResult[0]["WarrantyStatus"],
                    Cost = dbResult[0]["Cost"] != null ? decimal.Parse(dbResult[0]["Cost"].ToString()) : 0,//(decimal?)dbResult[0]["Cost"],
                    Result = new Comment(){
                        Title = (string)dbResult[0]["RepairResult"],
                        Body = (string)dbResult[0]["RepairDescription"] },
                    Comment = new Comment() {
                        Title = (string)dbResult[0]["UserComment"],
                        Body = (string)dbResult[0]["CommentContent"] }
                };
            }

            if (ExtensionFunction != null)
            {
                ExtraData = ExtensionFunction(maintenanceOrder);
            }

            return maintenanceOrder;
        }

        public string NewOrder(MaintenanceOrder Order)
        {
            //if (Order == null || string.IsNullOrEmpty(Order.ID))
            //{
            //    throw new IDNullException("维修记录ID不可为空！");
            //}

            string sqlCommandText = ModuleConfiguration.SQL_CMD_InsertMaintenanceOrder;

            this.dmlOperable.ExeSql(sqlCommandText, new Dictionary<string, object>() {
                //{ "@ID", long.Parse(Order.ID)},
                { "@OrderRefID", Order.OrderRefID},
                { "@OrderDate", Order.OrderDate},
                { "@InstrumentID", Order.Instrument.ID},
                { "@FaultTime", Order.FaultTime},
                { "@FaultCode", Order.Fault.FaultCode},
                { "@FaultDescription", Order.Fault.FaultMessage},
                { "@WarrantyStatus", Order.WarrantyStatus},
                { "@Cost", Order.Cost},
                { "@RepairResult", Order.Result.Title},
                { "@RepairDescription", Order.Result.Body},
                { "@UserComment", Order.Comment.Title},
                { "@CommentContent", Order.Comment.Body}
            });

            return GenerateMaintenanceOrderID(0); //Order.ID;
        }

        public string UpdateOrder(string OrderID, MaintenanceOrder Order)
        {
            if (Order == null || string.IsNullOrEmpty(OrderID))
            {
                throw new IDNullException("维修记录ID不可为空！");

            }
            string sqlCommandText = ModuleConfiguration.SQL_CMD_UpdateMaintenanceOrder;

            this.dmlOperable.ExeSql(sqlCommandText, new Dictionary<string, object>() {
                { "@ID", long.Parse(OrderID)},
                { "@OrderRefID", Order.OrderRefID},
                { "@OrderDate", Order.OrderDate},
                { "@InstrumentID", Order.Instrument.ID},
                { "@FaultTime", Order.FaultTime},
                { "@FaultCode", Order.Fault.FaultCode},
                { "@FaultDescription", Order.Fault.FaultMessage},
                { "@WarrantyStatus", Order.WarrantyStatus},
                { "@Cost", Order.Cost},
                { "@RepairResult", Order.Result.Title},
                { "@RepairDescription", Order.Result.Body},
                { "@UserComment", Order.Comment.Title},
                { "@CommentContent", Order.Comment.Body}
            });

            return Order.ID;
        }

        public string AddtMaintenanceOrderLineItem(MaintenanceOrderLineItem OrderLineItem)
        {
            //if (OrderLineItem == null || string.IsNullOrEmpty(OrderLineItem.ID))
            //{
            //    throw new IDNullException("维修过程ID不可为空！");
            //}

            string sqlCommandText = ModuleConfiguration.SQL_CMD_InsertMaintenanceOrderLineItem;

            this.dmlOperable.ExeSql(sqlCommandText, new Dictionary<string, object>() {
                //{ "@ID", long.Parse(OrderLineItem.ID)},
                { "@OrderID", OrderLineItem.OrderID},
                { "@FulfillmentDate", OrderLineItem.FulfillmentDate},
                { "@Notes", OrderLineItem.Notes.Body},
                { "@RepairResult", OrderLineItem.RepairResult}
            });

            return GenerateMaintenanceOrderLineItemID(0); //OrderLineItem.ID;
        }

        public MaintenanceOrderLineItem UpdatetMaintenanceOrderLineItem(string LineItemID, MaintenanceOrderLineItem OrderLineItem)
        {
            if (OrderLineItem == null || string.IsNullOrEmpty(LineItemID))
            {
                throw new IDNullException("维修过程ID不可为空！");
            }

            string sqlCommandText = ModuleConfiguration.SQL_CMD_UpdateMaintenanceOrderLineItem;

            this.dmlOperable.ExeSql(sqlCommandText, new Dictionary<string, object>() {
                { "@ID", long.Parse(LineItemID)},
                { "@Notes", OrderLineItem.Notes.Body},
                { "@RepairResult", OrderLineItem.RepairResult}
            });

            return OrderLineItem;
        }

        public string AddMaintenanceOrderAttachment(MaintenanceAttachment Attachment)
        {
            string sqlCommandText = ModuleConfiguration.SQL_CMD_InsertMaintenanceOrderAttachment;

            this.dmlOperable.ExeSql(sqlCommandText, new Dictionary<string, object>() {
                //{ "@ID", long.Parse(Attachment.ID)},
                { "@OrderID", Attachment.OrderID},
                { "@FileCreationTime", Attachment.Attachment.CreationTime},
                { "@FileID", Attachment.Attachment.ID}
            });

            return GenerateMaintenanceOrderAttachmentID(0); //Attachment.ID;
        }

        public System.Data.DataTable GetModuleDataTable()
        {
            return this.dmlOperable.GetDataTable(ModuleConfiguration.SQL_CMD_MaintenanceOrderData);
        }

        /// <summary>
        /// 维修查询
        /// </summary>
        /// <param name="strTime1">时间1</param>
        /// <param name="strTime2">时间2</param>
        /// <param name="strDanHao">维修单号</param>
        /// <param name="strXuLieHao">仪器ID</param>
        /// <param name="strJieGuo">维修结果</param>
        /// <returns></returns>
        public System.Data.DataTable GetCXModuleDataTable(string strTime1, string strTime2, string strDanHao, string strXuLieHao, string strJieGuo)
        {
            //string strCX = " OrderDate > '" + strTime1 + "'  AND  OrderDate < '" + strTime2 + "' ";

            //if (strDanHao != "")
            //{
            //    strCX += "  AND  OrderRefID like  '%" + strDanHao + "%' ";
            //}

            //if (strXuLieHao != "")
            //{
            //    strCX += " AND InstrumentID  =  " + strXuLieHao;
            //}

            //if (strJieGuo != "")
            //{
            //    strCX += " AND  RepairResult  =  " + strJieGuo;
            //}
            //string strSql = "SELECT CASE `RepairResult` WHEN '0' THEN '维修成功' WHEN '1' THEN '维修失败' END AS RepairResult,A.ID, InstrumentID, OrderRefID, OrderDate, FaultTime,Cost, FaultCode, FaultDescription, RepairResult, RepairDescription, UserComment, CommentContent,SerialNumber FROM maintenance_orders A, instruments B WHERE B.ID = A.InstrumentID AND  " + strCX;
            //return this.dmlOperable.GetDataTable(strSql);

            //string strCX = " OrderDate > '" + strTime1 + "'  AND  OrderDate < '" + strTime2 + "' ";

            string strCX = " OrderDate >= '" + strTime1 + "'  AND  OrderDate <= '" + strTime2 + "' "; //日期范围应该包括边界值，否则当天的数据指定当天的日期查不出来--王锐 2020.6.4.

            if (strDanHao != "")
            {
                strCX += "  AND  OrderRefID like  '%" + strDanHao + "%' ";
            }

            if (strXuLieHao != "")
            {
                strCX += " AND InstrumentID  =  " + strXuLieHao;
            }

            if (strJieGuo != "")
            {
                strCX += " AND  RepairResult  =  " + strJieGuo;
            }
            string strSql = "SELECT CASE RepairResult WHEN '0' THEN '维修成功' WHEN '1' THEN '维修失败' END AS RepairResult, A.ID  as ID, InstrumentID as InstrumentID, OrderRefID as OrderRefID, OrderDate as OrderDate, FaultTime as FaultTime, Cost as Cost, FaultCode as FaultTime, FaultDescription as FaultDescription, RepairResult as RepairResult, RepairDescription as RepairDescription, UserComment as UserComment, CommentContent as CommentContent, SerialNumber as SerialNumber FROM MaintenanceOrders A, instruments B WHERE B.ID = A.InstrumentID AND  " + strCX;
            return this.dmlOperable.GetDataTable(strSql);
        }

        public System.Data.DataTable GetModuleOrderLineltemDataTable(string strId)
        {
            return this.dmlOperable.GetDataTable(ModuleConfiguration.SQL_CMD_SelectInstrumentMaintenanceOrderLineItem, new Dictionary<string, object>() { { "@OrderID", long.Parse(strId) } });
        }

        public string DeleteMaintenanceOrder(string ID)
        {
            if (string.IsNullOrEmpty(ID))
            {
                throw new IDNullException("ID不可为空！");

            }
            string sqlCommandText = ModuleConfiguration.SQL_CMD_DeleteMaintenanceOrder;

            this.dmlOperable.ExeSql(sqlCommandText, new Dictionary<string, object>() { { "@ID", long.Parse(ID) } });

            return ID;
        }

        public string DeleteMaintenanceOrderLineitems(string ID)
        {
            if (string.IsNullOrEmpty(ID))
            {
                throw new IDNullException("ID不可为空！");
            }
            string sqlCommandText = ModuleConfiguration.SQL_CMD_DeleteMaintenanceOrderLineitems;

            this.dmlOperable.ExeSql(sqlCommandText, new Dictionary<string, object>() { { "@OrderID", long.Parse(ID) } });

            return ID;
        }
        public string DeleteMaintenanceOrderLineitems1(string ID)
        {
            if (string.IsNullOrEmpty(ID))
            {
                throw new IDNullException("ID不可为空！");
            }
            string sqlCommandText = ModuleConfiguration.SQL_CMD_DeleteMaintenanceOrderLineitems1;

            this.dmlOperable.ExeSql(sqlCommandText, new Dictionary<string, object>() { { "@ID", long.Parse(ID) } });

            return ID;
        }
        /// <summary>
        /// 文件记录新增
        /// </summary>
        /// <param name="strWeiXiuId">维修记录ID</param>
        /// <param name="strTime">上传时间</param>
        /// <param name="strFID">文件ID</param>
        /// <returns></returns>
        public string AddMaintenanceOrderAttachment1(string strWeiXiuId, string strTime, string strFID)
        {
            string sqlCommandText = ModuleConfiguration.SQL_CMD_InsertMaintenanceOrderAttachment;

            this.dmlOperable.ExeSql(sqlCommandText, new Dictionary<string, object>() {
                //{ "@ID", long.Parse(Attachment.ID)},
                { "@OrderID", strWeiXiuId},
                { "@FileCreationTime", DateTime.Parse(strTime)},
                { "@FileID", strFID}
            });

            return GenerateMaintenanceOrderAttachmentID(0); //Attachment.ID;
        }

        public string DeleteMaintenanceOrderAttachment(string ID)
        {
            if (string.IsNullOrEmpty(ID))
            {
                throw new IDNullException("ID不可为空！");
            }
            string sqlCommandText = ModuleConfiguration.SQL_CMD_DeleteMaintenanceOrderAttachment;

            this.dmlOperable.ExeSql(sqlCommandText, new Dictionary<string, object>() { { "@ID", long.Parse(ID) } });

            return ID;
        }

        public string GenerateMaintenanceOrderID(int Increment)
        {
            string maxIdStr = this.dmlOperable.ExeSqlScalar(ModuleConfiguration.SQL_CMD_SelectMaxMaintenanceOrderID);

            if (string.IsNullOrEmpty(maxIdStr))
            {
                return Increment.ToString();
            }

            long maxId = 0;

            if (long.TryParse(maxIdStr, out maxId))
            {
                maxId += Increment;
            }

            return maxId.ToString();
        }

        public string GenerateMaintenanceOrderLineItemID(int Increment)
        {
            string maxIdStr = this.dmlOperable.ExeSqlScalar(ModuleConfiguration.SQL_CMD_SelectMaxMaintenanceeOrderLineItemID);

            if (string.IsNullOrEmpty(maxIdStr))
            {
                return Increment.ToString();
            }

            long maxId = 0;

            if (long.TryParse(maxIdStr, out maxId))
            {
                maxId += Increment;
            }

            return maxId.ToString();
        }

        public string GenerateMaintenanceOrderAttachmentID(int Increment)
        {
            string maxIdStr = this.dmlOperable.ExeSqlScalar(ModuleConfiguration.SQL_CMD_SelectMaxMaintenanceOrderAttachmentID);

            if (string.IsNullOrEmpty(maxIdStr))
            {
                return Increment.ToString();
            }

            long maxId = 0;

            if (long.TryParse(maxIdStr, out maxId))
            {
                maxId += Increment;
            }

            return maxId.ToString();
        }

        public string GenerateInstrumentFaultID(int Increment)
        {
            string maxIdStr = this.dmlOperable.ExeSqlScalar(ModuleConfiguration.SQL_CMD_SelectMaxInstrumentFaultID);

            if (string.IsNullOrEmpty(maxIdStr))
            {
                return Increment.ToString();
            }

            long maxId = 0;

            if (long.TryParse(maxIdStr, out maxId))
            {
                maxId += Increment;
            }

            return maxId.ToString();
        }
    }
}
