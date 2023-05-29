using System;
using bootcamp_store_backend.Domain.Persistence;
using Microsoft.EntityFrameworkCore;

namespace bootcamp_store_backend.Infraestructure.Persistence
{
	public class UnitOfWork: IUnitOfWork
	{
		private readonly DbContext _dbContext;

		public UnitOfWork(DbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public IWork Init()
		{
			return new Work(_dbContext.Database.BeginTransaction());
		}
	}
}

