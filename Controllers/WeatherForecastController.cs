using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CustomSwaggerUI.Controllers
{
    /// <summary>
    /// Controlador respons�vel por fornecer previs�es meteorol�gicas.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    [SwaggerTag("Controller para gerenciar previs�es meteorol�gicas. Para mais informa��es, veja [a documenta��o aqui](https://sua-documentacao-url).")]
    public class WeatherForecastController(ILogger<WeatherForecastController> logger) : ControllerBase
    {
        private static readonly string[] Summaries =
        [
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        ];

        private readonly ILogger<WeatherForecastController> _logger = logger;

        /// <summary>
        /// Retorna uma lista de previs�es meteorol�gicas.
        /// </summary>
        /// <param name="days">O n�mero de dias para os quais a previs�o ser� gerada (m�nimo 1, m�ximo 10).</param>
        /// <returns>
        /// Uma cole��o de objetos <see cref="WeatherForecast"/> contendo a data, 
        /// temperatura em Celsius e um resumo da previs�o.
        /// </returns>
        /// <response code="200">Retorna a lista de previs�es meteorol�gicas.</response>
        /// <response code="400">O valor de 'days' est� fora do intervalo permitido (1 a 10).</response>
        /// <response code="500">Ocorreu um erro interno ao gerar a previs�o.</response>
        [HttpGet(Name = "GetWeatherForecast")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Get Weather Forecast")]
        public IEnumerable<WeatherForecast> Get(int days = 5)
        {
            if (days < 1 || days > 10)
            {
                throw new ArgumentOutOfRangeException(nameof(days), "O n�mero de dias deve estar entre 1 e 10.");
            }

            return Enumerable.Range(1, days).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
