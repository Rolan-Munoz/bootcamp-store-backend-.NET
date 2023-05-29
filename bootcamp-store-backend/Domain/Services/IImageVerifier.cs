using System;
namespace bootcamp_store_backend.Domain.Services
{
	public interface IImageVerifier
	{
		bool IsImage(byte[] bytes);
	}
}

