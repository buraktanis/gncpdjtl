using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Repositories.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfArticleDal : EfEntityRepositoryBase<Article, ApplicationDbContext>, IArticleDal
    {
        //public List<ArticleDetailDto> GetArticleDetail()
        //{
        //    using (ApplicationDbContext context= new ApplicationDbContext())
        //    {
        //        var result = from a in context.Articles
        //            join c in context.Categories on a.CategoryId equals c.Id
        //            //join u in context.Users on a.UserId equals u.Id 
        //            select new ArticleDetailDto
        //            {
        //                ArticleId = a.Id, Hood = a.Hood, CategoryName = c.CategoryName,
        //                //UserName = u.FirstName
        //            };
        //        return result.ToList();
                 

    }
}
