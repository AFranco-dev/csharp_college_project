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
        public DateTime fechaContrato { get; set; }
        public DateTime fechaRenuncia { get; set; }
        public DateTime fechaEmision { get; set; }
        public int mesesTrabajo { get; set; }

        public Empleados()
        {
            
        }
    }
}
