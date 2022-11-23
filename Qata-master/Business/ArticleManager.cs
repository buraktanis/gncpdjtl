using System;
using System.Collections.Generic;
using System.Text;
using Business;
using Core.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business
{
    public class ArticleManager:IArticleService
    {
        IArticleDal _articleDal;

        public ArticleManager(IArticleDal articleDal)
        {
            _articleDal = articleDal;
        }

        public IResult Add(Article article)
        {
           
                _articleDal.Add(article);
                return new SuccessResult("successful");
        }

        public IDataResult<Article> GetById(int articleId)
        {
            return new SuccessDataResult<Article>(_articleDal.Get(a => a.Id == articleId).Data);
        }
    }
}
