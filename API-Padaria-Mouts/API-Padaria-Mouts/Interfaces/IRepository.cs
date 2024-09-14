namespace API_Padaria_Mouts.Interfaces
{
    public interface IRepository<T>
    {
        public abstract List<T> FindAll();
        public abstract T FindById(int id);
        public abstract T Create(T t);
        public abstract T Update(T t);
        public abstract void Delete(int id);
    }
}
