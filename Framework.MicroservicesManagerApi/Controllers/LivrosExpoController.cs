using Framework.AuthApi.Controllers;
using Framework.MicroservicesManagerApi.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Framework.MicroservicesManagerApi.Controllers
{
    public class LivrosExpoController : Controller
    {
        private readonly ILogger _logger;
        public LivrosExpoController(ILogger<LivrosExpoController> logger)
        {
            _logger = logger;
        }

    }
}
