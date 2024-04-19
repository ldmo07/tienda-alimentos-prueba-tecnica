namespace Datos.Interfaces
{
    public interface IData<T, K>
    {
        public Task<T> insertar(K model);
        public Task<T> eliminar(Guid id);
        public Task<K> obtenerPorId(Guid id);
        public Task<List<K>> listar();
        public Task<T> actualizar(K model);
    }
}
