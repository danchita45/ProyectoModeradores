namespace ProyectoModeradores.Models
{
    public class Sala
    {
        public int SalaId { get; set; }
        public string Code { get; set; } 
        public string Description { get; set; }

        public int StatusId { get; set; }
        public Area Area { get; set; }
        public Status Status { get; set; }

    }
}
