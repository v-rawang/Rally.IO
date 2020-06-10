using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Rally.Lib.Persistence.Core;
using Rally.Framework.Core;

namespace Rally.Framework.Authorization
{
    public class AuthorizationManager : IAuthorization
    {
        public AuthorizationManager(IDMLOperable DMLOperable, DBTypeEnum DBType)
        {
            this.dbType = DBType;
            this.dmlOperable = DMLOperable;
        }
        
        private IDMLOperable dmlOperable;
        private DBTypeEnum dbType;

        public static IAuthorization NewInstance(IDMLOperable DMLOperable, DBTypeEnum DBType)
        {
            return new AuthorizationManager(DMLOperable, DBType);
        }

        public object AddActorToRole(string ActorID, string Role)
        {
            //string sqlTxt = "insert into roleactors (id, roleid, actorid) values (@id, @roleid, @actorid)";

            string sqlTxt = "insert into roleactors (id, roleid, actorid) values (@id, @roleid, @actorid)";

            int result = this.dmlOperable.ExeSql(sqlTxt, new Dictionary<string, object>() {
                { "@id", Guid.NewGuid().ToString() },
                {"@roleid", Role },
                { "@actorid", ActorID}});

            return result;
        }

        public object AddRoles(IDictionary<string, string> Roles, IDictionary<string, string> RoleDescriptions)
        {
            //string sqlTxt = "insert into Roles (Id, Name, RoleType, Description) values (@Id, @Name, @RoleType, @Description)";

            string sqlTxt = "insert into Roles (Id, Name, RoleType, Description) values (@Id, @Name, @RoleType, @Description)";

            int dbResult = -1;

            try
            {
                string roleName, roleType = "1", roleDecscription;

                this.dmlOperable.BeginTrans();
                dbResult++;

                foreach (string roleId in Roles.Keys)
                {
                    roleName = Roles[roleId];
                    roleDecscription = RoleDescriptions[roleId];
                    dbResult += this.dmlOperable.ExeSql(sqlTxt, new Dictionary<string, object>() {
                        { "@Id", roleId },
                        { "@RoleType", roleType},
                        { "@Name",roleName },
                        { "@Description",roleDecscription }
                    });
                }

                this.dmlOperable.CommitTrans();
            }
            catch (Exception ex)
            {
                this.dmlOperable.RollbackTrans();
                throw ex;
            }

            return dbResult;
        }

        public object DeleteActor(string ActorID)
        {
            //string sqlTxtDeleteRoleActors = "delete from RoleActors where ActorId = @ActorId";
            //string sqlTxtDeleteObjAuthItems = "delete from ObjectOperationAuthItems where ActorId = @ActorId";

            string sqlTxtDeleteRoleActors = "delete from RoleActors where ActorId = @ActorId";
            string sqlTxtDeleteObjAuthItems = "delete from ObjectOperationAuthItems where ActorId = @ActorId";

            int dbResult = -1;

            try
            {
                this.dmlOperable.BeginTrans();
                dbResult++;

                dbResult += this.dmlOperable.ExeSql(sqlTxtDeleteRoleActors, new Dictionary<string, object>() { { "@ActorId", ActorID } });
                dbResult += this.dmlOperable.ExeSql(sqlTxtDeleteObjAuthItems, new Dictionary<string, object>() { { "@ActorId", ActorID } });

                this.dmlOperable.CommitTrans();
            }
            catch (Exception ex)
            {
                this.dmlOperable.RollbackTrans();
                throw ex;
            }

            return dbResult;
        }

        public object DeleteDataScope(string DataScopeID)
        {
            //string sqlTxtDeleteRoleDataScopes = "delete from RoleDataScopes where DataScopeId = @DataScopeId";
            //string sqlTxtDeleteDataScopes = "delete from DataScopes where Id = @Id";

            string sqlTxtDeleteRoleDataScopes = "delete from RoleDataScopes where DataScopeId = @DataScopeId";
            string sqlTxtDeleteDataScopes = "delete from DataScopes where Id = @Id";

            int dbResult = -1;

