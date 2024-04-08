using Business.Abstract;
using Business.Constants;
using Core.Service;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ArticleManager : IArticleService
    {
        IArticleDal _articleDal;

        public ArticleManager(IArticleDal articleDal)
        {
            _articleDal = articleDal;
        }

        public IResult Add(Article entity)
        {
            _articleDal.Add(entity);
            return new SuccessResult(Messages.Article_Add);
        }

        public IResult Delete(Article entity)
        {
            _articleDal.Add(entity);
            return new SuccessResult(Messages.Article_Deleted);
        }

        public IDataResult<List<Article>> GetAll()
        {
            return new SuccessDataResult<List<Article>>(_articleDal.GetAll(),Messages.Article_Listed);
        }

        public IDataResult<Article> GetById(int id)
        {
            return new SuccessDataResult<Article>(_articleDal.Get(x => x.Id == id), Messages.Article_List);
        }

        public IResult Update(Article entity)
        {
            _articleDal.Add(entity);
            return new SuccessResult(Messages.Article_Edit);
        }

       
    }
}
