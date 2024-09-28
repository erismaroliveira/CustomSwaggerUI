using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CustomSwaggerUI.Controllers
{
    /// <summary>
    /// Controlador responsável por fornecer previsões meteorológicas.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    [SwaggerTag("Controller para gerenciar previsões meteorológicas. Para mais informações, veja [a documentação aqui](https://sua-documentacao-url).")]
    public class WeatherForecastController(ILogger<WeatherForecastController> logger) : ControllerBase
    {
        private static readonly string[] Summaries =
        [
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        ];

        private readonly ILogger<WeatherForecastController> _logger = logger;

        /// <summary>
        /// Retorna uma lista de previsões meteorológicas.
        /// </summary>
        /// <param name="days">O número de dias para os quais a previsão será gerada (mínimo 1, máximo 10).</param>
        /// <returns>
        /// Uma coleção de objetos <see cref="WeatherForecast"/> contendo a data, 
        /// temperatura em Celsius e um resumo da previsão.
        /// </returns>
        /// <response code="200">Retorna a lista de previsões meteorológicas.</response>
        /// <response code="400">O valor de 'days' está fora do intervalo permitido (1 a 10).</response>
        /// <response code="500">Ocorreu um erro interno ao gerar a previsão.</response>
        [HttpGet(Name = "GetWeatherForecast")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Get Weather Forecast")]
        public IEnumerable<WeatherForecast> Get(int days = 5)
        {
            if (days < 1 || days > 10)
            {
                throw new ArgumentOutOfRangeException(nameof(days), "O número de dias deve estar entre 1 e 10.");
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
