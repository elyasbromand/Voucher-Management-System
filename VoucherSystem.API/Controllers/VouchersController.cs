using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using VoucherSystem.Application.Vouchers.Commands;
using VoucherSystem.Application.Vouchers.Queries;

namespace VoucherSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VouchersController : ControllerBase
{
    private readonly IMediator _mediator;

    public VouchersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateVoucherCommand command,
        CancellationToken cancellationToken
    )
    {
        var id = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetByCode), new { code = "" }, id);
    }

    [HttpGet("{code}")]
    public async Task<ActionResult<VoucherSystem.Application.DTOs.VoucherDto>> GetByCode(
        string code,
        CancellationToken cancellationToken
    )
    {
        var dto = await _mediator.Send(
            new GetVoucherByCodeQuery { Code = code },
            cancellationToken
        );
        if (dto == null)
            return NotFound();
        return Ok(dto);
    }

    [HttpGet("campaign/{campaignId}")]
    public async Task<
        ActionResult<IReadOnlyList<VoucherSystem.Application.DTOs.VoucherDto>>
    > GetByCampaign(Guid campaignId, CancellationToken cancellationToken)
    {
        var list = await _mediator.Send(
            new GetVouchersByCampaignQuery { CampaignId = campaignId },
            cancellationToken
        );
        return Ok(list);
    }
}
