using Microsoft.AspNetCore.Mvc.Rendering;
using UITraining.Interfaces;
using UITraining.Models;
using UITraining.Models.DB;
using UITraining.Models.DTO;
using static UITraining.Models.GeneralStatus;

namespace UITraining.Services
{
    public class SupplierServices : ISupplier
    {
        private readonly ApplicationContext _context;

        public SupplierServices(ApplicationContext context)
        {
            _context = context;
        }

        public List<SelectListItem> Suppliers()
        {
            var datas = _context.Suppliers
                .Where(x => x.StatusSupplier != GeneralStatusData.Suspended)
                .Select(x => new SelectListItem
                {
                    Text = x.NameSupplier,
                    Value = x.id.ToString()
                }).ToList();
            return datas;
        }

        public Supplier GetSupplierById(int id)
        {
            var supplier = _context.Suppliers
                .Where(x => x.id == id && x.StatusSupplier != GeneralStatusData.Suspended)
                .FirstOrDefault();

            if(supplier == null)
            {
                return new Supplier();
            }
            return supplier;
        }

        public List<SupplierDTO> GetAllSupplier()
        {
            var suppliers = _context.Suppliers.Where(x => x.StatusSupplier != GeneralStatusData.Suspended)
                .Select(x => new SupplierDTO
                {
                    id = x.id,
                    NameSupplier = x.NameSupplier,
                    SupplierAddress = x.SupplierAddress,
                    StatusSupplier = x.StatusSupplier
                }).ToList();
            return suppliers;
        }

        public bool AddSupplier(SupplierDTO supplier)
        {
            try
            {

                var insertSupplier = new Supplier
                {
                    NameSupplier = supplier.NameSupplier,
                    SupplierAddress = supplier.SupplierAddress,
                    StatusSupplier = supplier.StatusSupplier
                };
                _context.Suppliers.Add(insertSupplier);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateSupplier(SupplierDTO supplier)
        {
            try
            {
                var dataSupplier = _context.Suppliers.FirstOrDefault(x => x.id == supplier.id);
                if (dataSupplier != null)
                {
                    dataSupplier.NameSupplier = supplier.NameSupplier;
                    dataSupplier.SupplierAddress = supplier.SupplierAddress;
                    dataSupplier.StatusSupplier = supplier.StatusSupplier;

                    _context.Suppliers.Update(dataSupplier);
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

        public bool SoftDeleteSupplier(int id)
        {
            try
            {
                var dataSupplier = _context.Suppliers.FirstOrDefault(DEL => DEL.id == id);
                if(dataSupplier != null)
                {
                    dataSupplier.StatusSupplier = GeneralStatusData.Suspended;
                    _context.Suppliers.Update(dataSupplier);
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