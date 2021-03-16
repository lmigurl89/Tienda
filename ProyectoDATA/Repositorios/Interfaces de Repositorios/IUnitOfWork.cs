using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProyectoDATA.Repositorios.Interfaces_de_Repositorios
{
    public interface IUnitOfWork : IDisposable
    {
        #region repositorios
        ITrazaRepositorio Trazas { get; }
        IOrdenRepositorio Ordenes { get; }
        IProductoRepositorio Productos { get; }
        #endregion

        #region metodos
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        #endregion
    }
}

