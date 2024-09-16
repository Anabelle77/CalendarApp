using System.Threading;
using CalendarApp.Models;
using CalendarApp.Repositories.Interfaces;
using CalendarApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace CalendarApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase
    {
        private readonly IEventoRepositorio _eventoRepositorio;
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly EmailService _emailService;

        public EventoController(IEventoRepositorio eventoRepositorio, IUsuarioRepositorio usuarioRepositorio, EmailService emailService)
        {
            _eventoRepositorio = eventoRepositorio;
            _usuarioRepositorio = usuarioRepositorio;
            _emailService = emailService;

        }

        [HttpGet]
        public async Task<ActionResult<List<EventoModel>>> BuscarTodosEventos()
        {
            List<EventoModel> eventos = await _eventoRepositorio.BuscarTodosEventos();
            return Ok(eventos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EventoModel>> BuscarPorId(int id)
        {
            EventoModel evento = await _eventoRepositorio.BuscarPorId(id);
            return Ok(evento);
        }

        [HttpPost]
        public async Task<ActionResult<EventoModel>> Cadastrar([FromBody] EventoCreateDto eventoDto)
        {
            if (eventoDto.Date < DateTime.Now)
            {
                return BadRequest("A data do evento não pode ser no passado.");
            }

            var participantes = await _usuarioRepositorio.BuscarUsuariosPorEmails(eventoDto.ParticipantesEmails);
            if (participantes == null || participantes.Count != eventoDto.ParticipantesEmails.Count)
            {
                return BadRequest("Um ou mais e-mails participantes não foram encontrados.");
            }

            EventoModel evento = new EventoModel
            {
                Nome = eventoDto.Nome,
                Descricao = eventoDto.Descricao,
                Date = eventoDto.Date,
                UsuarioId = eventoDto.UsuarioId, 
                Participantes = participantes
            };

            EventoModel eventoCriado = await _eventoRepositorio.Adicionar(evento);

            AgendarLembretesEmail(eventoCriado);

            return Ok(eventoCriado);
        }
        private void AgendarLembretesEmail(EventoModel evento)
        {
            DateTime lembrete3DiasAntes = evento.Date.AddDays(-3);
            DateTime lembrete2HorasAntes = evento.Date.AddHours(-2);

            Task.Run(() => EnviarLembrete(lembrete3DiasAntes, evento, "Lembrete: Seu evento ocorrerá em 3 dias"));
            Task.Run(() => EnviarLembrete(lembrete2HorasAntes, evento, "Lembrete: Seu evento ocorrerá em 2 horas"));
        }

        private async Task EnviarLembrete(DateTime agendarPara, EventoModel evento, string assunto)
        {
            var delay = agendarPara - DateTime.Now;

            if (delay > TimeSpan.Zero)
            {
                await Task.Delay(delay);
            }

            string mensagem = $"Olá! O evento {evento.Nome} ocorrerá em breve: {evento.Date}";

            foreach (var participante in evento.Participantes)
            {
                _emailService.EnviarEmail(participante.Email, assunto, mensagem);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EventoModel>> Atualizar([FromBody] EventoModel eventoModel, int id)
        {
            if (eventoModel.Date < DateTime.Now)
            {
                return BadRequest("A data do evento não pode ser no passado.");
            }

            eventoModel.Id = id;
            EventoModel eventoAtualizado = await _eventoRepositorio.Atualizar(eventoModel, id);
            return Ok(eventoAtualizado);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Apagar(int id)
        {
            bool apagado = await _eventoRepositorio.Apagar(id);
            return Ok(apagado);
        }


    }
}
