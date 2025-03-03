using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using UITraining.Interfaces;
using UITraining.Models;
using UITraining.Models.DB;
using UITraining.Models.DTO;
using static UITraining.Models.GeneralStatus;

namespace UITraining.Services
{
    public class ProductServices : IProduct
    {
        private readonly ApplicationContext _context;

        public ProductServices(ApplicationContext context)
        {
            _context = context;
        }

        public List<ProductDTO> GetAllProducts()
        {
            var products = _context.Products
                .Include(y => y.Supplier)
                .Where(x => x.ProductStatus != GeneralStatusData.deleted).Select(x => new ProductDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    Stoct = x.Stoct,
                    ProductStatus = x.ProductStatus,
                    SupplierName = x.Supplier.NameSupplier
                }).ToList();
            return products;
        }

        public Product GetProductById(int id)
        {
            var product = _context.Products.Where(x => x.Id == id && x.ProductStatus != GeneralStatusData.deleted).FirstOrDefault();

            if (product == null)
            {
                return new Product();
            }

            return product;
        }

        public bool UpdateProduct(ProductDTO product)
        {
            var data = _context.Products.FirstOrDefault(x => x.Id == product.Id);
            if (data == null)
            {
                return false;
            }

            data.Name = product.Name;
            data.Description = product.Description;
            data.Stoct = product.Stoct;
            data.Price = product.Price;
            data.ProductStatus = product.ProductStatus;

            _context.Products.Update(data);
            _context.SaveChanges();

            return true;
        }

        public bool AddProduct(ProductDTO product)
        {
            var datas = new Product();
            datas.Name = product.Name;
            datas.Description = product.Description;
            datas.Stoct = product.Stoct;
            datas.Price = product.Price;
            datas.ProductStatus = product.ProductStatus;
            datas.IdSupplier = product.IdSupplier;

            _context.Products.Add(datas);
            _context.SaveChanges();
            return true;
        }

        public bool SoftDelete(int id)
        {
            try
            {
                var dataProduct = _context.Products.FirstOrDefault(Del => Del.Id == id);
                if (dataProduct != null)
                {
                    dataProduct.ProductStatus = GeneralStatusData.deleted;
                    _context.Products.Update(dataProduct);
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}