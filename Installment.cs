namespace PayoutEngine;

public class Installment
{
    public int MonthNumber { get; set; }
    public decimal Principal { get; set; } 
    public decimal Interest { get; set; }  
    public decimal TotalAmount => Principal + Interest;
    public DateTime DueDate { get; set; }
}