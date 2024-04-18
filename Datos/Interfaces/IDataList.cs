namespace Datos.Interfaces
{
    public interface IDataList<T>
    {
        public Task<List<T>> listar();
    }
}
