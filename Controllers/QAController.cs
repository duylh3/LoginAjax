using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ideal.ModelView;
using ideal.Repositories;
using System.Net.Mail;
using System.Net;
using ideal.Models;
using System.Configuration;
using System.Text;
using ClosedXML.Excel;
using System.Data;
using System.IO;

namespace ideal.Controllers
{
    public class QAController : Controller
    {

        private QuestionRepository _re = new QuestionRepository();
        //    GET: QA
        public ActionResult Index()
        {
            return Redirect("~/QA/SendFeedback");
        }

        [HttpGet]
        public ActionResult CauHoi()
        {
            return Redirect("~/QA/SendFeedback");
        }

        public ActionResult GetData(int id, int page, int pageSize)
        {
            var list = _re.ListQuestionById(id);
            var model = list.Skip((page - 1) * pageSize).Take(pageSize);
            var totalRow = list.Count;

            return Json(new
            {
                data = model,
                total = totalRow,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SearchData(string keyword, int page, int pageSize)
        {
            var list = _re.SearchData(keyword);
            var model = list.Skip((page - 1) * pageSize).Take(pageSize);
            var totalRow = list.Count;

            return Json(new
            {
                data = model,
                total = totalRow,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }



        public JsonResult GetAnswer(int id)
        {
            var data = _re.GetAnswerById(id);

            return Json(data, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult SendFeedback()
        {
            return View();
        }

        public ActionResult Feedback(string content, int category, int? subCategory)
        {
            var status = true;
            try
            {
                int? createdBy = 0;
                if ((category == 17 && subCategory == 21) || (category == 17 && subCategory == 22))
                {
                    createdBy = null;
                    SaveData(category, (int)subCategory, content, createdBy);
                }
                else
                {
                    createdBy = (int)Session["UserId"];
                    SaveData(category, (int)subCategory, content, createdBy);
                }
                return Json(status, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                status = false;
                var Err = ex.InnerException;
                return Json(status, JsonRequestBehavior.AllowGet);
            }
        }

        private void SaveData(int category, int subCategory, string content, int? createdBy)
        {
            QuestionRepository _reQuestion = new QuestionRepository();
            if (subCategory != 0)
            {
                category = (int)subCategory;
                _reQuestion.Insert(content, category, createdBy);
            }
            else
            {
                _reQuestion.Insert(content, category, createdBy);
            }
            SendEmail(content);
        }

        public ActionResult GetCategory()
        {
            IdealDbEntities _db = new IdealDbEntities();

            return Json(_db.tb_Categories.Where(x => x.Type == "QA" && x.ParentId == null).Select(x => new
            {
                Id = x.Id,
                Category = x.Category,
                CategoryName = x.CategoryName
            }).ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSubCategory(int id)
        {
            IdealDbEntities _db = new IdealDbEntities();

            return Json(_db.tb_Categories.Where(x => x.Type == "QA" && x.ParentId == id).Select(x => new
            {
                Id = x.Id,
                Category = x.Category,
                CategoryName = x.CategoryName
            }).ToList(), JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult History()
        {
            return View();
        }

        [HttpPost]
        public FileResult ExportExcel()
        {

            DataTable dt = new DataTable("Grid");

            dt.Columns.AddRange(new DataColumn[4] { new DataColumn("Question"),
                                            new DataColumn("Login ID"),
                                            new DataColumn("Name"),
                                            new DataColumn("Workgroup") });
            var data = _re.History();

            foreach (var item in data)
            {
                dt.Rows.Add(item.Question, item.LoginId, item.Name, item.WorkGroup);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);

                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Grid.xlsx");
                }
            }
        }

        public ActionResult GetHistory(int page, int pageSize)
        {
            var list = _re.GetHistory();
            var model = list.Skip((page - 1) * pageSize).Take(pageSize);
            var totalRow = list.Count;

            return Json(new
            {
                data = model,
                total = totalRow,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        public static bool SendEmail(string body)
        {
            try
            {
                AppSettingsReader app = new AppSettingsReader();
                string from = (string)app.GetValue("FromEmailAddress", typeof(String));
                string pass = (string)app.GetValue("FromEmailPassword", typeof(String));
                string hostName = (string)app.GetValue("SMTPHost", typeof(String));
                string nameDisplay = (string)app.GetValue("FromEmailDisplayName", typeof(String));
                int port = (int)app.GetValue("SMTPPort", typeof(int));
                string To = "duylh@posco.net";
                string subject = "Mail góp ý";

                SmtpClient smtp = new SmtpClient();
                smtp.Host = hostName;
                smtp.Port = port;
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential(from, pass);

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(from, nameDisplay);
                mail.To.Add(To);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;
                mail.BodyEncoding = UTF8Encoding.UTF8;

                smtp.Send(mail);

                return true;


            }
            catch (Exception ex)
            {
                var Err = ex.InnerException + ex.Message;
                return false;
            }
        }
    }
}