namespace ProyectoModeradores.Models
{
    public class Exposicion
    {
        public int ExposicionId { get; set; }
        public string Name { get; set; }
        public int AreaId { get; set; }
        public int ModeradorId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime finishTime { get; set; }

        public int Alumnos { get; set; }
        public int StatusId { get; set; }

    }
}