            try
            {
                this.dmlOperable.BeginTrans();
                dbResult++;

                dbResult += this.dmlOperable.ExeSql(sqlTxtDeleteRoleDataScopes, new Dictionary<string, object>() { { "@DataScopeId", DataScopeID } });
                dbResult += this.dmlOperable.ExeSql(sqlTxtDeleteDataScopes, new Dictionary<string, object>() { { "@Id", DataScopeID } });

                this.dmlOperable.CommitTrans();
            }
            catch (Exception ex)
            {
                this.dmlOperable.RollbackTrans();
                throw ex;
            }

            return dbResult;
        }

        public object DeleteOperation(string OperationID)
        {
            //string sqlTxtDeleteRoleOperations = "delete from RoleOperations where OperationId = @OperationId";
            //string sqlTxtDeleteObjAuthItems = "delete from ObjectOperationAuthItems where OperationId = @OperationId";
            //string sqlTxtDeleteOperations = "delete from Operations where Id = @Id";

            string sqlTxtDeleteRoleOperations = "delete from RoleOperations where OperationId = @OperationId";
            string sqlTxtDeleteObjAuthItems = "delete from ObjectOperationAuthItems where OperationId = @OperationId";
            string sqlTxtDeleteOperations = "delete from Operations where Id = @Id";

            int dbResult = -1;

            try
            {
                this.dmlOperable.BeginTrans();
                dbResult++;

                dbResult += this.dmlOperable.ExeSql(sqlTxtDeleteRoleOperations, new Dictionary<string, object>() { { "@OperationId", OperationID } });
                dbResult += this.dmlOperable.ExeSql(sqlTxtDeleteObjAuthItems, new Dictionary<string, object>() { { "@OperationId", OperationID } });
                dbResult += this.dmlOperable.ExeSql(sqlTxtDeleteOperations, new Dictionary<string, object>() { { "@Id", OperationID } });

                this.dmlOperable.CommitTrans();
            }
            catch (Exception ex)
            {
                this.dmlOperable.RollbackTrans();
                throw ex;
            }

            return dbResult;
        }

        public object DeleteRole(string RoleID)
        {
            //string sqlTxtDeleteObjAuthItems = "delete from ObjectOperationAuthItems where (OperationId in (select OperationId from RoleOperations where RoleId = @RoleId)) or (ActorId in (select ActorId from RoleActors where RoleId = @RoleId))";
            //string sqlTxtDeleteRoleActors = "delete from RoleActors where RoleId = @RoleId";     
            //string sqlTxtDeleteRoleOperations = "delete from RoleOperations where RoleId = @RoleId";
            //string sqlTxtDeleteRoleDataScopes = "delete from RoleDataScopes where RoleId = @RoleId";
            //string sqlTxtDeleteRoles = "delete from Roles where Id = @Id";

            string sqlTxtDeleteObjAuthItems = "delete from ObjectOperationAuthItems where (OperationId in (select OperationId from RoleOperations where RoleId = @RoleId)) or (ActorId in (select ActorId from RoleActors where RoleId = @RoleId))";
            string sqlTxtDeleteRoleActors = "delete from RoleActors where RoleId = @RoleId";
            string sqlTxtDeleteRoleOperations = "delete from RoleOperations where RoleId = @RoleId";
            string sqlTxtDeleteRoleDataScopes = "delete from RoleDataScopes where RoleId = @RoleId";
            string sqlTxtDeleteRoles = "delete from Roles where Id = @Id";

            int dbResult = -1;

            try
            {
                this.dmlOperable.BeginTrans();
                dbResult++;

                dbResult += this.dmlOperable.ExeSql(sqlTxtDeleteObjAuthItems, new Dictionary<string, object>() { { "@RoleId", RoleID } });
                dbResult += this.dmlOperable.ExeSql(sqlTxtDeleteRoleActors, new Dictionary<string, object>() { { "@RoleId", RoleID } });    
                dbResult += this.dmlOperable.ExeSql(sqlTxtDeleteRoleOperations, new Dictionary<string, object>() { { "@RoleId", RoleID } });
                dbResult += this.dmlOperable.ExeSql(sqlTxtDeleteRoleDataScopes, new Dictionary<string, object>() { { "@RoleId", RoleID } });
                dbResult += this.dmlOperable.ExeSql(sqlTxtDeleteRoles, new Dictionary<string, object>() { { "@Id", RoleID } });

                this.dmlOperable.CommitTrans();
            }
            catch (Exception ex)
            {
                this.dmlOperable.RollbackTrans();
                throw ex;
            }

