namespace ProyectoHDP.Models
{
    public class Empleados
    {
        public int iD { get; set; }
        public string nombre { get; set; } = string.Empty;
        public string tipoContrato { get; set; } = string.Empty;
        public string pais { get; set; } = string.Empty;
        public string empresa { get; set; } = string.Empty;
        public double salario { get; set; }
        public DateOnly fechaContrato { get; set; }
        public DateOnly fechaRenuncia { get; set; }
        public DateOnly fechaEmision { get; set; }

        public Empleados()
        {
            
        }
    }
}
