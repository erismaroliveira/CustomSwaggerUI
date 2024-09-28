using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CustomSwaggerUI.Filters;

public class ValidationOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        // Adiciona informações sobre validação
        operation.Description += " \n\n **Validações:** Este endpoint pode ter as seguintes validações:\n";

        // Adiciona um link para a documentação de validação
        operation.Description += "[Clique aqui para mais informações sobre validações.](https://validator.swagger.io/validator/debug?url=https%3A%2F%2Flocalhost:7069%2Fv2%2Fswagger%2Fv2%2Findex.html)";

        // Adiciona uma resposta para 405 - Method Not Allowed
        operation.Responses.Add("405", new OpenApiResponse
        {
            Description = "Método não permitido. Verifique a solicitação e tente novamente."
        });
    }
}