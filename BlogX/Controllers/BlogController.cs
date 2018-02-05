using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogX.Models;
using BlogX.data;

namespace BlogX.Controllers
{
    public class BlogController: Controller
    {
        private BlogRepository __blogRepository;
        private int _pageSize = 2;

        public BlogController()
        {
            __blogRepository = BlogRepository.GetRepository();
        }
        
        

        public ActionResult Index(int page = 0, bool json = false)
        {
            int offset = _pageSize * page;
            int maxPage = -1 + (__blogRepository.GetBlogCount() / _pageSize);
            bool hasNext = maxPage > page;
            bool hasPrev = maxPage >= 0 && page > 0;
            
            List<Blog> resultset = __blogRepository.GetBlogs().OrderByDescending(b=>b.Id)
                .Skip(offset).Take(_pageSize).ToList();
            if (!json)
            {
                ViewBag.HasNext = hasNext;
                ViewBag.HasPrev = hasPrev;
                ViewBag.Page = page;
                return View(resultset);
            } else
            {
                return Json(new
                    {
                        page = page,
                        hasNext = hasNext,
                        hasPrev = hasPrev,
                        results = resultset
                    }, 
                    JsonRequestBehavior.AllowGet
                );
            }
        }

        /*
        [HttpPost]
        public ActionResult Index(Blog b)
        {
            //ViewBag.Title = name;
            // = new Blog();
            return View(b);

        }
        */

        public ActionResult Detail(int? id)
        {
            Blog b = __blogRepository.GetBlog((int) id);
            if(b == null)
            {
                return RedirectToAction("index");
            }
            return View(b);

        }

        public ActionResult Edit(int? id)
        {
            Blog b; 
            if (id == null || (b = __blogRepository.GetBlog((int)id) ) == null)
            {
                return HttpNotFound(); // new HttpNotFoundResult();
            }
            return View(b);
        }

        [HttpPost]
        public ActionResult Edit(Blog b)
        {
            if (this.ModelState.IsValid)
            {
                //save and redirect to detail
                __blogRepository.Update(b);
                return RedirectToAction("Index");
            }
            return View(b);
        }
        public ActionResult Add(int? id)
        {            
            return View("Edit", new Blog());
        }

        [HttpPost]
        public ActionResult Add(Blog b)
        {
            if (this.ModelState.IsValid)
            {
                __blogRepository.SaveBlog(b);

                return RedirectToAction("Index");
            }
            return View("Edit", b);
        }

        public ActionResult fun()
        {
            return null;

        }
    }
}