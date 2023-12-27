using Application.Models;
using Ganss.Excel;

namespace Application.Services;

public class CalculationService
{
    public async Task<Stream> CalculateMetrics(Stream stream)
    {
        List<Customer> customers = [];

        var import = new ExcelMapper(stream);
        await stream.DisposeAsync();
        IEnumerable<Transaction>? transactionImport = import.Fetch<Transaction>();
        var transactions = transactionImport.ToList();

        // Create a collection of all customers with transactions in the provided file
        foreach (Transaction transaction in transactions)
        {
            // Check if customer is already in list, otherwise add to list
            if (!customers.Any(x => x.Name == transaction.CustomerName))
            {
                Customer newCustomer = new()
                {
                    Name = transaction.CustomerName,
                    FirstName = transaction.CustomerFirstName,
                    LastName = transaction.CustomerLastName,
                    Company = transaction.CustomerCompany,
                    Email = transaction.CustomerEmail,
                    PhoneNumber = transaction.CustomerPhoneNumber,
                    Address1 = transaction.CustomerAddress1,
                    Address2 = transaction.CustomerAddress2,
                    Address3 = transaction.CustomerAddress3,
                    City = transaction.CustomerCity,
                    State = transaction.CustomerState,
                    PostalCode = transaction.CustomerPostalCode,
                    Country = transaction.CustomerCountry
                };

                customers.Add(newCustomer);
            }
        }

        // Create a dictionary of transactions per customer
        var transactionDictionary =
            transactions.GroupBy(x => x.CustomerName).ToDictionary(g => g.Key, g => g.OrderBy(x => x.CustomerName).ToList());

        // Calculate RFM metrics for each customer in list
        foreach (Customer customer in customers)
        {
            if (!transactionDictionary.TryGetValue(customer.Name, out List<Transaction>? ownTransactions))
            {
                throw new KeyNotFoundException($"Failed to find a key for customer {customer} in the transaction dictionary!");
            }

            customer.TransactionCount = ownTransactions.Count();
            customer.TransactionTotal = ownTransactions.Sum(x => x.Amount);
            customer.MostRecentTransaction = ownTransactions.Max(x => x.Date);
        }

        using MemoryStream resultStream = new();
        await new ExcelMapper().SaveAsync(resultStream, customers, "Customer RFM Metrics");
        return new MemoryStream(resultStream.ToArray());
    }
}