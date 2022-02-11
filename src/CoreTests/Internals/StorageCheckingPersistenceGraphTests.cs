using Marten;
using Marten.Internal;
using Marten.Storage;
using Marten.Testing.Documents;
using NSubstitute;
using Shouldly;
using Xunit;

namespace CoreTests.Internals
{
    public class StorageCheckingPersistenceGraphTests
    {
        [Fact]
        public void only_calls_ensure_storage_exists_on_first_call()
        {
            var options = new StoreOptions();
            var inner = new ProviderGraph(options);
            var storage = Substitute.For<IMartenDatabase>();

            var graph = new StorageCheckingProviderGraph(storage, inner);

            var userPersistence = graph.StorageFor<User>();
            var userPersistence2 = graph.StorageFor<User>();
            var userPersistence3 = graph.StorageFor<User>();

            storage.Received(1).EnsureStorageExists(typeof(User));

            userPersistence.ShouldBeSameAs(userPersistence2);
            userPersistence.ShouldBeSameAs(userPersistence3);
        }
    }
}