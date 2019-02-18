using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PrinterManagerProject.EF.Test
{
    /// <summary>
    /// 数据同步测试
    /// </summary>
    [TestClass]
    public class DataSyncTest
    {
        DataSync dataSyncManager = new DataSync();
        /// <summary>
        /// 医嘱同步测试
        /// </summary>
        [TestMethod]
        public void SyncOrder()
        {
            var startTime = DateTime.Now;
            MapperConfig.Config();
            dataSyncManager.SyncOrder(DateTime.Now);

            var ms = (DateTime.Now - startTime).TotalMilliseconds;
            System.Console.WriteLine(ms);
        }
    }
}