            return dbResult;
        }

        public object[] GetActorDataScopes(string Actor, string DataTypeName)
        {
            //string sqlTxtSelectDataScopes = "select Id, ScopeName, ScopeType, DataType, DataIdentifier from DataScopes where DataType = @DataType";
            //string sqlTxtSelectRoleActors = "select RoleId, ActorId from RoleActors where ActorId = @ActorId";
            //string sqlTxtSelectRoleDataScopes = "select RoleId, DataScopeId, ScopeValue from RoleActors";

            string sqlTxtSelectDataScopes = "select Id as Id, ScopeName as ScopeName, ScopeType as ScopeType, DataType as DataType, DataIdentifier as DataIdentifier from DataScopes where DataType = @DataType";
            string sqlTxtSelectRoleActors = "select RoleId as RoleId, ActorId as ActorId from RoleActors where ActorId = @ActorId";
            string sqlTxtSelectRoleDataScopes = "select RoleId as RoleId, DataScopeId as DataScopeId, ScopeValue as ScopeValue from RoleDataScopes";

            IDictionary<string, IList<object>> dataScopeValues = new Dictionary<string, List<object>>() as IDictionary<string, IList<object>>;
            IDictionary<string, string> dataScopeTypes = new Dictionary<string, string>();
            string dataIndetifier = null;

            IList<IDictionary<string, object>> dbResultDataScopes = this.dmlOperable.ExeReader(sqlTxtSelectDataScopes, new Dictionary<string, object>() { { "@DataType", DataTypeName } });

            if (dbResultDataScopes == null || dbResultDataScopes.Count <= 0)
            {
                return null;
            }

            IList<IDictionary<string, object>> dbResultRoleActors = this.dmlOperable.ExeReader(sqlTxtSelectRoleActors, new Dictionary<string, object>() { { "@ActorId", Actor } });

            if (dbResultRoleActors == null || dbResultRoleActors.Count <= 0)
            {
                return null;
            }

            IList<IDictionary<string, object>> dbResultRoleDataScopes = this.dmlOperable.ExeReader(sqlTxtSelectRoleDataScopes, null);

            if (dbResultRoleDataScopes == null || dbResultRoleDataScopes.Count <= 0)
            {
                return null;
            }

            IList<IDictionary<string, object>> roleDataScopes = new List<IDictionary<string, object>>();

            for (int i = 0; i < dbResultRoleActors.Count; i++)
            {
                for (int j = 0; j < dbResultRoleDataScopes.Count; j++)
                {
                    if (dbResultRoleActors[i]["RoleId"].ToString().ToLower() == dbResultRoleDataScopes[j]["RoleId"].ToString().ToLower())
                    {
                        roleDataScopes.Add(dbResultRoleDataScopes[j]);
                    }
                }
            }

            string dataScopeName = "";

            for (int i = 0; i < roleDataScopes.Count; i++)
            {
                for (int j = 0; j < dbResultDataScopes.Count; j++)
                {
                    if (roleDataScopes[i]["DataScopeId"].ToString().ToLower() == dbResultDataScopes[j]["Id"].ToString().ToLower())
                    {
                        if (String.IsNullOrEmpty(dataIndetifier))
                        {
                            dataIndetifier = dbResultDataScopes[j]["DataIdentifier"].ToString();
                        }

                        dataScopeName = dbResultDataScopes[j]["ScopeName"].ToString();

                        if (!dataScopeTypes.ContainsKey(dataScopeName))
                        {
                            dataScopeTypes.Add(dataScopeName, dbResultDataScopes[j]["ScopeType"].ToString());
                        }

                        if (!dataScopeValues.ContainsKey(dataScopeName))
                        {
                            dataScopeValues.Add(dataScopeName, new List<object>());
                        }

                        dataScopeValues[dataScopeName].Add(roleDataScopes[i]["ScopeValue"]);              
                    }
                }
            }

