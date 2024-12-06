using eticketing.Application.Security;
using eticketing.Exceptions;
using eticketing.Http.Responses;
using eticketing.Infrastructure.Database;
using eticketing.Infrastructure.Repository;
using eticketing.Models;

namespace eticketing.Application.Services;

public class CustomerService(CustomerRepository customerRepository)
{
    private readonly CustomerRepository _customerRepository = customerRepository;

    public async Task<ApiResponse<RegisterCustomerReponse>> CreateCustomer(
        RegisterCustomerRequest request
    )
    {
        var customerByEmail = await _customerRepository.GetCustomerByEmailAsync(request.Email);
        if (customerByEmail != null)
        {
            throw new ConflictException("Email already used");
        }
        var customerByUsername = await _customerRepository.GetCustomerByUsernameAsync(
            request.Username
        );
        if (customerByUsername != null)
        {
            throw new ConflictException("Username already used");
        }

        var hashedPassword = PasswordHasher.HashPassword(request.Password);

        var customerEntity = new Customer
        {
            Id = Guid.NewGuid(),
            Username = request.Username,
            Email = request.Email,
            Name = request.Name,
            Password = hashedPassword,
        };

        var createdCustomer = await _customerRepository.CreateCustomer(customerEntity);

        var response = new ApiResponse<RegisterCustomerReponse>
        {
            Success = true,
            Message = "success register",
            Data = new RegisterCustomerReponse { Id = createdCustomer.Id },
        };

        return response;
    }
}
