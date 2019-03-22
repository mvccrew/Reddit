using DataAccess.Entities;
using DataAccess.Repositories;
using Reddit.ViewModels.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace Reddit.Controllers
{
    public abstract class BaseController<E, R, TFilterVM, TIndexVM, TEditVM> : Controller
    where E : BaseEntity, new()
        where R : BaseRepository<E>, new()
        where TFilterVM : BaseFilterVM<E>, new()
        where TIndexVM : BaseIndexVM<E, TFilterVM>, new()
        where TEditVM : BaseEditVM<E>, new()
    {
        public virtual void PopulateIndexVM(TIndexVM model) { }
        public virtual void PopulateEditVM(TEditVM model) { }

        [HttpGet]
        public ActionResult Index(TIndexVM model)
        {
            model.Pager = model.Pager ?? new PagerVM();
            model.Pager.Page = model.Pager.Page <= 0 ? 1 : model.Pager.Page;
            model.Pager.ItemsPerPage = model.Pager.ItemsPerPage <= 0 ? 10 : model.Pager.ItemsPerPage;

            model.Filter = model.Filter ?? new TFilterVM();
            PopulateIndexVM(model);

            Expression<Func<E, bool>> filter = model.Filter.GenerateFilter();

            R repo = new R();
            model.Items = repo.GetAll(filter, model.Pager.Page, model.Pager.ItemsPerPage);
            model.Pager.PagesCount = (int)Math.Ceiling(repo.Count(filter) / (double)(model.Pager.ItemsPerPage));

            return View(model);
        }

        [HttpGet]
        public virtual ActionResult Edit(int? id)
        {
            E item = null;

            R repo = new R();
            item = id == null ? new E() : repo.GetById(id.Value);

            TEditVM model = new TEditVM();
            model.PopulateModel(item);
            PopulateEditVM(model);

            return View(model);
        }

        [HttpPost]
        public virtual ActionResult Edit(TEditVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            R repo = new R();
            E item = new E();
            model.PopulateEntity(item);

            repo.Save(item);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            R repo = new R();
            E item = repo.GetById(id);

            repo.Delete(item);

            return RedirectToAction("Index");
        }
    }
}