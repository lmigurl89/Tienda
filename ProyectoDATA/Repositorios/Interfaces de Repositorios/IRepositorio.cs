using ProyectoDATA.Entidades;
using ProyectoDATA.Modelo_de_Datos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProyectoDATA.Repositorios.Interfaces_de_Repositorios
{
    public interface IRepositorio<TEntity, TEntityModel> where TEntity : class where TEntityModel : class
    {
        void Actualizar(TEntityModel entidad);
        void Actualizar(List<TEntityModel> entidades);
        void ActualizarProfundo(TEntityModel entidad);
        void ActualizarProfundo(List<TEntityModel> entidades);
        Task<TEntityModel> Adicionar(TEntityModel entidad);
        Task Adicionar(List<TEntityModel> entidades);
        Task<TEntityModel> Buscar(Guid id, bool track = true);
        Task<TEntityModel> BuscarPrimero(bool track = true );
        Task<List<TEntityModel>> BuscarTodos(bool track = true);
        Task<TEntityModel> BuscarUltimo(bool track = true);
        Task<int> Contar();
        Task<TEntityModel> Detallar(Guid id);
        Task<TEntityModel> Eliminar(Guid id);
        TEntityModel Eliminar(TEntityModel entidad);
        Task EliminarTodos();
        Task<bool> Existe();        
        Task<bool> Existe(Guid id);
        Task<List<TEntityModel>> Listar(int paginaActual, int cantidadFilas);
    }
}

