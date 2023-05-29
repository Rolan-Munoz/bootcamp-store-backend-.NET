using System;
namespace bootcamp_store_backend.Domain.Persistence
{
	public interface IWork: IDisposable
	{
		void Complete();
		void Rollback();
	}
}

