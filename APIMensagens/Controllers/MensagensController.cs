using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Azure.ServiceBus;
using APIMensagens.Models;

namespace APIMensagens.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MensagensController : ControllerBase
    {
        [HttpPost]
        public object Post(
            [FromServices]IConfiguration config,
            Mensagem mensagem)
        {
            var body = Encoding.UTF8.GetBytes(mensagem.Conteudo);

            var client = new QueueClient(
                config["AzureServiceBus:ConnectionString"],
                config["AzureServiceBus:Queue"],
                ReceiveMode.ReceiveAndDelete);
            client.SendAsync(new Message(body)).Wait();

            return new
            {
                Resultado = "Mensagem processada com sucesso!"
            };
        }
    }
}