            if (!String.IsNullOrEmpty(dataIndetifier) && dataScopeTypes.Count > 0 && dataScopeValues.Count > 0)
            {
                return new object[] { DataTypeName, dataIndetifier, dataScopeTypes, dataScopeValues };
            }

            return null;
        }

        public object[] GetAuthorizedObjects(string Actor, string DataTypeName, Func<string, object, IDictionary<string, string>, IDictionary<string, IList<object>>, object[]> ComputingFunction)
        {
            //string sqlTxtSelectDataScopes = "select Id, ScopeName, ScopeType, DataType, DataIdentifier from DataScopes where DataType = @DataType";
            //string sqlTxtSelectRoleActors = "select RoleId, ActorId from RoleActors where ActorId = @ActorId";
            //string sqlTxtSelectRoleDataScopes = "select RoleId, DataScopeId, ScopeValue from RoleActors";

            string sqlTxtSelectDataScopes = "select Id as Id, ScopeName as ScopeName, ScopeType as ScopeType, DataType as DataType, DataIdentifier as DataIdentifier from DataScopes where DataType = @DataType";
            string sqlTxtSelectRoleActors = "select RoleId as RoleId, ActorId as ActorId from RoleActors where ActorId = @ActorId";
            string sqlTxtSelectRoleDataScopes = "select RoleId as RoleId, DataScopeId as DataScopeId, ScopeValue as ScopeValue from RoleDataScopes";

            IDictionary<string, IList<object>> dataScopeValues = new Dictionary<string, List<object>>() as IDictionary<string, IList<object>>;
            IDictionary<string, string> dataScopeTypes = new Dictionary<string, string>();
            string dataIndetifier = null;

            IList<IDictionary<string, object>> dbResultDataScopes = this.dmlOperable.ExeReader(sqlTxtSelectDataScopes, new Dictionary<string, object>() { { "@DataType", DataTypeName } });

            if (dbResultDataScopes == null || dbResultDataScopes.Count <= 0)
            {
                return null;
            }

            IList<IDictionary<string, object>> dbResultRoleActors = this.dmlOperable.ExeReader(sqlTxtSelectRoleActors, new Dictionary<string, object>() { { "@ActorId", Actor } });

            if (dbResultRoleActors == null || dbResultRoleActors.Count <= 0)
            {
                return null;
            }

            IList<IDictionary<string, object>> dbResultRoleDataScopes = this.dmlOperable.ExeReader(sqlTxtSelectRoleDataScopes, null);

            if (dbResultRoleDataScopes == null || dbResultRoleDataScopes.Count <= 0)
            {
                return null;
            }

            IList<IDictionary<string, object>> roleDataScopes = new List<IDictionary<string, object>>();

            for (int i = 0; i < dbResultRoleActors.Count; i++)
            {
                for (int j = 0; j < dbResultRoleDataScopes.Count; j++)
                {
                    if (dbResultRoleActors[i]["RoleId"].ToString().ToLower() == dbResultRoleDataScopes[j]["RoleId"].ToString().ToLower())
                    {
                        roleDataScopes.Add(dbResultRoleDataScopes[j]);
                    }
                }
            }

            string dataScopeName = "";

            for (int i = 0; i < roleDataScopes.Count; i++)
            {
                for (int j = 0; j < dbResultDataScopes.Count; j++)
                {
                    if (roleDataScopes[i]["DataScopeId"].ToString().ToLower() == dbResultDataScopes[j]["Id"].ToString().ToLower())
                    {
                        if (String.IsNullOrEmpty(dataIndetifier))
                        {
                            dataIndetifier = dbResultDataScopes[j]["DataIdentifier"].ToString();
                        }

                        dataScopeName = dbResultDataScopes[j]["ScopeName"].ToString();

                        if (!dataScopeTypes.ContainsKey(dataScopeName))
                        {
                            dataScopeTypes.Add(dataScopeName, dbResultDataScopes[j]["ScopeType"].ToString());
                        }

                        if (!dataScopeValues.ContainsKey(dataScopeName))
                        {
                            dataScopeValues.Add(dataScopeName, new List<object>());
                        }

                        dataScopeValues[dataScopeName].Add(roleDataScopes[i]["ScopeValue"]);
                    }
                }
            }

