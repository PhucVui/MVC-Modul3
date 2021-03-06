﻿using StoreManagement.Models;
using StoreManagement.Models.Entities;
using System.Collections.Generic;
using System.Linq;

namespace StoreManagement.Services
{
    public class CategoryService : ICategoryService
    {
        public const int DefaultCategoryId= 20;
        private readonly StoreDbContext _context;

        public CategoryService(StoreDbContext context)
        {
            this._context = context;
        }

        public int CreateCategory(Category category)
        {
            _context.Add(category);
            return (_context.SaveChanges());
        }

        public int DeleteCategory(int id)
        {
            var category = _context.Categories.FirstOrDefault(el => el.Id == id);

            if (category != null)
            {
                List<Product> Products = _context.Products.ToList().FindAll(el => el.CategoryId == category.Id);

                Products.ForEach(el => el.CategoryId = DefaultCategoryId);
                _context.UpdateRange(Products);

                _context.Categories.Remove(category);

                return _context.SaveChanges();
            }
            return -1;
        }

        public IEnumerable<Category> GetCategories()
        {
            return (_context.Categories.ToList());
        }

        public Category GetCategoryById(int id)
        {
            return _context.Categories.ToList().Find(el => el.Id == id);
        }

        public List<Product> GetListProductByCategoryId(int id)
        {
            return _context.Products.ToList().FindAll(el=>el.CategoryId==id);
        }

        //khi xóa category sẻ thay đổi các categoryId của những sản phẩm thuộc category đó = DefaultCategoryId
        public int MoveCategoryForProduct(int id)
        {
            var product = _context.Products.FirstOrDefault(el=>el.Id==id);
            product.CategoryId = DefaultCategoryId;
            _context.Update(product);
            return _context.SaveChanges();
        }

        public int MoveRangeCategoryForProduct(List<Product> products)
        {
            _context.UpdateRange(products);
            return _context.SaveChanges();
        }

        public int UpdateCategory(Category category)
        {
            if (category == null)
            {
                return -1;
            }
            var FindCategory = GetCategoryById(category.Id);

            FindCategory.Name = category.Name;
            FindCategory.Status = category.Status;
            FindCategory.Products = category.Products;

            _context.Update(FindCategory);
            return _context.SaveChanges();
        }
    }
}
