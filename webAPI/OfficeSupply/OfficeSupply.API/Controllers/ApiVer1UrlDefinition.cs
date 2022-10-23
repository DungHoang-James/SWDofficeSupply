using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfficeSupply.API.Controllers
{
    public class ApiVer1UrlDefinition
    {
        public static class Auth
        {
            internal const string Token = AppConstants.ApiPrefix + "auth";
        }
        public static class User
        {
            internal const string Users = AppConstants.ApiPrefix + "users";
        }
        public static class OrderDetail
        {
            internal const string OrderDetails = AppConstants.ApiPrefix + "orderdetails";
        }
        public static class Supplier
        {
            internal const string Suppliers = AppConstants.ApiPrefix + "suppliers";
        }
        public static class Category
        {
            internal const string Categories = AppConstants.ApiPrefix + "catergories";
            internal const string Add = AppConstants.ApiPrefix + "catergory";
            internal const string Get = AppConstants.ApiPrefix + "catergories";
            internal const string GetDetail = AppConstants.ApiPrefix + "catergory";
            internal const string Update = AppConstants.ApiPrefix + "catergory";
            internal const string Delete = AppConstants.ApiPrefix + "catergory";
        }
        public static class Company
        {
            internal const string Companies = AppConstants.ApiPrefix + "companies";
            internal const string Add = AppConstants.ApiPrefix + "company";
            internal const string Get = AppConstants.ApiPrefix + "companys";
            internal const string GetDetail = AppConstants.ApiPrefix + "company";
            internal const string Update = AppConstants.ApiPrefix + "company";
            internal const string Delete = AppConstants.ApiPrefix + "company";
        }
        public static class Department
        {
            internal const string Departments = AppConstants.ApiPrefix + "departments";
            internal const string Add = AppConstants.ApiPrefix + "department";
            internal const string Get = AppConstants.ApiPrefix + "departments";
            internal const string GetDetail = AppConstants.ApiPrefix + "department";
            internal const string Update = AppConstants.ApiPrefix + "department";
            internal const string Delete = AppConstants.ApiPrefix + "department";
        }
        public static class Menu
        {
            internal const string Menus = AppConstants.ApiPrefix + "menus";
            internal const string Add = AppConstants.ApiPrefix + "menu";
            internal const string Get = AppConstants.ApiPrefix + "menus";
            internal const string GetDetail = AppConstants.ApiPrefix + "menu";
            internal const string Update = AppConstants.ApiPrefix + "menu";
            internal const string Delete = AppConstants.ApiPrefix + "menu";
        }
        public static class Order
        {
            internal const string Orders = AppConstants.ApiPrefix + "orders";
            internal const string Add = AppConstants.ApiPrefix + "order";
            internal const string Get = AppConstants.ApiPrefix + "orders";
            internal const string GetDetail = AppConstants.ApiPrefix + "order";
            internal const string Update = AppConstants.ApiPrefix + "order";
            internal const string Delete = AppConstants.ApiPrefix + "order";
        }
        public static class Period
        {
            internal const string Periods = AppConstants.ApiPrefix + "periods";
            internal const string Add = AppConstants.ApiPrefix + "period";
            internal const string Get = AppConstants.ApiPrefix + "periods";
            internal const string GetDetail = AppConstants.ApiPrefix + "period";
            internal const string Update = AppConstants.ApiPrefix + "period";
            internal const string Delete = AppConstants.ApiPrefix + "period";
        }
        public static class Product
        {
            internal const string Products = AppConstants.ApiPrefix + "products";
            internal const string Add = AppConstants.ApiPrefix + "product";
            internal const string Get = AppConstants.ApiPrefix + "products";
            internal const string GetDetail = AppConstants.ApiPrefix + "product";
            internal const string Update = AppConstants.ApiPrefix + "product";
            internal const string Delete = AppConstants.ApiPrefix + "product";
        }
        public static class Role
        {
            internal const string Roles = AppConstants.ApiPrefix + "roles";
            internal const string Add = AppConstants.ApiPrefix + "role";
            internal const string Get = AppConstants.ApiPrefix + "roles";
            internal const string GetDetail = AppConstants.ApiPrefix + "role";
            internal const string Update = AppConstants.ApiPrefix + "role";
            internal const string Delete = AppConstants.ApiPrefix + "role";
        }

        public static class Area
        {
            internal const string Areas = AppConstants.ApiPrefix + "areas";
            internal const string Add = AppConstants.ApiPrefix + "area";
            internal const string Get = AppConstants.ApiPrefix + "areas";
            internal const string GetDetail = AppConstants.ApiPrefix + "area";
            internal const string Update = AppConstants.ApiPrefix + "area";
            internal const string Delete = AppConstants.ApiPrefix + "area";
        }

    }

}
