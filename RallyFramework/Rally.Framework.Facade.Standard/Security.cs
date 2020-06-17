using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IO = System.IO;
using System.IO;
using Rally.Lib.Utility.Xml;
using Rally.Framework.Core;

namespace Rally.Framework.Facade
{
    public class Security
    {
        public static object Regiser()
        {
            string configXmlPath = Rally.Framework.Authorization.ModuleConfiguration.DefaultResourceACConfigurationFilePath;
            string configXml = "";

            if (!IO.File.Exists(configXmlPath))
            {
                return null;
            }

            using (FileStream stream = new FileStream(configXmlPath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    configXml = reader.ReadToEnd();
                }
            }

            ResourceAuthConfiguration authConf = XmlUtility.XmlDeserialize<ResourceAuthConfiguration>(configXml, new Type[] { typeof(Resource), typeof(Subject), typeof(Scope), typeof(Action) }, "utf-8");

            IAuthorization authorizationManager = Facade.CreateAuthorizationManager();

            string regiseredOpId = null, registeredScopeId = null;

            string[] ops = authorizationManager.GetDataTypeOperations(null), scopes = authorizationManager.GetDataScopes(null);

            List<string> opsRegistered = null, scopesRegistered = null, rolesRegistered = null, allRoles = new List<string>();

            List<object> identityResults = null;

