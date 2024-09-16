namespace CalendarApp.Models
{
    public class UsuarioDto
    {
        public required string Nome { get; set; }
        public required string Email { get; set; }
        public required string Senha { get; set; }
        public string ConfirmarSenha {  get; set; }
    }
}
