using OfficeSupply.Data.Entities;
using OfficeSupply.Data.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeSupply.Data.PropertyMapping
{
    public class PropertyMappingService : IPropertyMappingService
    {
        private Dictionary<string, PropertyMappingValue> propertyMapping1 =
            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
            {
                { "ID", new PropertyMappingValue(new List<string>(){"ID"}) },
                { "Name", new PropertyMappingValue(new List<string>(){"Name"}) },
            };

        private Dictionary<string, PropertyMappingValue> companyPropertyMapping =
            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
            {
                {"ID",new PropertyMappingValue(new List<string>(){"ID"}) },
                {"Name", new PropertyMappingValue(new List<string>(){"Name"}) },
                {"Wallet", new PropertyMappingValue(new List<string>(){"Wallet"}) }
            };

        private Dictionary<string, PropertyMappingValue> productPropertyMapping =
           new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
           {
                {"ID",new PropertyMappingValue(new List<string>(){"ID"}) },
                {"Name", new PropertyMappingValue(new List<string>(){"Name"}) },
                {"Quantity", new PropertyMappingValue(new List<string>(){"Quantity"}) },
                {"Price", new PropertyMappingValue(new List<string>(){"Price"}) }
           };

        private IList<IPropertyMapping> _propertyMappings = new List<IPropertyMapping>();

        public PropertyMappingService()
        {
            _propertyMappings.Add(new PropertyMapping<CategoryDTO, Category>(propertyMapping1));
            _propertyMappings.Add(new PropertyMapping<AreaDTO, Area>(propertyMapping1));
            _propertyMappings.Add(new PropertyMapping<CompanyDTO, Company>(companyPropertyMapping));
            _propertyMappings.Add(new PropertyMapping<DepartmentDTO, Department>(propertyMapping1));
            _propertyMappings.Add(new PropertyMapping<MenuDTO, Menu>(propertyMapping1));
            _propertyMappings.Add(new PropertyMapping<SupplierDTO, Supplier>(propertyMapping1));
            _propertyMappings.Add(new PropertyMapping<ProductDTO, Product>(productPropertyMapping));
            _propertyMappings.Add(new PropertyMapping<UserDTO, User>(propertyMapping1));
            _propertyMappings.Add(new PropertyMapping<OrderDTO, Order>(propertyMapping1));
            _propertyMappings.Add(new PropertyMapping<RoleDTO, Role>(propertyMapping1));
        }

        public Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource, TDestination>()
        {
            var matchingMapping = _propertyMappings.OfType<PropertyMapping<TSource, TDestination>>();

            if (matchingMapping.Count() == 1)
            {
                return matchingMapping.First()._mappingDictionary;
            }

            throw new Exception($"Cannot find exact property mapping instance for {typeof(TSource)}, {typeof(TDestination)}");
        }
    }
}
