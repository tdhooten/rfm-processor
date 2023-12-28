using Application.Models;
using Ganss.Excel;

namespace Application.Services;

public class TemplateService
{
    public async Task<Stream> CreateTransactionTemplateWorksheet()
    {
        List<Transaction> transactions =
        [
            new Transaction { Amount = 100, Date = DateOnly.FromDateTime(DateTime.Now), CustomerName = "Test Customer", }
        ];

        using MemoryStream resultStream = new();
        await new ExcelMapper().SaveAsync(resultStream, transactions, "Transactions");
        return new MemoryStream(resultStream.ToArray());
    }
    
}