            if (ComputingFunction != null)
            {
                return ComputingFunction(DataTypeName, dataIndetifier, dataScopeTypes, dataScopeValues);
            }

            return null;
        }

        public string[] GetDataScopes(string DataTypeName)
        {
            //string sqlTxt = "select Id from DataScopes";//"select Id, ScopeName, ScopeType, DataType, DataIdentifier from DataScopes where DataType = @DataType";

            string sqlTxt = "select Id as Id from DataScopes";

            IDictionary<string, object> sqlParam = null;

            if (!String.IsNullOrEmpty(DataTypeName))
            {
                //sqlTxt = "select Id from DataScopes where DataType = @DataType";

                sqlTxt = "select Id as Id from DataScopes where DataType = @DataType";
                sqlParam = new Dictionary<string, object>() { { "@DataType", DataTypeName } };
            }

            IList<IDictionary<string, object>> dbResult = this.dmlOperable.ExeReader(sqlTxt,  sqlParam);

            if (dbResult != null && dbResult.Count > 0)
            {
                List<string> dataScopeIds = new List<string>();

                for (int i = 0; i < dbResult.Count; i++)
                {
                    dataScopeIds.Add(dbResult[i]["Id"].ToString());
                }

                return dataScopeIds.ToArray();
            }

            return null;
        }

        public string[] GetDataTypeOperations(string DataTypeName)
        {
            //string sqlTxt = "select Id from Operations";

            string sqlTxt = "select Id as Id from Operations";

            IDictionary<string, object> sqlParam = null;

            if (!String.IsNullOrEmpty(DataTypeName))
            {
                //sqlTxt = "select Id from Operations where DataType = @DataType";

                sqlTxt = "select Id as Id from Operations where DataType = @DataType";
                sqlParam = new Dictionary<string, object>() { { "@DataType", DataTypeName } };
            }

            IList<IDictionary<string, object>> dbResult = this.dmlOperable.ExeReader(sqlTxt, sqlParam);

            if (dbResult != null && dbResult.Count > 0)
            {
                List<string> dataOpIds = new List<string>();

                for (int i = 0; i < dbResult.Count; i++)
                {
                    dataOpIds.Add(dbResult[i]["Id"].ToString());
                }

                return dataOpIds.ToArray();
            }

            return null;
        }

