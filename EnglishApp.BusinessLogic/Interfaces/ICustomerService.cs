using EnglishApp.BusinessLogic.BaseClasses;
using EnglishApp.BusinessLogic.DTOs.Authentications;

namespace EnglishApp.BusinessLogic.Interfaces;

public interface ICustomerService
{
    string GetCurrentCustomerName();
    string GetCurrentUserId();
    Task<StatusCode> Update(CustomerDTO customerDto);
    Task<StatusCode> Create(CustomerDTO customerDto);
    Task<CustomerDTO?> GetCustomerDtoByUserId(string userId);
    Task<CustomerDTO?> GetCustomerDtoById(int id);
    bool CustomerExists(int id);
}