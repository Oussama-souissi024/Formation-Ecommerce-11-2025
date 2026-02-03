using AutoMapper;
using Formation_Ecommerce_11_2025.Application.Products.Dtos;
using Formation_Ecommerce_11_2025.Application.Products.Mapping;
using Formation_Ecommerce_11_2025.Application.Products.Services;
using Formation_Ecommerce_11_2025.Core.Entities.Category;
using Formation_Ecommerce_11_2025.Core.Entities.Product;
using Formation_Ecommerce_11_2025.Test.Fakes;
using Microsoft.AspNetCore.Http;

namespace Formation_Ecommerce_11_2025.Test.Unit;

/// <summary>
/// Tests unitaires de <c>ProductServices</c> (CRUD + gestion d'image via <c>IFileHelper</c>).
/// </summary>
public class ProductServicesTests
{
    private static IMapper CreateMapper()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<ProductProfile>());
        return config.CreateMapper();
    }

    [Fact]
    public async Task AddAsync_without_image_does_not_upload_file()
    {
        var productRepo = new FakeProductRepository();
        var fileHelper = new FakeFileHelper();
        var categoryService = new FakeCategoryService();
        var service = new ProductServices(productRepo, CreateMapper(), categoryService, fileHelper);

        var dto = await service.AddAsync(new CreateProductDto
        {
            Name = "Prod",
            Price = 10m,
            Description = "Desc",
            CategoryID = Guid.NewGuid(),
            ImageFile = null
        });

        Assert.NotNull(dto);
        Assert.Equal(0, fileHelper.UploadCallCount);
        Assert.Null(dto!.ImageUrl);
    }

    [Fact]
    public async Task AddAsync_with_image_uploads_file_and_sets_image_url()
    {
        var productRepo = new FakeProductRepository();
        var fileHelper = new FakeFileHelper { UploadReturnValue = "new.png" };
        var categoryService = new FakeCategoryService();
        var service = new ProductServices(productRepo, CreateMapper(), categoryService, fileHelper);

        var file = new FormFile(new MemoryStream(new byte[] { 1, 2, 3 }), 0, 3, "ImageFile", "img.png");

        var dto = await service.AddAsync(new CreateProductDto
        {
            Name = "Prod",
            Price = 10m,
            Description = "Desc",
            CategoryID = Guid.NewGuid(),
            ImageFile = file
        });

        Assert.NotNull(dto);
        Assert.Equal(1, fileHelper.UploadCallCount);
        Assert.Equal("new.png", dto!.ImageUrl);
    }

    [Fact]
    public async Task ReadByIdAsync_when_not_found_throws_key_not_found()
    {
        var productRepo = new FakeProductRepository();
        var fileHelper = new FakeFileHelper();
        var categoryService = new FakeCategoryService();
        var service = new ProductServices(productRepo, CreateMapper(), categoryService, fileHelper);

        await Assert.ThrowsAsync<KeyNotFoundException>(() => service.ReadByIdAsync(Guid.NewGuid()));
    }

    [Fact]
    public async Task ReadProductsByCategoryName_when_category_not_found_throws_key_not_found()
    {
        var productRepo = new FakeProductRepository();
        var fileHelper = new FakeFileHelper();
        var categoryService = new FakeCategoryService { CategoryIdByName = null };
        var service = new ProductServices(productRepo, CreateMapper(), categoryService, fileHelper);

        await Assert.ThrowsAsync<KeyNotFoundException>(() => service.ReadProductsByCategoryName("missing"));
    }

    [Fact]
    public async Task DeleteAsync_when_product_has_image_deletes_file()
    {
        var productRepo = new FakeProductRepository();
        var fileHelper = new FakeFileHelper();
        var categoryService = new FakeCategoryService();
        var service = new ProductServices(productRepo, CreateMapper(), categoryService, fileHelper);

        var category = new Category
        {
            Id = Guid.NewGuid(),
            Name = "Cat",
            Description = "Desc",
            Products = new List<Product>()
        };

        var product = await productRepo.AddAsync(new Product
        {
            Id = Guid.NewGuid(),
            Name = "Prod",
            Description = "Desc",
            Price = 10m,
            ImageUrl = "old.png",
            CategoryId = category.Id,
            Category = category,
            CartDetails = new List<Formation_Ecommerce_11_2025.Core.Entities.Cart.CartDetails>()
        });

        await service.DeleteAsync(product.Id);

        Assert.Equal(1, fileHelper.DeleteCallCount);
    }

    [Fact]
    public async Task UpdateAsync_with_new_image_uploads_and_deletes_old_image()
    {
        var productRepo = new FakeProductRepository();
        var fileHelper = new FakeFileHelper { UploadReturnValue = "new.png" };
        var categoryService = new FakeCategoryService();
        var service = new ProductServices(productRepo, CreateMapper(), categoryService, fileHelper);

        var category = new Category
        {
            Id = Guid.NewGuid(),
            Name = "Cat",
            Description = "Desc",
            Products = new List<Product>()
        };

        var existing = await productRepo.AddAsync(new Product
        {
            Id = Guid.NewGuid(),
            Name = "Old",
            Description = "Desc",
            Price = 10m,
            ImageUrl = "old.png",
            CategoryId = category.Id,
            Category = category,
            CartDetails = new List<Formation_Ecommerce_11_2025.Core.Entities.Cart.CartDetails>()
        });

        var file = new FormFile(new MemoryStream(new byte[] { 1 }), 0, 1, "ImageFile", "img.png");

        await service.UpdateAsync(new UpdateProductDto
        {
            Id = existing.Id,
            Name = "New",
            Price = 20m,
            Description = "Desc",
            CategoryId = category.Id,
            ImageFile = file
        });

        Assert.Equal(1, fileHelper.UploadCallCount);
        Assert.Equal(1, fileHelper.DeleteCallCount);
        Assert.Equal("new.png", productRepo.LastUpdatedProduct?.ImageUrl);
    }

    [Fact]
    public async Task UpdateAsync_when_product_not_found_throws_wrapped_exception()
    {
        var productRepo = new FakeProductRepository();
        var fileHelper = new FakeFileHelper();
        var categoryService = new FakeCategoryService();
        var service = new ProductServices(productRepo, CreateMapper(), categoryService, fileHelper);

        var ex = await Assert.ThrowsAsync<Exception>(() => service.UpdateAsync(new UpdateProductDto
        {
            Id = Guid.NewGuid(),
            Name = "Name",
            Price = 10m,
            CategoryId = Guid.NewGuid()
        }));

        Assert.Contains("Erreur lors de la mise à jour du produit", ex.Message);
    }
}
