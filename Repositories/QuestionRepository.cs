using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ideal.Models;
using ideal.ModelView;
using System.Data.Entity.Core.Objects;
using ideal.Helper;

namespace ideal.Repositories
{
    public class QuestionRepository
    {
        private IdealDbEntities _db = new IdealDbEntities();

        //public List<QuestionViewModel> ListQuestion(string category)
        //{
        //    try
        //    {
        //        var list = (from l in _db.GetQuestionList(category)
        //                    select new QuestionViewModel()
        //                    {
        //                        Id = l.Id,
        //                        Question = l.Question,
        //                        CategoryName = l.CategoryName,
        //                        CreatedBy = l.CreatedBy,
        //                        EmpId = l.EMP_ID
        //                    }).ToList();

        //        return list;
        //    }
        //    catch (Exception ex)
        //    {
        //        var ErrMeg = ex.InnerException.Message + ex.Message;
        //        return null;
        //    }
        //}

        public List<QuestionViewModel> ListQuestionById(int id)
        {
            try
            {
                var list = (from l in _db.GetQuestionById(id)
                            select new QuestionViewModel()
                            {
                                Id = l.Id,
                                Question = l.Question,
                                CategoryName = l.CategoryName,
                                CreatedBy = l.CreatedBy,
                                EmpId = l.EMP_ID
                            }).ToList();

                return list;
            }
            catch (Exception ex)
            {
                var ErrMeg = ex.InnerException.Message + ex.Message;
                return null;
            }
        }

        // --------------------------------- Search data ------------------------ //
        public List<QuestionViewModel> SearchData(string keyword)
        {
            try
            {
                var list = (from l in _db.SearchQuestion(keyword)
                            select new QuestionViewModel()
                            {
                                Question = l.Question,
                                CategoryName = l.CategoryName,
                                CreatedBy = l.CreatedBy,
                                EmpId = l.Username
                            }).ToList();

                return list;
            }
            catch (Exception ex)
            {
                var ErrMeg = ex.InnerException.Message + ex.Message;
                return null;
            }
        }
        // --------------------------------- Search data ------------------------ //

        public ListResult<HistoryViewModel> ListHistory()
        {
            try
            {
                var list = (from l in _db.GetHistory()
                            select new HistoryViewModel()
                            {
                                Question = l.Question,
                                LoginId = l.EMP_ID,
                                Name = l.NAME,
                                EnterDate = l.ENTER_DATE,
                                WorkGroup = l.WORKGROUP,
                                CreatedDate = l.CreatedDate
                            }).ToList();
                var pagination = new ListResult<HistoryViewModel>()
                {
                    Results = list,
                    RowCount = 30
                };

                return pagination;
            }
            catch (Exception ex)
            {
                var Err = ex.InnerException + ex.Message;
                return null;
            }
        }
        //------------------------- End ------------------------//


        public List<HistoryViewModel> GetHistory()
        {
            try
            {
                var list = (from l in _db.GetHistory()
                            select new HistoryViewModel()
                            {
                                Question = l.Question,
                                LoginId = l.EMP_ID,
                                Name = l.NAME,
                                EnterDate = l.ENTER_DATE,
                                WorkGroup = l.WORKGROUP,
                                CreatedDate = l.CreatedDate
                            }).ToList();
                return list;
            }
            catch (Exception ex)
            {
                var Err = ex.InnerException + ex.Message;
                return null;
            }
        }


        public List<HistoryViewModel> History()
        {
            try
            {
                var list = (from l in _db.GetHistory()
                            select new HistoryViewModel()
                            {
                                Question = l.Question,
                                LoginId = l.EMP_ID,
                                Name = l.NAME,
                                EnterDate = l.ENTER_DATE,
                                WorkGroup = l.WORKGROUP,
                                CreatedDate = l.CreatedDate
                            }).ToList();

                return list;
            }
            catch (Exception ex)
            {
                var Err = ex.InnerException + ex.Message;
                return null;
            }
        }

        public string Insert(string content, int category, int? createdBy)
        {
            try
            {
                if (content == null)
                    return "Error";
                _db.InsertQuestion(category, content, createdBy);
                return "Ok";
            }
            catch (Exception ex)
            {
                return "Message" + ex.Message + " Inner Exception: " + ex.InnerException.Message;
            }

        }
        public AnswersViewModel GetAnswerById(int id)
        {
            try
            {
                var item = (from l in _db.GetAnswer(id)
                            select new AnswersViewModel()
                            {
                                Question = l.Question,
                                Answer = l.Answer,
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