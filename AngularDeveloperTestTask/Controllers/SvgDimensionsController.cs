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
        //qekjo punon mire 
        //[HttpPut]
        //public async Task<IActionResult> UpdateDimensions([FromBody] SvgDimensionsModel model)
        //{
        //    try
        //    {
        //        var jsonFilePath = Path.Combine(_webHostEnvironment.WebRootPath, "svgDimensions.json");

        //        // Serialize the model to JSON
        //        var jsonContent = JsonSerializer.Serialize(model);

        //        // Write the JSON content to the file
        //        await System.IO.File.WriteAllTextAsync(jsonFilePath, jsonContent, Encoding.UTF8);

        //        return Ok("Dimensions updated successfully");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, "An error occurred while updating dimensions");
        //    }
        //}

        //qekjo e ka ni prb cannot convert json to string 
        //[HttpPut]
        //public async Task<IActionResult> UpdateDimensions([FromBody] string jsonContent)
        //{
        //    try
        //    {
        //        var jsonFilePath = Path.Combine(_webHostEnvironment.WebRootPath, "svgDimensions.json");

        //        // Write the JSON content to the file
        //        await System.IO.File.WriteAllTextAsync(jsonFilePath, jsonContent, Encoding.UTF8);

        //        return Ok("Dimensions updated successfully");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, "An error occurred while updating dimensions");
        //    }
        //}

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


        //[HttpPut]
        //public async Task<IActionResult> UpdateDimensions(JsonDocument updatedDimensions)
        //{
        //    using (var streamWriter = new StreamWriter(_jsonFilePath))
        //    {
        //        await JsonSerializer.SerializeAsync(streamWriter.BaseStream, updatedDimensions);
        //        return Ok();
        //    }
        //}
    }
    //public class SvgDimensionsModel
    //{
    //    public int Width { get; set; }
    //    public int Height { get; set; }
    //}
}

