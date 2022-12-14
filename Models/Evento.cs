namespace ProyectoModeradores.Models
{
    public class Evento
    {
        public int EventId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int ModeradorId { get; set; }

        public Moderador Moderador { get; set; }

        public int SalaId { get; set; }

        public Sala Sala { get; set; }

        public int AreaId { get; set; }

        public Area Area { get; set; }

        public int StatusId { get; set; }

        public Status Status { get; set; }
        public DateTime FInicio { get; set; }
        public DateTime FTermino { get; set; }
        List<Alumno> Alumnos { get; set; }

    }
}