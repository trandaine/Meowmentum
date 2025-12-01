using EnglishApp.BusinessLogic.BaseClasses;

namespace EnglishApp.BusinessLogic.Interfaces
{
    public interface ITransactionService
    {
        Task<StatusCode> Checkout();
    }
}
