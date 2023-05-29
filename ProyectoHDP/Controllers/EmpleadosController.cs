using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoHDP.Data;
using ProyectoHDP.Models;
using iText.Html2pdf;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System.IO;
using System.Web;
using iText.Kernel.Geom;
//using IronPdf;

namespace ProyectoHDP.Controllers
{
    public class EmpleadosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmpleadosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Empleados
        public async Task<IActionResult> Index()
        {
              return _context.Empleados != null ? 
                          View(await _context.Empleados.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Empleados'  is null.");
        }
        //--------------------------LO QUE YO HE HECHO----------------------------------------------
        //--------------------------LO QUE YO HE HECHO----------------------------------------------
        //--------------------------LO QUE YO HE HECHO----------------------------------------------
        // GET: Empleados/ShowBuscarYearForm
        public async Task<IActionResult> ShowBuscarYearForm()
        {
            return View();
        }
        public async Task<IActionResult> ShowBuscarYearResults(DateTime SearchYear)
        {
            return _context.Empleados != null ?
                          View("Index", await _context.Empleados.Where(x => x.fechaEmision.Year == SearchYear.Year).ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Empleados'  is null.");
        }
        // GET: Empleados/ShowBuscarYearForm
        public async Task<IActionResult> ShowBuscarEmpresaForm()
        {
            return View();
        }
        public async Task<IActionResult> ShowBuscarEmpresaResults(string SearchEmpresa)
        {
            return _context.Empleados != null ?
                          View("Index", await _context.Empleados.Where(x => x.empresa.Contains(SearchEmpresa)).ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Empleados'  is null.");
        }

        public IActionResult DownloadTextFile(int id)
        {
            var empleados = _context.Empleados.FirstOrDefault(m => m.iD == id);
            if (empleados == null)
            {
                return NotFound();
            }

            // Generate the text content for the file using the model information
            string fileContent = $"ID: {empleados.iD}\n" +
                                 $"Nombre: {empleados.nombre}\n" +
                                 $"Tipo de Contrato: {empleados.tipoContrato}\n" +
                                 $"País: {empleados.pais}\n" +
                                 $"Empresa: {empleados.empresa}\n" +
                                 $"Salario: {empleados.salario}\n" +
                                 $"Fecha de Contrato: {empleados.fechaContrato}\n" +
                                 $"Fecha de Renuncia: {empleados.fechaRenuncia}\n" +
                                 $"Fecha de Emisión: {empleados.fechaEmision}\n" +
                                 $"Meses de Trabajo: {empleados.mesesTrabajo}";

            byte[] fileBytes = Encoding.UTF8.GetBytes(fileContent);
            string fileName = $"Empleado_{empleados.iD}.txt";

            // Return the file as a download response
            return File(fileBytes, "text/plain", fileName);
        }

        public IActionResult DownloadAllUsersTextFile()
        {
            var empleadosList = _context.Empleados.ToList();
            if (empleadosList.Count == 0)
            {
                return NotFound();
            }

            // Generate the text content for the file
            var fileContent = new StringBuilder();
            foreach (var empleado in empleadosList)
            {
                fileContent.AppendLine($"ID: {empleado.iD}");
                fileContent.AppendLine($"Nombre: {empleado.nombre}");
                fileContent.AppendLine($"Tipo de Contrato: {empleado.tipoContrato}");
                fileContent.AppendLine($"País: {empleado.pais}");
                fileContent.AppendLine($"Empresa: {empleado.empresa}");
                fileContent.AppendLine($"Salario: {empleado.salario}");
                fileContent.AppendLine($"Fecha de Contrato: {empleado.fechaContrato}");
                fileContent.AppendLine($"Fecha de Renuncia: {empleado.fechaRenuncia}");
                fileContent.AppendLine($"Fecha de Emisión: {empleado.fechaEmision}");
                fileContent.AppendLine($"Meses de Trabajo: {empleado.mesesTrabajo}");
                fileContent.AppendLine();
            }

            byte[] fileBytes = Encoding.UTF8.GetBytes(fileContent.ToString());
            string fileName = "AllUsers.txt";

            // Return the file as a download response
            return File(fileBytes, "text/plain", fileName);
        }

        public IActionResult DownloadAllUsersByEmpresaTextFile(string empresa)
        {
            var query = _context.Empleados.AsQueryable();

            if (!string.IsNullOrEmpty(empresa))
            {
                query = query.Where(x => x.empresa.Contains(empresa));
            }

            var empleadosList = query.ToList();

            if (empleadosList.Count == 0)
            {
                return NotFound();
            }

            // Generate the text content for the file
            var fileContent = new StringBuilder();
            foreach (var empleado in empleadosList)
            {
                fileContent.AppendLine($"ID: {empleado.iD}");
                fileContent.AppendLine($"Nombre: {empleado.nombre}");
                fileContent.AppendLine($"Tipo de Contrato: {empleado.tipoContrato}");
                fileContent.AppendLine($"País: {empleado.pais}");
                fileContent.AppendLine($"Empresa: {empleado.empresa}");
                fileContent.AppendLine($"Salario: {empleado.salario}");
                fileContent.AppendLine($"Fecha de Contrato: {empleado.fechaContrato}");
                fileContent.AppendLine($"Fecha de Renuncia: {empleado.fechaRenuncia}");
                fileContent.AppendLine($"Fecha de Emisión: {empleado.fechaEmision}");
                fileContent.AppendLine($"Meses de Trabajo: {empleado.mesesTrabajo}");
                fileContent.AppendLine();
            }

            byte[] fileBytes = Encoding.UTF8.GetBytes(fileContent.ToString());
            string fileName = "AllUsers.txt";

            // Return the file as a download response
            return File(fileBytes, "text/plain", fileName);
        }

        public IActionResult DownloadAllUsersByYearTextFile(DateTime SearchYear)
        {
            var query = _context.Empleados.AsQueryable();

            if (SearchYear != DateTime.MinValue)
            {
                query = query.Where(x => x.fechaEmision.Year == SearchYear.Year);
            }

            var empleadosList = query.ToList();

            if (empleadosList.Count == 0)
            {
                return NotFound();
            }

            // Generate the text content for the file
            var fileContent = new StringBuilder();
            foreach (var empleado in empleadosList)
            {
                fileContent.AppendLine($"ID: {empleado.iD}");
                fileContent.AppendLine($"Nombre: {empleado.nombre}");
                fileContent.AppendLine($"Tipo de Contrato: {empleado.tipoContrato}");
                fileContent.AppendLine($"País: {empleado.pais}");
                fileContent.AppendLine($"Empresa: {empleado.empresa}");
                fileContent.AppendLine($"Salario: {empleado.salario}");
                fileContent.AppendLine($"Fecha de Contrato: {empleado.fechaContrato}");
                fileContent.AppendLine($"Fecha de Renuncia: {empleado.fechaRenuncia}");
                fileContent.AppendLine($"Fecha de Emisión: {empleado.fechaEmision}");
                fileContent.AppendLine($"Meses de Trabajo: {empleado.mesesTrabajo}");
                fileContent.AppendLine();
            }

            byte[] fileBytes = Encoding.UTF8.GetBytes(fileContent.ToString());
            string fileName = "AllUsers.txt";

            // Return the file as a download response
            return File(fileBytes, "text/plain", fileName);
        }


        public IActionResult GeneratePDF()
        {
            // Define your custom HTML string
            string htmlString = "<html><body><h1>Hello, PDF!</h1></body></html>";

            // Create a memory stream to hold the PDF document
            MemoryStream stream = new MemoryStream();

            // Create an iText7 PdfWriter
            PdfWriter writer = new PdfWriter(stream);
            iText.Kernel.Pdf.PdfDocument pdf = new iText.Kernel.Pdf.PdfDocument(writer);

            // Create an iText7 ConverterProperties object
            ConverterProperties properties = new ConverterProperties();

            // Create an iText7 Document
            Document document = new Document(pdf);

            // Convert the HTML string to PDF and save it to the document
            HtmlConverter.ConvertToPdf(htmlString, pdf, properties);

            // Close the document and writer, which will flush the content to the stream
            document.Close();
            writer.Close();

            // Create a new memory stream from the existing stream
            MemoryStream newStream = new MemoryStream(stream.ToArray());

            // Return the PDF file as a download
            return File(newStream, "application/pdf", "custom.pdf");
        }

        //public IActionResult GeneratePDF()
        //{
        //    // Define your custom HTML string
        //    string htmlString = "<html><body><h1>Hello, PDF!</h1></body></html>";

        //    // Create a memory stream to hold the PDF document
        //    MemoryStream stream = new MemoryStream();

        //    // Create an iText7 PdfWriter
        //    PdfWriter writer = new PdfWriter(stream);
        //    iText.Kernel.Pdf.PdfDocument pdf = new iText.Kernel.Pdf.PdfDocument(writer);

        //    // Create an iText7 ConverterProperties object
        //    ConverterProperties properties = new ConverterProperties();

        //    // Create an iText7 Document
        //    Document document = new Document(pdf);

        //    // Convert the HTML string to PDF and save it to the document
        //    HtmlConverter.ConvertToPdf(htmlString, pdf, properties);

        //    // Close the document and writer, which will flush the content to the stream
        //    document.Close();
        //    writer.Close();


        //    // Return the PDF file as a download
        //    return File(stream, "application/pdf", "custom.pdf");
        //}



        //        public ActionResult GeneratePDF()
        //{
        //    // Replace with the actual content from your view
        //    string content = "<div>Your content goes here</div>";

        //            // Create a memory stream to hold the PDF document
        //            MemoryStream stream = new MemoryStream();

        //        // Create an iText7 PdfWriter
        //        PdfWriter writer = new PdfWriter(stream);
        //        PdfDocument pdf = new PdfDocument(writer);

        //        // Create an iText7 Document
        //        Document document = new Document(pdf, PageSize.LETTER);

        //            // Convert HTML content to PDF
        //            HtmlConverter.ConvertToPdf(content, pdf);

        //            // Close the document and writer
        //            document.Close();
        //        writer.Close();

        //        // Save the PDF document to a byte array
        //        byte[] pdfBytes = stream.ToArray();

        //        // Return the PDF file as a download
        //        return File(pdfBytes, "application/pdf", "document.pdf");

        //}


        //--------------------------LO QUE YO HE HECHO----------------------------------------------
        //--------------------------LO QUE YO HE HECHO----------------------------------------------
        //--------------------------LO QUE YO HE HECHO----------------------------------------------
        // GET: Empleados/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Empleados == null)
            {
                return NotFound();
            }

            var empleados = await _context.Empleados
                .FirstOrDefaultAsync(m => m.iD == id);
            if (empleados == null)
            {
                return NotFound();
            }

            return View(empleados);
        }

        // GET: Empleados/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Empleados/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("iD,nombre,tipoContrato,pais,empresa,salario,fechaContrato,fechaRenuncia,fechaEmision,mesesTrabajo")] Empleados empleados)
        {
            if (ModelState.IsValid)
            {
                _context.Add(empleados);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(empleados);
        }

        // GET: Empleados/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Empleados == null)
            {
                return NotFound();
            }

            var empleados = await _context.Empleados.FindAsync(id);
            if (empleados == null)
            {
                return NotFound();
            }
            return View(empleados);
        }

        // POST: Empleados/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("iD,nombre,tipoContrato,pais,empresa,salario,fechaContrato,fechaRenuncia,fechaEmision,mesesTrabajo")] Empleados empleados)
        {
            if (id != empleados.iD)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(empleados);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpleadosExists(empleados.iD))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(empleados);
        }

        // GET: Empleados/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Empleados == null)
            {
                return NotFound();
            }

            var empleados = await _context.Empleados
                .FirstOrDefaultAsync(m => m.iD == id);
            if (empleados == null)
            {
                return NotFound();
            }

            return View(empleados);
        }

        // POST: Empleados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Empleados == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Empleados'  is null.");
            }
            var empleados = await _context.Empleados.FindAsync(id);
            if (empleados != null)
            {
                _context.Empleados.Remove(empleados);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmpleadosExists(int id)
        {
          return (_context.Empleados?.Any(e => e.iD == id)).GetValueOrDefault();
        }
    }
}
