using Core.Librarys.SQLite;
using Core.Models;
using Core.Servicers.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Servicers.Instances
{
    public class WebSiteCategorys : IWebSiteCategorys
    {
        private List<WebSiteCategoryModel> _categories;

        public WebSiteCategorys()
        {
            _categories = new List<WebSiteCategoryModel>();
        }

        public WebSiteCategoryModel Create(WebSiteCategoryModel category)
        {
            using (var db = new TaiDbContext())
            {
                db.WebSiteCategories.Add(category);
                db.SaveChanges();
                _categories.Add(category);
                return category;
            }
        }

        public void Delete(WebSiteCategoryModel category)
        { 
            using (var db = new TaiDbContext())
            {
                var item = db.WebSiteCategories.Where(m => m.ID == category.ID).FirstOrDefault();
                if (item != null)
                { 
                    db.WebSiteCategories.Remove(item);
                    db.SaveChanges();
                    _categories.Remove(category);
                }
            }
        }

        public List<WebSiteCategoryModel> GetCategories()
        {
            return _categories;
        }

        public WebSiteCategoryModel GetCategory(int id)
        {
            return _categories.Where(m => m.ID == id).FirstOrDefault();
        }

        public void Load()
        {
            Debug.WriteLine("加载网站分类");
            using (var db = new TaiDbContext())
            {
                _categories = db.WebSiteCategories.ToList();
                Debug.WriteLine("加载网站分类完成");
            }
        }

        public void Update(WebSiteCategoryModel category)
        { 
            using (var db = new TaiDbContext())
            {
                db.Entry(category).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}