namespace CalendarApp.Models
{
    public class EventoCreateDto
    {
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public int UsuarioId { get; set; } 
        public List<string> ParticipantesEmails { get; set; }
        public EventoCreateDto()
        {
            ParticipantesEmails = new List<string>();
        }

    }
}
