using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ViagemYamaha.Core.Contracts.Api;
using ViagemYamaha.Core.Contracts.Services;
using ViagemYamaha.Core.Contracts.Ui;

namespace ViagemYamaha.API.Controllers
{
    public class RotasController : BaseController
    {
        private readonly IRotaService _rotaService;

        public RotasController(IRotaService rotaService)
        {
            _rotaService = rotaService;
        }

        /// <summary>
        /// Pesquisa Melhor Rota (preço)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResult))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(SuccessResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResult))]
        public async Task<IActionResult> GetAsync([FromQuery] GetRotaRequest request)
        {
            var result = await _rotaService.ObterMelhorRotaAsync(request);
            return CustomResponse(result);
        }

        /// <summary>
        /// Insere nova Rota
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(SuccessResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResult))]
        public async Task<IActionResult> PostAsync(PostRotaRequest request)
        {
            await _rotaService.AdicionarRotaAsync(request);
            return Ok();
        }

    }
}
