using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProyectoDATA.DBContext;
using ProyectoDATA.Entidades;
using ProyectoDATA.Entidades.Mod_Seguridad;
using ProyectoDATA.Modelo_de_Datos;
using ProyectoDATA.Modelo_de_Datos.Mod_Seguridad;
using ProyectoDATA.Repositorios.Interfaces_de_Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProyectoDATA.Repositorios
{
    public class Repositorio<TEntity, TEntityModel> : IRepositorio<TEntity, TEntityModel> where TEntity : BaseEntity where TEntityModel : BaseModel
    {
        public readonly ApplicationDbContext _context;
        public readonly IMapper _mapper;

        public Repositorio(ApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public void Actualizar(TEntityModel entidad)
        {
            _context.Entry(_mapper.Map<TEntity>(entidad)).State = EntityState.Modified;
        }


        public void Actualizar(List<TEntityModel> entidades)
        {
            //en este metodo se actualiza solo el objeto pero no sus propiedades navigacionales
            _context.Entry(_mapper.Map<List<TEntity>>(entidades)).State = EntityState.Modified;
        }

        public void ActualizarProfundo(TEntityModel entidad)
        {
            //en este metodo se actualiza el objeto junto a todas sus propiedades navigacionales
            _context.Set<TEntity>().Update(_mapper.Map<TEntity>(entidad));
        }

        public void ActualizarProfundo(List<TEntityModel> entidades)
        {
            //en este metodo se actualizan los objetos junto a todas sus propiedades navigacionales
            _context.Set<TEntity>().UpdateRange((_mapper.Map<List<TEntity>>(entidades)));
        }

        public async Task<TEntityModel> Adicionar(TEntityModel entidad)
        {
            entidad.FechaCreado = DateTime.Now;
            return _mapper.Map<TEntityModel>((await _context.Set<TEntity>().AddAsync(_mapper.Map<TEntity>(entidad))).Entity);
        }

        public async Task Adicionar(List<TEntityModel> entidades)
        {
            List<Task> tareas = new List<Task>();
            entidades.ForEach(entidad => tareas.Add(Adicionar(entidad)));

            await Task.WhenAll(tareas);
        }

        public virtual Task<TEntityModel> Detallar(Guid id)
        {
            return Buscar(id);
        }

        public async Task<TEntityModel> Buscar(Expression<Func<TEntity, bool>> predicate, bool track = true)
        {
            //cuando el parametro track esta en true el todo cambio que se le haga al objeto devuelto sera guardado en BD cuando se llame al metodo SaveChanges
            return track ? _mapper.Map<TEntityModel>(await _context.Set<TEntity>().SingleOrDefaultAsync(predicate)) : _mapper.Map<TEntityModel>(await _context.Set<TEntity>().AsNoTracking().SingleOrDefaultAsync(predicate));
        }

        public async Task<TEntityModel> Buscar(Guid id, bool track = true)
        {
            //cuando el parametro track esta en true el todo cambio que se le haga al objeto devuelto sera guardado en BD cuando se llame al metodo SaveChanges
            return await Buscar(entidad => entidad.Id == id, track);
        }

        public async Task<TEntityModel> BuscarPrimero(bool track = true)
        {
            //cuando el parametro track esta en true el todo cambio que se le haga al objeto devuelto sera guardado en BD cuando se llame al metodo SaveChanges
            return track ? _mapper.Map<TEntityModel>(await _context.Set<TEntity>().FirstOrDefaultAsync()) : _mapper.Map<TEntityModel>(await _context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync());
        }

        public async Task<TEntityModel> BuscarPrimero(Expression<Func<TEntity, bool>> predicate, bool track = true)
        {
            //cuando el parametro track esta en true el todo cambio que se le haga al objeto devuelto sera guardado en BD cuando se llame al metodo SaveChanges
            return track ? _mapper.Map<TEntityModel>(await _context.Set<TEntity>().FirstOrDefaultAsync(predicate)) : _mapper.Map<TEntityModel>(await _context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(predicate));
        }

        public async Task<List<TEntityModel>> BuscarTodos(bool track = true)
        {
            //cuando el parametro track esta en true el todo cambio que se le haga al objeto devuelto sera guardado en BD cuando se llame al metodo SaveChanges
            return track ? _mapper.Map<List<TEntityModel>>(await _context.Set<TEntity>().ToListAsync()) : _mapper.Map<List<TEntityModel>>(await _context.Set<TEntity>().AsNoTracking().ToListAsync());
        }

        public async Task<List<TEntityModel>> BuscarTodos(Expression<Func<TEntity, bool>> predicate, bool track = true)
        {
            //cuando el parametro track esta en true el todo cambio que se le haga al objeto devuelto sera guardado en BD cuando se llame al metodo SaveChanges
            return track ? _mapper.Map<List<TEntityModel>>(await _context.Set<TEntity>().Where(predicate).ToListAsync()) : _mapper.Map<List<TEntityModel>>(await _context.Set<TEntity>().AsNoTracking().Where(predicate).ToListAsync());
        }

        public async Task<TEntityModel> BuscarUltimo(bool track = true)
        {
            //cuando el parametro track esta en true el todo cambio que se le haga al objeto devuelto sera guardado en BD cuando se llame al metodo SaveChanges
            return track ? _mapper.Map<TEntityModel>(await _context.Set<TEntity>().LastAsync()) : _mapper.Map<TEntityModel>(await _context.Set<TEntity>().AsNoTracking().LastAsync());
        }

        public async Task<TEntityModel> BuscarUltimo(Expression<Func<TEntity, bool>> predicate, bool track = true)
        {
            //cuando el parametro track esta en true el todo cambio que se le haga al objeto devuelto sera guardado en BD cuando se llame al metodo SaveChanges
            return track ? _mapper.Map<TEntityModel>(await _context.Set<TEntity>().LastAsync(predicate)) : _mapper.Map<TEntityModel>(await _context.Set<TEntity>().AsNoTracking().LastAsync(predicate));
        }

        public async Task<int> Contar()
        {
            return await _context.Set<TEntity>().CountAsync();
        }

        public async Task<int> Contar(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().CountAsync(predicate);
        }

        public async Task<TEntityModel> Eliminar(Guid id)
        {
            return Eliminar(await Buscar(id, false));
        }

        public TEntityModel Eliminar(TEntityModel entidad)
        {
            return _mapper.Map<TEntityModel>(_context.Set<TEntity>().Remove(_mapper.Map<TEntity>(entidad)).Entity);
        }

        public async Task EliminarTodos()
        {
            _context.Set<TEntity>().RemoveRange(await _context.Set<TEntity>().ToListAsync());
        }

        public async Task EliminarTodos(Expression<Func<TEntity, bool>> predicate)
        {
            _context.Set<TEntity>().RemoveRange(await _context.Set<TEntity>().Where(predicate).ToListAsync());
        }

        public async Task<bool> Existe()
        {
            return await _context.Set<TEntity>().AnyAsync();
        }

        public async Task<bool> Existe(Guid id)
        {
            return await _context.Set<TEntity>().AnyAsync(entidad => entidad.Id == id);
        }

        public async Task<bool> Existe(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().AnyAsync(predicate);
        }

        public virtual async Task<List<TEntityModel>> Listar(int paginaActual, int cantidadFilas)
        {
            return _mapper.Map<List<TEntityModel>>(await _context.Set<TEntity>().Skip((paginaActual - 1) * cantidadFilas).Take(cantidadFilas).OrderBy(entidad => entidad.Id).ToListAsync());
        }

        public IQueryable<TEntity> Include(IQueryable<TEntity> query, string includeProperties = "")
        {
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split
                    (new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }
            return query;
        }
    }
}
