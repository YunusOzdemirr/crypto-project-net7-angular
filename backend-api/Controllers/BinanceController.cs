using crypto_api.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace crypto_api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BinanceController : Controller
{

  [HttpGet]
  public async Task<IActionResult> Get()
  {
    var data=BinanceHub.TickerDictionary;
    return Ok(data);
  }
}