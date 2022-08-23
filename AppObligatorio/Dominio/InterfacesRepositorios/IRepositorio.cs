using System.Collections.Generic;

namespace Dominio.InterfacesRepositorios
{
	public interface IRepositorio<T>
	{
		public bool Add(T obj);

		public bool Remove(object id);

		public bool Update(T obj);

		public T FindById(object id);

		public IEnumerable<T> FindAll();

	}

}

