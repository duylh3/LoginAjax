using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ideal.Repositories;
using System.IO;

namespace ideal.Controllers
{
    public class DocumentController : Controller
    {
        DocumentRepository _re = new DocumentRepository();

        // GET: Document
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ThamKhao()
        {
            return View();
        }

        public ActionResult GetData(string cate, int page, int pageSize)
        {
            var categoryName = "";
            var list = _re.ListDocument(cate);
            var model = list.Skip((page - 1) * pageSize).Take(pageSize);
            var totalRow = list.Count;
            if (totalRow == 0)
            {
                return Json(new
                {
                    data = model,
                    total = totalRow,
                    status = true,
                 //   categoryName = ""
                }, JsonRequestBehavior.AllowGet);
            }
            else
                categoryName = list.FirstOrDefault().CategoryName;

            return Json(new
            {
                data = model,
                total = totalRow,
                status = true,
                categoryName = categoryName
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDocumentDetail(int id)
        {
            var data = _re.GetDocumentDetail(id);

            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}