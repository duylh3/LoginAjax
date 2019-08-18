using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ideal.Models;
using ideal.Helper;
using ideal.ModelView;

namespace ideal.Repositories
{
    public class DocumentRepository
    {
        private IdealDbEntities _db = new IdealDbEntities();

        public List<DocumentViewModel> ListDocument(string category)
        {
            try
            {
                var list = (from l in _db.GetDocumentList(category)
                            select new DocumentViewModel()
                            {
                                Id = l.Id,
                                Category = l.Category,
                                DocumentName = l.DocumentName,
                                CategoryName = l.CategoryName,
                                Url = l.Url,
                                Username = l.Username
                            }).ToList();
   
                return list;
            }
            catch (Exception ex)
            {
                var ErrMeg = ex.InnerException.Message + ex.Message;
                return null;
            }
        }

        public DocumentViewModel GetDocumentDetail(int id)
        {
            try
            {
                var item = (from l in _db.GetDocumentDetail(id)
                            select new DocumentViewModel()
                            {                              
                                DocumentName = l.DocumentName,
                                Url = l.Url,
                                CreatedBy = l.CreatedBy,
                                CreatedDate = l.CreatedDate,
                            }).FirstOrDefault();
                return item;
            }
            catch (Exception ex)
            {
                var ErrMeg = ex.InnerException.Message + ex.Message;
                return null;
            }
        }
    }
}