using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace Proyecto_comu.Views.Home
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmotionalTrackerController : ControllerBase
    {
        [HttpPost("save")]
        public IActionResult SaveEmotionalData([FromBody] Dictionary<string, string> responses)
        {
            try
            {
                // Crear un elemento de registro con la fecha y las respuestas
                XElement record = new XElement("Record",
                    new XElement("Date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                );

                foreach (var response in responses)
                {
                    record.Add(new XElement(response.Key, response.Value));
                }

                // Ruta del archivo XML donde se guardarán los datos
                string filePath = "emotional_records.xml";

                XElement root;
                if (System.IO.File.Exists(filePath))
                {
                    root = XElement.Load(filePath);
                }
                else
                {
                    root = new XElement("EmotionalRecords");
                }

                root.Add(record);
                root.Save(filePath);

                return Ok(new { message = "Datos guardados correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error al guardar los datos", error = ex.Message });
            }
        }
    }
}

