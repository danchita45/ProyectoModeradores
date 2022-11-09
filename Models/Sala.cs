namespace ProyectoModeradores.Models
{
    public class Sala
    {
        public int SalaId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int StatusId { get; set; }
        public int AreaId { get; set; }
        public string AreaD { get; set; }
        public Area Area { get; set; }
        public Status Status { get; set; }
        public int ModeradorId { get; set; }
        public string Fecha { get; set; }
        public string Bloque { get; set; }
        public string Salon { get; set; }
        public string Ubicacion { get; set; }

    }
}
