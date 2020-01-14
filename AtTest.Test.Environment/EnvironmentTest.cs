using System;
using System.IO;
using System.Reflection;
using System.Xml.Linq;
using Signum.Engine;
using Signum.Engine.Authorization;
using Signum.Engine.Maps;
using Signum.Engine.Operations;
using Signum.Utilities;
using AtTest.Logic;
using Xunit;

namespace AtTest.Test.Environment
{
    public class EnvironmentTest
    {
        [Fact]
        public void GenerateEnvironment()
        {
            var authRules = XDocument.Load(@"..\..\..\..\AtTest.Load\AuthRules.xml");

            AtTestEnvironment.Start(includeDynamic: false);

            Administrator.TotalGeneration();

            Schema.Current.Initialize();

            OperationLogic.AllowSaveGlobally = true;

            using (AuthLogic.Disable())
            {
                AtTestEnvironment.LoadBasics();

                AuthLogic.LoadRoles(authRules);
                AtTestEnvironment.LoadEmployees();
                AtTestEnvironment.LoadUsers();
                AtTestEnvironment.LoadProducts();
                AtTestEnvironment.LoadCustomers();
                AtTestEnvironment.LoadShippers();

                AuthLogic.ImportRulesScript(authRules, interactive: false)!.PlainSqlCommand().ExecuteLeaves();
            }

            OperationLogic.AllowSaveGlobally = false;
        }
    }
}
