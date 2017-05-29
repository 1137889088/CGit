using CGit.Models;
using CGit.Src.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGit.Src.bll
{
    public class CommentBll
    {
        CommentDao dao = new CommentDao();
        public List<Comment> findAllcommentByRepositoryId(string repositoryId)
        {
            return dao.findAllcommentByRepositoryId(repositoryId);
        }
        public int save(Comment comment)
        {
            return dao.save(comment);
        }
    }
}
