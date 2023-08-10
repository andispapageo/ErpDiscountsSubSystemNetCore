using System.Data.Common;

namespace Integration.Tests.Interfaces
{
    public interface ITestDb
    {
        Task InitialiseAsync();

        DbConnection GetConnection();

        Task ResetAsync();

        Task DisposeAsync();
    }
}
