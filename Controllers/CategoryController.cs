using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AutoMapper;
using maxapp.Controllers.Resources;
using maxapp.Core.Interfaces;
using maxapp.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace maxapp.Controllers
{
    [AllowAnonymous]
    [Route("/api/categories")]
    public class CategoryController : Controller
    {
        private readonly IMapper mapper;
        private readonly ICategoryRepository categoryRepository;
        private readonly ISymptomRepository symptomRepository;
        private readonly IUnitOfWork unitOfWork;

        
        public CategoryController(IMapper mapper, ICategoryRepository categoryRepository, ISymptomRepository symptomRepository, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.categoryRepository = categoryRepository;
            this.symptomRepository = symptomRepository;
            this.unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] SaveCategoryResource categoryResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var symptoms = await symptomRepository.GetSymptoms(categoryResource.Symptoms);
            
            if (categoryResource.ParentId == 0) //Kategorie hat keine Parentkategorie
            {
                var category = mapper.Map<SaveCategoryResource, Category>(categoryResource);
                category.Symptoms = symptoms;
                categoryRepository.Add(category);
                await categoryRepository.CompleteAsync();
                
                // Validierung //ToDo: [Kategorie] Auslagern in eine Serviceklasse als Generic <T>
                category = await categoryRepository.GetCategory(category.CategoryId);
                var result = mapper.Map<Category, CategoryResource>(category);
                return Ok(result);
            } 
            else //Kategorie wird als Subkategorie von einer Parentkategorie erstellt
            {
                // Prüfen nach, ob es eine Kategorie für die ParentId gibt
                if (await categoryRepository.GetCategory(categoryResource.ParentId, includeRelated: false) == null)
                    return NotFound();
                
                var subcategory = mapper.Map<SaveCategoryResource, Subcategory>(categoryResource);
                subcategory.SubCategory.Symptoms = symptoms;
                categoryRepository.Add(subcategory);
                await categoryRepository.CompleteAsync();
                
                //Validierung
                var category = await categoryRepository.GetCategory(subcategory.CategoryId);
                var result = mapper.Map<Category, CategoryResource>(category);
                return Ok(result);
            }
            
        }
        
        [ProducesResponseType(typeof(CategoryResource),200)]
        [ProducesResponseType(302)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {

            var category = await categoryRepository.GetCategory(id);

            if (category == null)
                return NotFound();

            var categoryResource = mapper.Map<Category, CategoryResource>(category);

            return Ok(categoryResource);
        }
        
        [HttpGet("parentcategory/{id}")]
        public async Task<IActionResult> GetParentCategory(int id)
        {

            var category = await categoryRepository.GetParentCategory(id);

            if (category == null)
                return NotFound();

            var categoryResource = mapper.Map<Subcategory, CategoryResource>(category);

            return Ok(categoryResource);
        }

        [HttpGet("parentcategories/{id}")] //ToDo: Symptome der Kategorien auslassen
        public async Task<IActionResult> GetParentCategories(int id)
        {

            ICollection<ParentCategoryResource> parentCategoriesResource = new Collection<ParentCategoryResource>();
            var hasParent = true;
            var subCategoryId = id;

            while (hasParent)
            {
                var subCategory = await categoryRepository.GetParentCategory(subCategoryId);

                if (subCategory == null)
                {
                    hasParent = false;
                }
                else
                {
                    var parentCategoryResource = mapper.Map<Subcategory, ParentCategoryResource>(subCategory);
                    parentCategoriesResource.Add(parentCategoryResource);
                    subCategoryId = subCategory.CategoryId;
                }
            }

            return Ok(parentCategoriesResource);
        }


        [HttpGet("subcategories/{id}")]
        public async Task<IActionResult> GetSubcategories(int id)
        {

            var category = await categoryRepository.GetSubcategories(id);

            if (category == null)
                return NotFound();

            var categoryResource = mapper.Map<Category, CategoryResource>(category);

            return Ok(categoryResource);
        }
        
        [HttpGet]
        public async Task<IEnumerable<MapCategoryResource>> GetCategories()
        {

            var categories = await categoryRepository.GetCategories();
            
            return mapper.Map<IEnumerable<Category>, IEnumerable<MapCategoryResource>>(categories);
        }
        

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] SaveCategoryResource categoryResource)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var category = await categoryRepository.GetCategory(id, excludeSubcategories:true);
            if (category == null)
                return NotFound();
            mapper.Map<Category, SaveCategoryResource>(category, categoryResource);
            var symptoms = await symptomRepository.GetSymptoms(categoryResource.Symptoms);
            mapper.Map<ICollection<Symptom>, Category>(symptoms, category);
            await categoryRepository.CompleteAsync();
            
            // Oberste Kategorie
            if (categoryResource.ParentId == 0)
            {
                //Lösche Parentkategorie, falls eine da ist
                var parentCategory = await categoryRepository.GetParentCategory(id);
                if (parentCategory != null)
                {
                    categoryRepository.Remove(parentCategory);
                    await categoryRepository.CompleteAsync();
                }
                
                //Validierung
                category = await categoryRepository.GetCategory(category.CategoryId);
                var result = mapper.Map<Category, CategoryResource>(category);
                return Ok(result);
                
            }else // Kategorie die untegeordnet ist
            {
                // Kategorie hat eine Parentkategorie, aber soll geändert werden
                var parentCategory = await categoryRepository.GetParentCategory(id);
                if (parentCategory != null)
                {
                    //Lösche Parentkategorie
                    categoryRepository.Remove(parentCategory);
                    await categoryRepository.CompleteAsync(); 

                }
                
                    parentCategory = mapper.Map<Category, Subcategory>(category);
                    mapper.Map<SaveCategoryResource, Subcategory>(categoryResource, parentCategory);
                    categoryRepository.Add(parentCategory);
                    await categoryRepository.CompleteAsync();
                    
                    //Validierung
                    category = await categoryRepository.GetCategory(parentCategory.CategoryId);
                    var result = mapper.Map<Category, CategoryResource>(category);
                    return Ok(result);
            }
            
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await categoryRepository.GetCategory(id);
            
            if (category == null)
                return NotFound();
            
            categoryRepository.Remove(category);
            await categoryRepository.CompleteAsync();
            return Ok(id);

        }

    }
}