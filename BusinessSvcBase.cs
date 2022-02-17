using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using log4net.Config;


namespace mhcb.EntityHelper
{
    public class BusinessSvcBase
    {
        public StringBuilder sb1 = new StringBuilder("");
        public StringBuilder sb2 = new StringBuilder("");
        public StringBuilder sb3 = new StringBuilder("");

        public string _Server;
        public string _DbName;
        public string _UserName;
        public string _Password;

        public  static readonly ILog _logWriter = LogManager.GetLogger(typeof(BusinessSvcBase));

        public string output { get; set; }

        public BusinessSvcBase()
        {
        }

        public BusinessSvcBase(string server, string dbName, string userName, string password)
        {
            _Server = server;
            _DbName = dbName;
            _UserName = userName;
            _Password = password;

            //BasicConfigurator.Configure();
            XmlConfigurator.Configure();
        }
    }


}
