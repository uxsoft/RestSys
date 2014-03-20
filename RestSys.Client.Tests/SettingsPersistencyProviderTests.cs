using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using RestSys.Client.Exports;
using RestSys.Models.Exports;

namespace RestSys.Client.Tests
{
    [TestClass]
    public class SettingsPersistencyProviderTests
    {
        [TestMethod]
        public void ComprehensiveTest()
        {
            SettingsPersistenceProvider p = new SettingsPersistenceProvider();

            SampleEntity o1 = new SampleEntity() { Id = 1, Data = "SampleEntity1" };
            SampleEntity o2 = new SampleEntity() { Id = 2, Data = "SampleEntity2" };
            SampleEntity o3 = new SampleEntity() { Id = 3, Data = "SampleEntity3" };
            SampleEntity o4 = new SampleEntity() { Id = 4, Data = "SampleEntity4" };
            SampleEntity o5 = new SampleEntity() { Id = 5, Data = "SampleEntity5" };

            //Add
            p.Add(o1);
            p.Add(o2);
            p.Add(o3);
            p.Add(o4);
            p.Add(o5);

            Assert.AreEqual(5, p.Get<SampleEntity>().Count());

            //Find
            SampleEntity f3 = p.Find<SampleEntity>(3);
            Assert.AreEqual(o3.Id, f3.Id);
            Assert.AreEqual(o3.Data, f3.Data);

            //Delete
            p.Delete(p.Find<SampleEntity>(4));
            Assert.AreEqual(4, p.Get<SampleEntity>().Count());
            Assert.AreEqual(null, p.Find<SampleEntity>(4));

            //Delete - Delete on nonexistent item does nothing
            p.Delete(p.Find<SampleEntity>(4));
            Assert.AreEqual(4, p.Get<SampleEntity>().Count());
            Assert.AreEqual(null, p.Find<SampleEntity>(4));

            //Get
            SampleEntity f5 = p.Get<SampleEntity>().Last();
            Assert.AreEqual(o5.Id, f5.Id);
            Assert.AreEqual(o5.Data, f5.Data);

            //Update
            o2.Data = "UpdatedEntity2";
            p.Update(o2);
            SampleEntity f2 = p.Find<SampleEntity>(2);
            Assert.AreEqual(o2.Id, f2.Id);
            Assert.AreEqual(o2.Data, f2.Data);

            //Error Handling
            p.Add<SampleEntity>(null);
            p.Update<SampleEntity>(null);
            p.Delete<SampleEntity>(null);
        }

        [TestMethod]
        public void SerializationTest()
        {
            SettingsPersistenceProvider p = new SettingsPersistenceProvider();
            SampleEntity o1 = new SampleEntity() { Id = 1, Data = "SampleEntity1" };

            string serialized = SettingsPersistenceProvider.Serialize(o1);
            SampleEntity o2 = SettingsPersistenceProvider.Deserialize<SampleEntity>(serialized);

            Assert.AreEqual(o1.Id, o2.Id);
            Assert.AreEqual(o1.Data, o2.Data);
        }

        public class SampleEntity : IRSEntity
        {
            public int Id { get; set; }
            public string Data { get; set; }
        }
    }
}