            if (authConf != null)
            {
                opsRegistered = new List<string>();
                scopesRegistered = new List<string>();
                //rolesRegistered = new List<string>();

                foreach (var resource in authConf.Resources)
                {
                    if (resource != null)
                    {
                        if ((resource.Actions != null) && (resource.Actions.Length > 0))
                        {
                            if (ops == null)
                            {
                                ops = new string[] { };
                            }

                            ops = ops.Select(o => o.ToLower()).ToArray();

                            foreach (var action in resource.Actions)
                            {
                                if ((action != null) && (!ops.Contains(action.ID.ToLower())) && (!opsRegistered.Contains(action.ID.ToLower())))
                                {
                                    regiseredOpId = (int)authorizationManager.RegisterOperation(resource.Name, action.ID, action.Name) >= 0 ? action.ID : null;

                                    if (!String.IsNullOrEmpty(regiseredOpId))
                                    {
                                        opsRegistered.Add(regiseredOpId.ToLower());
                                    }
                                }
                            }

                            if (Rally.Framework.Authorization.ModuleConfiguration.ShouldDeleteObsoleteOperationsOnRegistration)
                            {
                                var obsoleteRoleOps = authorizationManager.GetRoleOperations(null) as IList<IDictionary<string, object>>;
                                obsoleteRoleOps = obsoleteRoleOps == null ? null : obsoleteRoleOps.Where(ro => !opsRegistered.Contains((string)ro["OperationId"]) && !ops.Contains((string)ro["OperationId"])) as IList<IDictionary<string, object>>;

                                if (obsoleteRoleOps != null && obsoleteRoleOps.Count > 0)
                                {
                                    for (int i = 0; i < obsoleteRoleOps.Count; i++)
                                    {
                                        authorizationManager.DeleteOperation((string)obsoleteRoleOps[i]["OperationId"]);
                                    }
                                }

                                var obsoleteObjectAuthItems = authorizationManager.GetObjectAuthItems() as IList<IDictionary<string,object>>;
                                obsoleteObjectAuthItems = obsoleteObjectAuthItems == null ? null : obsoleteObjectAuthItems.Where(oo => !opsRegistered.Contains((string)oo["OperationId"]) && !ops.Contains((string)oo["OperationId"])) as IList<IDictionary<string, object>>;

                                if (obsoleteObjectAuthItems != null && obsoleteObjectAuthItems.Count > 0)
                                {
                                    for (int i = 0; i < obsoleteObjectAuthItems.Count; i++)
                                    {
                                        authorizationManager.DeleteOperation((string)obsoleteObjectAuthItems[i]["OperationId"]);
                                    }
                                }

                                var obsoleteOps = authorizationManager.GetOperations() as IList<IDictionary<string, object>>;
                                obsoleteOps = obsoleteOps == null ? null : obsoleteOps.Where(o => !opsRegistered.Contains((string)o["Id"]) && !ops.Contains((string)o["Id"])) as IList<IDictionary<string, object>>;

                                if (obsoleteOps != null && obsoleteOps.Count > 0)
                                {
                                    for (int i = 0; i < obsoleteOps.Count; i++)
                                    {
                                        authorizationManager.DeleteOperation((string)obsoleteOps[i]["Id"]);
                                    }
                                }
                            }
                        }

                        if ((resource.Scopes != null) && (resource.Scopes.Length > 0))
                        {
                            if (scopes == null)
                            {
                                scopes = new string[] { };
                            }

                            scopes = scopes.Select(s => s.ToLower()).ToArray();

                            foreach (var scope in resource.Scopes)
                            {
                                if ((scope != null) && (!scopes.Contains(scope.ID.ToLower())) && (!scopesRegistered.Contains(scope.ID.ToLower())))
                                {
                                    registeredScopeId = (int)authorizationManager.RegisterDataScope(resource.Name, scope.ID, scope.Name, scope.Type, resource.Key) >= 0 ? scope.ID : null;

                                    if (!String.IsNullOrEmpty(registeredScopeId))
                                    {
                                        scopesRegistered.Add(registeredScopeId.ToLower());
                                    }
                                }
                            }

                            if (Rally.Framework.Authorization.ModuleConfiguration.ShouldDeleteObsoleteDataScopesOnRegistration)
                            {
                                var obsoleteRoleDataScopes = authorizationManager.GetRoleDataScopes(null) as IList<IDictionary<string, object>>;
                                obsoleteRoleDataScopes = obsoleteRoleDataScopes == null ? null : obsoleteRoleDataScopes.Where(rd => ! !scopesRegistered.Contains((string)rd["DataScopeId"]) && !scopes.Contains((string)rd["DataScopeId"])) as IList<IDictionary<string, object>>;

                                if (obsoleteRoleDataScopes != null && obsoleteRoleDataScopes.Count > 0)
                                {
                                    for (int i = 0; i < obsoleteRoleDataScopes.Count; i++)
                                    {
                                        authorizationManager.DeleteDataScope((string)obsoleteRoleDataScopes[i]["DataScopeId"]);
                                    }
                                }

                                var obsoleteDataScopes = authorizationManager.GetDataScopes(null) as string[];
                                obsoleteDataScopes = obsoleteDataScopes == null ? null : obsoleteDataScopes.Where(ds => !scopesRegistered.Contains(ds) &&!scopes.Contains(ds)) as string[];

                                if (obsoleteDataScopes != null && obsoleteDataScopes.Length > 0)
                                {
                                    for (int i = 0; i < obsoleteDataScopes.Length; i++)
                                    {
                                        authorizationManager.DeleteDataScope(obsoleteDataScopes[i]);
                                    }
                                }
                            }
                        }

                        if ((resource.Subjects != null) && (resource.Subjects.Length > 0))
                        {
                            var accountManager = Facade.CreateAccountManager();
                            var userManager = Facade.CreateUserManager();

                            IDictionary<string, string> rolesToRegister = new Dictionary<string, string>();
                            IDictionary<string, string> roleDescriptions = new Dictionary<string, string>();

                            foreach (var subject in resource.Subjects)
                            {
                                if (subject.Type.ToLower() == "fixedrole")
                                {
                                    allRoles.Add(subject.ID);

                                    if(!authorizationManager.RoleExists(subject.ID, subject.Name))
                                    {
                                        rolesToRegister.Add(subject.ID, subject.Name);
                                        roleDescriptions.Add(subject.ID, subject.Description);
                                    }
                                }
                                else if (subject.Type.ToLower() == "fixeduser")
                                {
                                    bool userExists = userManager.UserExists(subject.ID, subject.Name);
                                    var identityUser = accountManager.GetAccount(subject.ID); 

                                    if (identityUser == null)
                                    {
                                        identityUser = accountManager.GetAccountByNickName(subject.Name); 
                                    }

                                    if ((identityUser != null) && userExists && Rally.Framework.Authorization.ModuleConfiguration.ShouldDeleteObsoleteUsersOnRegistration)
                                    {

                                    }

                                    if (identityUser == null && !userExists)
                                    {
                                        userManager.AddUser<Core.DomainModel.Account>(subject.ID, subject.Name, Rally.Framework.Authentication.ModuleConfiguration.DefaultFixedUserPassword, (u) => {

                                            string userId = u.ToString();

                                            var account = new Core.DomainModel.Account() {
                                                ID = userId,
                                                Name = subject.Name,
                                                NickName = subject.Name
                                            };

                                            accountManager.AddAccount(account);

                                            return account;
                                        });
                                    }
                                }
                            }

                            rolesRegistered = (int)authorizationManager.AddRoles(rolesToRegister, roleDescriptions) >= 0 ? rolesRegistered : null;

                            if (rolesRegistered == null)
                            {
                                rolesRegistered = new List<string>();
                            }

                            if (Rally.Framework.Authorization.ModuleConfiguration.ShouldDeleteObsoleteRolesOnRegistration)
                            {
                                var obsoleteRoleDataScopes = authorizationManager.GetRoleDataScopes(null) as IList<IDictionary<string, object>>;
                                obsoleteRoleDataScopes = obsoleteRoleDataScopes == null ?  null : obsoleteRoleDataScopes.Where(rd => !rolesRegistered.Contains((string)rd["RoleId"])) as IList<IDictionary<string, object>>;

                                if (obsoleteRoleDataScopes != null && obsoleteRoleDataScopes.Count > 0)
                                {
                                    for (int i = 0; i < obsoleteRoleDataScopes.Count; i++)
                                    {
                                        authorizationManager.DeleteDataScope((string)obsoleteRoleDataScopes[i]["DataScopeId"]);
                                    }
                                }

                                var obsoleteRoleOps = authorizationManager.GetRoleOperations(null) as IList<IDictionary<string, object>>;
                                obsoleteRoleOps = obsoleteRoleOps == null ? null : obsoleteRoleOps.Where(ro => !opsRegistered.Contains((string)ro["RoleId"]) && !ops.Contains((string)ro["RoleId"])) as IList<IDictionary<string, object>>;

                                if (obsoleteRoleOps != null && obsoleteRoleOps.Count > 0)
                                {
                                    for (int i = 0; i < obsoleteRoleOps.Count; i++)
                                    {
                                        authorizationManager.DeleteOperation((string)obsoleteRoleOps[i]["OperationId"]);
                                    }
                                }

                                var obsoleteRoles = authorizationManager.GetRoles() as IList<IDictionary<string, object>>;
                                obsoleteRoles = obsoleteRoles == null ? null : obsoleteRoles.Where(r => !rolesRegistered.Contains((string)r["Id"]) && !allRoles.Contains((string)r["Id"])) as IList<IDictionary<string, object>>;

                                if (obsoleteRoles != null && obsoleteRoles.Count > 0)
                                {
                                    for (int i = 0; i < obsoleteRoles.Count; i++)
                                    {
                                        authorizationManager.DeleteRole((string)obsoleteRoles[i]["Id"]);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return ((opsRegistered != null) || (scopesRegistered != null) || (rolesRegistered != null) || (identityResults != null)) ? new object[] { opsRegistered, scopesRegistered, rolesRegistered, identityResults } : null;
        }
    }
}
