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
    }
}
