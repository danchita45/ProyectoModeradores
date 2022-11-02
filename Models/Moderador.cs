namespace ProyectoModeradores.Models
{
    public class Moderador
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ApellidoP { get; set; }
        public string ApellidoM { get; set; }
        public string email { get; set; }
        public string Password { get; set; }
        public int statusId { get; set; }
        public int Area1 {get; set;}
        public int Area2 {get; set;}

        public Status status { get; set; }
        public string DArea1 { get; set; }
        public string DArea2 { get; set; }
        public string InstitucionId { get; set; }


        
    }
}
