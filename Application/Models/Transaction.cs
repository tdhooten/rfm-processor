namespace Application.Models;

public class Transaction
{
    public decimal Amount { get; set; }
    
    public DateOnly Date { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string CustomerFirstName { get; set; } = string.Empty;
    public string CustomerLastName { get; set; } = string.Empty;
    public string CustomerCompany { get; set; } = string.Empty;
    public string CustomerEmail { get; set; } = string.Empty;
    public string CustomerPhoneNumber { get; set; } = string.Empty;
    public string CustomerAddress1 { get; set; } = string.Empty;
    public string CustomerAddress2 { get; set; } = string.Empty;
    public string CustomerAddress3 { get; set; } = string.Empty;
    public string CustomerCity { get; set; } = string.Empty;
    public string CustomerState { get; set; } = string.Empty;
    public string CustomerPostalCode { get; set; } = string.Empty;
    public string CustomerCountry { get; set; } = string.Empty;
}