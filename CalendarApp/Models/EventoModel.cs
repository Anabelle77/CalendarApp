namespace CalendarApp.Models
{
    public class EventoModel{
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime Date { get; set; }

        public int? UsuarioId { get; set; }
        public List<UsuarioModel> Participantes { get; set; }

        public virtual UsuarioModel? Usuario { get; set; }
    }
}
