using API_Padaria_Mouts.Models;
using API_Padaria_Mouts.Repositories;
using API_Padaria_Mouts.Utilities;

namespace API_Padaria_Mouts.Services
{
    public class SaleService
    {
        private readonly SalesRepository _repo;
        public SaleService(SalesRepository repo)
        {
            _repo = repo;
        }

        public Sale Create(Sale sale)
        {
            return _repo.Create(sale);
        }

        public List<Sale> FindAll()
        {
            return (List<Sale>)_repo.FindAll();
        }

        public void Delete(int id)
        {
            Get(id);
            _repo.Delete(id);
        }

        public Sale Get(int id)
        {
            var existingSale = _repo.FindById(id);
            if (existingSale == null)
            {
                throw new Exception("Sale does not exits");
            }
            return _repo.FindById(id);
        }
        public void Update(Sale sale)
        { 
            Get(sale.Id);
            _repo.Update(sale);
        }

        public List<Sale> GetSalesByDocument(string document)
        {
            return _repo.FindByDocument(document);
        }

    }
}
