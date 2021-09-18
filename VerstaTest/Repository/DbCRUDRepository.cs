using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VerstaTest.Models;

namespace VerstaTest.Repository
{
	public interface IDbCRUDRepository<ModelT> where ModelT : ModelBase
	{
		Task<List<ModelT>> GetAll();
		Task<ModelT> Get(int id);
		Task Insert(ModelT entity);
		Task Update(ModelT entity);
		Task Delete(ModelT entity);
		Task SaveChanges();
	}

	public class DbCRUDRepository<ContextT, ModelT> : IDbCRUDRepository<ModelT> 
		where ModelT : ModelBase
		where ContextT : DbContext
	{
		private readonly ContextT context;
		private DbSet<ModelT> entities;

		public DbCRUDRepository(ContextT context)
		{
			this.context = context;
			entities = context.Set<ModelT>();
		}

		private bool CheckNull(ModelT input)
		{
			if (input == null)
				throw new ArgumentNullException("input entity was null");
			return true;
		}

		public async Task<ModelT> Get(int id)
		{
			return await entities.FirstOrDefaultAsync(or => or.Id == id);
		}

		public async Task<List<ModelT>> GetAll()
		{
			return await entities.ToListAsync();
		}

		public async Task Insert(ModelT entity)
		{
			CheckNull(entity);
			await entities.AddAsync(entity);
			await SaveChanges();
		}

		public async Task Delete(ModelT entity)
		{
			CheckNull(entity);
			entities.Remove(entity);
			await SaveChanges();
		}

		public async Task Update(ModelT entity)
		{
			CheckNull(entity);
			entities.Update(entity);
			await SaveChanges();
		}

		public async Task SaveChanges()
		{
			await context.SaveChangesAsync();
		}
	}
}