        public bool IsAuthorized(string Actor, string OperationID)
        {
            //string sqlTxtSelectRoleActors = "select RoleId, ActorId from RoleActors where ActorId = @ActorId";
            //string sqlTxtSelectRoleOps = "select RoleId, OperationId from RoleOperations where OperationId = @OperationId";

            string sqlTxtSelectRoleActors = "select RoleId as RoleId, ActorId as ActorId from RoleActors where ActorId = @ActorId";
            string sqlTxtSelectRoleOps = "select RoleId as RoleId, OperationId as OperationId from RoleOperations where OperationId = @OperationId";

            IList<IDictionary<string, object>> dbResultRoleActors = this.dmlOperable.ExeReader(sqlTxtSelectRoleActors, new Dictionary<string, object>() { { "@ActorId", Actor } });

            if (dbResultRoleActors == null ||dbResultRoleActors.Count <= 0)
            {
                return false;
            }

            IList<IDictionary<string, object>> dbResultRoleOps = this.dmlOperable.ExeReader(sqlTxtSelectRoleOps, new Dictionary<string, object>() { { "@OperationId",OperationID } });

            if (dbResultRoleOps == null || dbResultRoleOps.Count <= 0)
            {
                return false;
            }

            for (int i = 0; i < dbResultRoleOps.Count; i++)
            {
                for (int j = 0; j < dbResultRoleActors.Count; j++)
                {
                    if ((dbResultRoleOps[i]["RoleId"].ToString()== dbResultRoleActors[j]["RoleId"].ToString()) && (dbResultRoleOps[i]["OperationId"].ToString() == OperationID) && (dbResultRoleActors[j]["ActorId"].ToString() == Actor))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool IsAuthorized(string Actor, string ObjectID, string OperationID)
        {
            //string sqlTxt = "select Id, ObjectId, ActorId, OperationId from ObjectOperationAuthItems where ObjectId = @ObjectId and ActorId = @ActorId and OperationId = @OperationId";

            string sqlTxt = "select Id as Id, ObjectId as ObjectId, ActorId as ActorId, OperationId as OperationId from ObjectOperationAuthItems where ObjectId = @ObjectId and ActorId = @ActorId and OperationId = @OperationId";

            IList<IDictionary<string, object>> dbResult = this.dmlOperable.ExeReader(sqlTxt, new Dictionary<string, object>() {
                { "@ObjectId", ObjectID },
                { "@ActorId", Actor },
                { "@OperationId", OperationID }
            });

            if (dbResult != null && dbResult.Count > 0)
            {
                return true;
            }

            return false;
        }

        public bool IsSupperUser(string ActorID)
        {
            string supperUserId = ModuleConfiguration.DefaultSystemSupperUserID;

            return (ActorID.ToLower() == supperUserId.ToLower());

            //string[] supperUsers = ModuleConfiguration.DefaultSystemSupperUsers;

            //return (supperUsers.Contains(ActorID));
        }

        public bool IsSupperUser(IIdentity Identity, Func<object, object> ComputingFunction)
        {
            if (Identity.IsAuthenticated)
            {
                if (ComputingFunction != null)
                {
                    var user = ComputingFunction(Identity.Name);

                    if (user != null){
                        string supperUserId = ModuleConfiguration.DefaultSystemSupperUserID;
                        return (user.ToString().ToLower() == supperUserId.ToLower());

                        //string[] supperUsers = ModuleConfiguration.DefaultSystemSupperUsers;

                        //return (supperUsers.Contains(user.ToString()));
                    }
                }     
            }

            return false;
        }

        public object RegisterDataScope(string DataTypeName, string DataScopeID, string DataScopeName, string DataScopeType, string DataIdentifier)
        {
            //string sqlTxt = "insert into DataScopes (Id, ScopeName, ScopeType, DataType, DataIdentifier) values (@Id, @ScopeName, @ScopeType, @DataType, @DataIdentifier)";
            string sqlTxt = "insert into DataScopes (Id, ScopeName, ScopeType, DataType, DataIdentifier) values (@Id, @ScopeName, @ScopeType, @DataType, @DataIdentifier)";

            int result = this.dmlOperable.ExeSql(sqlTxt, new Dictionary<string, object>() {
                { "@Id", DataScopeID },
                { "@ScopeName", DataScopeName },
                { "@ScopeType", DataScopeType},
                { "@DataType", DataTypeName},
                { "@DataIdentifier", DataIdentifier}
            });

            return result;
        }

        public object RegisterOperation(string DataTypeName, string OperationID, string OperationName)
        {
            //string sqlTxt = "insert into Operations (Id, Name, DataType) values (@Id, @Name, @DataType)";
            string sqlTxt = "insert into Operations (Id, Name, DataType) values (@Id, @Name, @DataType)";

            int result = this.dmlOperable.ExeSql(sqlTxt, new Dictionary<string, object>() {
                { "@Id", OperationID },
                { "@Name", OperationName },
                { "@DataType", DataTypeName}
            });

            return result;
        }

        public object SetActorObjectOperation(string ActorID, string ObjectID, string OperationID)
        {
            //string sqlTxt = "insert into ObjectOperationAuthItems (Id, ObjectId, ActorId, OperationId) values (@Id, @ObjectId, @ActorId, @OperationId)";
            string sqlTxt = "insert into ObjectOperationAuthItems (Id, ObjectId, ActorId, OperationId) values (@Id, @ObjectId, @ActorId, @OperationId)";

            int result = this.dmlOperable.ExeSql(sqlTxt, new Dictionary<string, object>() {
                { "@Id", Guid.NewGuid().ToString() },
                { "@ObjectId", ObjectID },
                { "@ActorId", ActorID},
                { "OperationId", OperationID}
            });

            return result;
        }

        public object UnsetActorObjectOperation(string ActorID, string ObjectID, string OperationID)
        {
            //string sqlTxt = "delete from ObjectOperationAuthItems where ObjectId = @ObjectId and ActorId = @ActorId and OperationId= @OperationId";
            string sqlTxt = "delete from ObjectOperationAuthItems where ObjectId = @ObjectId and ActorId = @ActorId and OperationId= @OperationId";

            IDictionary<string, object> sqlParams = new Dictionary<string, object>() {
                { "@ObjectId", ObjectID },
                { "@ActorId", ActorID},
                { "OperationId", OperationID}
            };

            if (String.IsNullOrEmpty(OperationID))
            {
                //sqlTxt = "delete from ObjectOperationAuthItems where ObjectId = @ObjectId and ActorId = @ActorId";
                sqlTxt = "delete from ObjectOperationAuthItems where ObjectId = @ObjectId and ActorId = @ActorId";

                sqlParams = new Dictionary<string, object>() {
                { "@ObjectId", ObjectID },
                { "@ActorId", ActorID}};
            }

            int result = this.dmlOperable.ExeSql(sqlTxt, sqlParams);

            return result;
        }

        public object SetActorRole(string ActorID, string Role)
        {
            //string sqlTxtDeleteRoleActors = "delete from RoleActors where RoleId = @RoleId and ActorId = @ActorId";

            string sqlTxtDeleteRoleActors = "delete from RoleActors where RoleId = @RoleId and ActorId = @ActorId";

            int result = this.dmlOperable.ExeSql(sqlTxtDeleteRoleActors, new Dictionary<string, object>() {
                { "@RoleId",Role },
                { "@ActorId", ActorID}
            });

            return result;
        }

        public object SetRoleDataScope(string RoleID, string DataScopeID, string DataScopeValue)
        {
            //string sqlTxt = "insert into RoleDataScopes (Id, RoleId, DataScopeId, ScopeValue) values (@Id, @RoleId, @DataScopeId, @ScopeValue)";

            string sqlTxt = "insert into RoleDataScopes (Id, RoleId, DataScopeId, ScopeValue) values (@Id, @RoleId, @DataScopeId, @ScopeValue)";

            int result = this.dmlOperable.ExeSql(sqlTxt, new Dictionary<string, object>() {
                { "@Id", Guid.NewGuid().ToString() },
                { "@RoleId", RoleID },
                { "@DataScopeId", DataScopeID},
                { "@ScopeValue", DataScopeValue}
            });

            return result;
        }

        public object SetRoleOperation(string RoleID, string OperationID)
        {
            //string sqlTxt = "insert into RoleOperations (Id, RoleId, OperationId) values (@Id, @RoleId, @OperationId)";

            string sqlTxt = "insert into RoleOperations (Id, RoleId, OperationId) values (@Id, @RoleId, @OperationId)";

            int result = this.dmlOperable.ExeSql(sqlTxt, new Dictionary<string, object>() {
                { "@Id", Guid.NewGuid().ToString() },
                { "@RoleId", RoleID },
                { "@OperationId", OperationID}
            });

            return result;
        }

        public object GetRoles()
        {
            //string sqlTxt = "select Id, Name, RoleType, Description from roles";

            string sqlTxt = "select Id as Id, Name as Name, RoleType as RoleType, Description as Description from roles";

            var dbResult = this.dmlOperable.ExeReader(sqlTxt, null);

            return dbResult;
        }

        public object GetRoleActors(string RoleID)
        {
            //string sqlTxt = "select Id, RoleId, ActorId from roleactors where RoleId = @RoleId";

            string sqlTxt = "select Id as Id, RoleId as RoleId, ActorId as ActorId from roleactors where RoleId = @RoleId";

            var dbResult = this.dmlOperable.ExeReader(sqlTxt, new Dictionary<string, object>(){ { "@RoleId", RoleID} });

            return dbResult;
        }

        public object GetActorRoles(string ActorID)
        {
            //string sqlTxt = "select Id, RoleId, ActorId from roleactors where ActorId = @ActorId";

            string sqlTxt = "select Id as Id, RoleId as RoleId, ActorId as ActorId from roleactors where ActorId = @ActorId";

            var dbResult = this.dmlOperable.ExeReader(sqlTxt, new Dictionary<string, object>() { { "@ActorId", ActorID } });

            return dbResult;
        }

        public object GetOperations()
        {
            //string sqlTxt = "select Id, Name, DataType from operations";

            string sqlTxt = "select Id as Id, Name as Name, DataType as DataType from operations";

            var dbResult = this.dmlOperable.ExeReader(sqlTxt, null);

            return dbResult;
        }

        public object GetRoleOperations(string RoleID)
        {
            //string sqlTxt = "select Id, RoleId, OperationId from roleoperations";
            string sqlTxt = "select Id as Id, RoleId as RoleId, OperationId as OperationId from roleoperations";

            IDictionary<string, object> sqlParam = null;

            if (!string.IsNullOrEmpty(RoleID))
            {
                //sqlTxt = "select Id, RoleId, OperationId from roleoperations where RoleId = @RoleId";
                sqlTxt = "select Id as Id, RoleId as RoleId, OperationId as OperationId from roleoperations where RoleId = @RoleId";

                sqlParam = new Dictionary<string, object> { { "@RoleId", RoleID} };
            }

            var dbResult = this.dmlOperable.ExeReader(sqlTxt, sqlParam);

            return dbResult;
        }

        public object GetObjectAuthItems()
        {
            //string sqlTxt = "select Id, ObjectId, ActorId, OperationId from objectoperationauthitems";
            string sqlTxt = "select Id as Id, ObjectId as ObjectId, ActorId as ActorId, OperationId as OperationId from objectoperationauthitems";

            var dbResult = this.dmlOperable.ExeReader(sqlTxt, null);

            return dbResult;
        }

        public object GetRoleDataScopes(string RoleID)
        {
            //string sqlTxt = "select Id, RoleId, DataScopeId, ScopeValue from roledatascopes";

            string sqlTxt = "select Id as Id, RoleId as RoleId, DataScopeId as DataScopeId, ScopeValue as ScopeValue from roledatascopes";

            IDictionary<string, object> sqlParam = null;

            if (!string.IsNullOrEmpty(RoleID))
            {
                //sqlTxt = "select Id, RoleId, DataScopeId, ScopeValue from roledatascopes where RoleId = @RoleId";
                sqlTxt = "Id as Id, RoleId as RoleId, DataScopeId as DataScopeId, ScopeValue as ScopeValue from roledatascopes where RoleId = @RoleId";

                sqlParam = new Dictionary<string, object> { { "@RoleId", RoleID } };
            }

            var dbResult = this.dmlOperable.ExeReader(sqlTxt, sqlParam);

            return dbResult;
        }

        public bool RoleExists(string RoleID, string RoleName)
        {
            if (string.IsNullOrEmpty(RoleID) && string.IsNullOrEmpty(RoleName))
            {
                return false;
            }

            //string sqlTxt = "SELECT Id, Name FROM roles";
            string sqlTxt = "SELECT Id as Id, Name as Name FROM roles";

            IDictionary<string, object> sqlParam = null;

            if (!string.IsNullOrEmpty(RoleID) && !string.IsNullOrEmpty(RoleName))
            {
                //sqlTxt = "SELECT Id, Name FROM roles WHERE Id = @Id or Name = @Name";
                sqlTxt = "SELECT Id as Id, Name as Name FROM roles WHERE Id = @Id or Name = @Name";

                sqlParam = new Dictionary<string, object> { { "@Id", RoleID }, { "@Name", RoleName } };
            }
            else if (!string.IsNullOrEmpty(RoleID) && string.IsNullOrEmpty(RoleName))
            {
                //sqlTxt = "SELECT Id, Name FROM roles WHERE Id = @Id";
                sqlTxt = "SELECT Id as Id, Name as Name FROM roles WHERE Id = @Id";

                sqlParam = new Dictionary<string, object> { { "@Id", RoleID } };
            }
            else if (string.IsNullOrEmpty(RoleID) && !string.IsNullOrEmpty(RoleName))
            {
                //sqlTxt = "SELECT Id, Name FROM roles WHERE Name = @Name";
                sqlTxt = "SELECT Id as Id, Name as Name FROM roles WHERE Name = @Name";

                sqlParam = new Dictionary<string, object> { { "@Name", RoleName } };
            }

            var dbResult = this.dmlOperable.ExeReader(sqlTxt, sqlParam);

            if (dbResult != null && dbResult.Count > 0)
            {
                return true;
            }

            return false;
        }
    }
}
