namespace ProyectoModeradores.Models
{
    public class Moderador
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ApellidoP { get; set; }
        public string ApellidoM { get; set; }
        public string email { get; set; }

        public int statusId { get; set; }
        public string Area1 {get; set;}
        public string Area2 {get; set;}

        public Status status { get; set; }
        public Area Area { get; set; }
        public int InstitucionId { get; set; }


        
    }
}
