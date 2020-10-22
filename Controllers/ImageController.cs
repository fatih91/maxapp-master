using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using maxapp.Controllers.Resources;
using maxapp.Core.Interfaces;
using maxapp.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace maxapp.Controllers
{
    [Route("/api/symptoms/{symptomId}/images")]
    [AllowAnonymous]
    public class ImageController : Controller
    {
        private readonly IHostingEnvironment host;
        private readonly ISymptomRepository symptomRepository;
        private readonly IMapper mapper;
        private readonly ImageSettings imageSettings;
        private readonly IImageRepository imageRepository;

        public ImageController(IHostingEnvironment host, IMapper mapper, ISymptomRepository symptomRepository,
            IOptionsSnapshot<ImageSettings> options, IImageRepository imageRepository)
        {
            this.host = host;
            this.symptomRepository = symptomRepository;
            this.mapper = mapper;
            this.imageSettings = options.Value;
            this.imageRepository = imageRepository;
        }



        [HttpPost]
        public async Task<IActionResult> Upload(int symptomId, IFormFile file)
        {
            var symptom = await symptomRepository.GetSymptom(symptomId, includeRelated: false);
            if (symptom == null)
                return NotFound();

            if (file == null) return BadRequest("Null file");
            if (file.Length == 0) return BadRequest("Empfy file");
            if (file.Length > imageSettings.MaxBytes) return BadRequest("Max file size exceeded");
            if (imageSettings.IsNotSupported(file.FileName))
                return BadRequest("Invalid file type");

            var uploadsFolderPath = Path.Combine(host.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsFolderPath))
                Directory.CreateDirectory(uploadsFolderPath);

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var image = new Image
            {
                FileName = fileName
            };
            symptom.Images.Add(image);
            await symptomRepository.CompleteAsync();

            return Ok(mapper.Map<Image, ImageResource>(image));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateImage(string id, [FromBody] SaveImageResource imageResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var image = await imageRepository.GetImage(id);

            if (image == null)
                return NotFound();

            mapper.Map<SaveImageResource, Image>(imageResource, image); //ToDo: Mapping Profile für SaveImageResource in Image

            await imageRepository.CompleteAsync();

            image = await imageRepository.GetImage(image.FileName);

            var result = mapper.Map<Image, ImageResource>(image); //ToDo: Mapping Profile für Image in Image Resource
            return Ok(result);
        }

        [HttpGet]
        public async Task<IEnumerable<SaveImageResource>> GetImages(int symptomId)
        {
            var images = await imageRepository.GetImages(symptomId);
            
            return mapper.Map<IEnumerable<Image>, IEnumerable<SaveImageResource>>(images);
        }
        
        [HttpDelete("{fileName}")]
        public async Task<IActionResult> DeleteImage(string fileName)
        {
            var image = await imageRepository.GetImage(fileName);
            
            if(image == null)
                return NotFound();
            
            imageRepository.Remove(image);
            await imageRepository.CompleteAsync();
            return Ok();
        }
    }
}