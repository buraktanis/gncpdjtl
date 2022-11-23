using System;
using System.Collections.Generic;
using System.Text;
using Core.Results;
using Entities.Concrete;

namespace Business
{
    public interface IArticleService
    {
        IDataResult<Article> GetById(int articleId);
        IResult Add(Article article);
    }
}
  