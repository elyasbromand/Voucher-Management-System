using MediatR;
using Microsoft.AspNetCore.Mvc;
using VoucherSystem.Application.Campaigns.Commands;

namespace VoucherSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CampaignsController : ControllerBase
{
    private readonly IMediator _mediator;

    public CampaignsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateCampaignCommand command,
        CancellationToken cancellationToken
    )
    {
        var id = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = id }, id);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<VoucherSystem.Application.DTOs.CampaignDto>> GetById(
        Guid id,
        [FromServices] VoucherSystem.Domain.Interfaces.ICampaignRepository repo,
        CancellationToken cancellationToken
    )
    {
        var campaign = await repo.GetByIdAsync(id, cancellationToken);
        if (campaign == null)
            return NotFound();
        var dto = new VoucherSystem.Application.DTOs.CampaignDto
        {
            Id = campaign.Id,
            Name = campaign.Name,
            StartDate = campaign.StartDate,
            EndDate = campaign.EndDate,
            BudgetCap = campaign.BudgetCap,
            TotalDiscountIssued = campaign.TotalDiscountIssued,
            Status = campaign.Status.ToString(),
            CreatedAt = campaign.CreatedAt,
        };
        return Ok(dto);
    }
}
