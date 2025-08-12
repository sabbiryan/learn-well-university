using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearnWellUniversity.WebApi.Controllers.Bases
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public abstract class ApiControllerV1 : ControllerBase
    {

    }
}
