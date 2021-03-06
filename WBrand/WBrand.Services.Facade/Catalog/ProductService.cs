using AutoMapper;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WBrand.Common.Page;
using WBrand.Core.Domain.Entities.Catalog;
using WBrand.Core.Domain.Models.Catalog;
using WBrand.Data;
using WBrand.Data.Catalog;
using WBrand.Services.Catalog;
using AutoMapper.QueryableExtensions;
using WBrand.Common.Helper;
using WBrand.Common.AutoMapper;

namespace WBrand.Services.Facade.Catalog
{
    public class ProductService : BaseServices<Product>, IProductService
    {
        IProductRepository _productRepo;
        ICatalogCategoryRepository _catalogCategoryRepository;
        IProductCategoryRepository _productCategoryRepo;
        public ProductService(IProductRepository productRepo,
            IProductCategoryRepository productCategoryRepo,
            ICatalogCategoryRepository catalogCategoryRepository) : base(productRepo)
        {
            _productRepo = productRepo;
            _productCategoryRepo = productCategoryRepo;
            _catalogCategoryRepository = catalogCategoryRepository;
        }

        public void DeleteById(long id)
        {
            var entity = _productRepo.GetById(id);
            entity.IsDel = true;
            _productRepo.Update(entity);
        }

        public PaginationSet<ProductModel> GetAll(int pageIndex, int pageSize, string filter = "", int categoryId = 0, string category = "", bool? isPublish = null)
        {
            var query = _productRepo.TableNoTracking.Where(x => !x.IsDel);

            if (!string.IsNullOrWhiteSpace(filter))
                query = query.Where(x => x.Name.Contains(filter));
            if (categoryId != 0)
                query = from q in query
                        join c in _productCategoryRepo.TableNoTracking.Where(x => x.CategoryId == categoryId)
                        on q.Id equals c.ProductId
                        select q;
            if (!string.IsNullOrWhiteSpace(category))
                query = from q in query
                        join c in _productCategoryRepo.TableNoTracking
                        on q.Id equals c.ProductId
                        join cat in _catalogCategoryRepository.TableNoTracking.Where(x => x.Alias == category)
                        on c.CategoryId equals cat.Id
                        select q;


            if (isPublish != null)
                query = query.Where(x => x.IsPublish == isPublish.Value);

            var result = query.OrderBy(x => x.Name).ToPagedList(pageIndex, pageSize);

            return new PaginationSet<ProductModel>
            {
                Items = Mapper.Map<IEnumerable<ProductModel>>(result.ToList()),
                Page = pageIndex - 1,
                PageSize = pageSize,
                TotalCount = result.TotalItemCount,
                TotalPages = result.PageCount
            };

        }

        public ProductModel GetByAlias(string alias)
        {
            return _productRepo.TableNoTracking.Where(x => x.IsDel == false && x.Alias == alias && x.IsPublish == true).QueryTo<ProductModel>().FirstOrDefault();
        }

        public ProductModel GetById(long id)
        {
            var entity = _productRepo.GetById(id);
            var model = Mapper.Map<ProductModel>(entity);
            model.ProductCategories = Mapper.Map<IEnumerable<ProductCategoryModel>>(_productCategoryRepo.TableNoTracking.Where(x => x.ProductId == model.Id));
            model.CategoryIds = model.ProductCategories.Select(x => x.CategoryId).ToList();
            return model;
        }

        public IEnumerable<ProductModel> GetRandom(int amountItem)
        {
            return _productRepo.TableNoTracking.Where(x => x.IsDel == false && x.IsPublish == true).QueryTo<ProductModel>().OrderBy(o => Guid.NewGuid()).Take(amountItem).ToList();
        }

        public IEnumerable<Product> GetTop6()
        {
            return _productRepo.TableNoTracking.Where(x => x.IsDel == false && x.IsPublish == true && x.IsHome == true).OrderBy(x => x.Name).Take(6).ToList();
        }

        public CreateProductModel Insert(CreateProductModel model)
        {
            try
            {
                var newProduct = Mapper.Map<Product>(model.Product);
                newProduct.Name = newProduct.Name.Trim();
                newProduct.CreatedDate = CoreHelper.SystemTimeNow;
                newProduct.Alias = StringHelper.ToUrlFriendlyWithDate(newProduct.Name, newProduct.CreatedDate.Value.DateTime);
                _productRepo.BeginTran();
                _productRepo.Insert(newProduct);
                if (model.CategoryIds.Count > 0)
                {
                    var newProductCat = CreateListCat(model.CategoryIds, newProduct.Id);
                    _productCategoryRepo.Insert(newProductCat);
                }

                _productRepo.CommitTran();

                return model;

            }
            catch
            {
                _productRepo.RollbackTran();
                throw;
            }
        }

        public UpdateProductModel Update(UpdateProductModel model)
        {
            try
            {
                var updateProduct = Mapper.Map<Product>(model.Product);
                updateProduct.UpdatedDate = CoreHelper.SystemTimeNow;
                updateProduct.Name = updateProduct.Name.Trim();
                updateProduct.Alias = StringHelper.ToUrlFriendlyWithDate(updateProduct.Name, updateProduct.CreatedDate.Value.DateTime);
                _productRepo.BeginTran();
                _productRepo.Update(updateProduct);

                var entityProductCat = _productCategoryRepo.Table.Where(x => x.ProductId == model.Product.Id).ToList();

                _productCategoryRepo.Delete(entityProductCat);
                if (model.CategoryIds != null && model.CategoryIds.Count > 0)
                {
                    var newProductCat = CreateListCat(model.CategoryIds, updateProduct.Id);
                    _productCategoryRepo.Insert(newProductCat);
                }
                _productRepo.CommitTran();

                return model;

            }
            catch
            {
                _productRepo.RollbackTran();
                throw;
            }
        }

        private IEnumerable<ProductCategory> CreateListCat(List<int> ids, long productId)
        {
            if (ids.Count < 1)
                return null;
            var listProductCat = new List<ProductCategory>();
            foreach (var item in ids)
            {
                listProductCat.Add(new ProductCategory
                {
                    CategoryId = item,
                    ProductId = productId
                });
            }
            return listProductCat;
        }
    }
}
