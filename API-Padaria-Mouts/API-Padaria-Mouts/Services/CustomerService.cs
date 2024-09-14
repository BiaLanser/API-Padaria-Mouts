using API_Padaria_Mouts.Models;
using API_Padaria_Mouts.Repositories;
using API_Padaria_Mouts.Utilities;

namespace API_Padaria_Mouts.Services
{
    public class CustomerService
    {
        private readonly CustomerRepository _repo;
        public CustomerService(CustomerRepository repo)
        {
            _repo = repo;
        }

        public Customer Create(Customer customer)
        {
            if (!Utility.validDocument(customer.Document))
                throw new Exception("Invalid document");
            return _repo.Create(customer);
        }

        public List<Customer> FindAll()
        {
            return (List<Customer>)_repo.FindAll();
        }

        public void Delete(int id)
        {
            Get(id);
            _repo.Delete(id);
        }

        public Customer Get(int id)
        {
            var existingCustomer = _repo.FindById(id);
            if (existingCustomer == null)
            {
                throw new Exception("Customer does not exits");
            }
            return _repo.FindById(id);
        }
        public void Update(Customer customer)
        {
            if (!Utility.validDocument(customer.Document))
                throw new Exception("Invalid document");

            Get(customer!.Id);
            _repo.Update(customer);
        }

        

    }
}
