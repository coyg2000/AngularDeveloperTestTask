using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AngularDeveloperTestTask.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SvgDimensionsController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SvgDimensionsController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult GetDimensions()
        {
            var jsonFilePath = Path.Combine(_webHostEnvironment.WebRootPath, "svgDimensions.json");
            var jsonContent = System.IO.File.ReadAllText(jsonFilePath);
            return Ok(jsonContent);
        }


        [HttpPut]
        public async Task<IActionResult> UpdateDimensions([FromBody] JsonElement jsonContent)
        {
            try
            {
                var jsonFilePath = Path.Combine(_webHostEnvironment.WebRootPath, "svgDimensions.json");

                // Convert the JsonElement  back to a JSON string
                var jsonString = jsonContent.ToString();

                // Write the JSON content to the file
                await System.IO.File.WriteAllTextAsync(jsonFilePath, jsonString, Encoding.UTF8);

                return Ok("Dimensions updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating dimensions");
            }
        }


    }
}

