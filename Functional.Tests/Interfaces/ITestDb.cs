using System.Data.Common;

namespace Functional.Tests.Interfaces
{
    public interface ITestDb
    {
        Task InitialiseAsync();

        DbConnection GetConnection();

        Task ResetAsync();

        Task DisposeAsync();
    }
}
