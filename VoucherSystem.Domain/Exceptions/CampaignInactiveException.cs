namespace VoucherSystem.Domain.Exceptions;

public sealed class CampaignInactiveException : Exception
{
    public CampaignInactiveException(string campaignName)
        : base($"Campaign '{campaignName}' is not currently active.") { }
}
