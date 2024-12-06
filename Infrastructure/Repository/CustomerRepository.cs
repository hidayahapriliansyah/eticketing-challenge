using eticketing.Infrastructure.Database;
using eticketing.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eticketing.Infrastructure.Repository;

public class CustomerRepository(ETicketingDbContext dbContext)
{
    private readonly ETicketingDbContext _dbContext = dbContext;

    public async Task<Customer> CreateCustomer(Customer customerEntity)
    {
        _dbContext.Set<Customer>().Add(customerEntity);
        await _dbContext.SaveChangesAsync();
        return customerEntity;
    }

    public async Task<Customer?> GetCustomerByUsernameAsync(string username)
    {
        return await _dbContext.Customer.FirstOrDefaultAsync(c => c.Username == username);
    }

    public async Task<Customer?> GetCustomerByEmailAsync(string email)
    {
        return await _dbContext.Customer.FirstOrDefaultAsync(c => c.Email == email);
    }
}
