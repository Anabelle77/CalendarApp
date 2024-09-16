namespace CalendarApp.Models
{
    public class EventoDto
    {
        public string Nome { get; set; }      
        public string Descricao { get; set; } 
        public DateTime Date { get; set; }
        public int UsuarioId { get; set; }
        public List<string> ParticipantesEmails { get; set; }

        public EventoDto()  
        {
            ParticipantesEmails = new List<string>();
        }
    }
}
