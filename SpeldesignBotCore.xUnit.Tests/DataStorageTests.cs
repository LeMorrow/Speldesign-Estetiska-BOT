using SpeldesignBotCore.Storage;
using SpeldesignBotCore.Storage.Implementations;
using Xunit;

namespace SpeldesignBotCore.xUnit.Tests
{
    public class DataStorageTests
    {
        [Fact]
        public void StorageDefaultToJsonTest()
        {
            var storage = Unity.Resolve<IDataStorage>();
            Assert.True(storage is JsonStorage);
        }

        [Fact]
        public void InMemoryStorageOverrideTest()
        {
            const string key = "KEY";
            const string firstString = "I should be overriden";
            const string expected = "I should override the previous data";

            IDataStorage storage = new InMemoryStorage();

            storage.StoreObject(firstString, key);
            storage.StoreObject(expected, key);

            var actual = storage.RestoreObject<string>(key);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void InMemoryStorageRestoreWrongKeyTest()
        {
            IDataStorage storage = new InMemoryStorage();
            Assert.Throws<System.ArgumentException>(() => storage.RestoreObject<string>("fake-key"));
        }
    }
}