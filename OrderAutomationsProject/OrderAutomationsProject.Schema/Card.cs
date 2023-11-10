namespace OrderAutomationsProject.Schema;
public class CardRequest
{
    public string CardNumber { get; set; }
    public string ExpiryDate { get; set; }
    public string CVV { get; set; }
    public decimal ExpenseLimit { get; set; }
    public string CardHolderType { get; set; }
}

public class CardResponse
{
    public int Id { get; set; }
    public string CardNumber { get; set; }
    public string ExpiryDate { get; set; }
    public string CVV { get; set; }
    public decimal ExpenseLimit { get; set; }
    public int DealerId { get; set; }
    public string BankName { get; set; }
    public string CardHolderType { get; set; }
}

public class UpdateCardRequest
{
    public decimal ExpenseLimit { get; set; }
}
