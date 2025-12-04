using AutoMapper;
using Formation_Ecommerce_11_2025.Application.Categories.Dtos;
using Formation_Ecommerce_11_2025.Application.Categories.Interfaces;
using Formation_Ecommerce_11_2025.Models.Category;
using Microsoft.AspNetCore.Mvc;

namespace Formation_Ecommerce_11_2025.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }
        public async Task<IActionResult> CategoryIndex()
        {
            IEnumerable<CategoryDto> categories = await _categoryService.ReadAllAsync();
            IEnumerable<CategoryViewModel> viewModels = _mapper.Map<IEnumerable<CategoryViewModel>>(categories);
            return View(viewModels);
        }

        public IActionResult CreateCategory()
        {
            return View(new CreateCategoryViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryViewModel createCategoryViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(createCategoryViewModel);
            }

            try
            {
                var createCategoryDto = _mapper.Map<CreateCategoryDto>(createCategoryViewModel);
                var result = await _categoryService.AddAsync(createCategoryDto);

                if (result != null)
                {
                    TempData["success"] = "Category created successfully!";
                    return RedirectToAction(nameof(CategoryIndex));
                }
                return View(createCategoryViewModel);
            }
            catch (Exception ex)
            {
                // Consider logging the exception
                ModelState.AddModelError("", "An error occurred while creating the category.");
                TempData["error"] = "Failed to create category.";
                return View(createCategoryViewModel);
            }
        }
    }
}
