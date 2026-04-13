namespace VoucherSystem.Domain.Enums;

public enum CampaignStatus
{
    Draft,
    Active,
    Paused,
    Closed,
    BudgetExhausted, // This means that the BudgetCap is reached, for example: if you announe $5000 in a Compaign,
    // Once the total sales reached 5000$, it will automatically stop the compaign saying it si exhausted.
}
