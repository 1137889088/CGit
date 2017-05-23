using CGit.Models;
using CGit.Src.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGit.Src.bll
{
    public class RepositoryBll
    {
        RepositoryDao dao = new RepositoryDao();
        public List<Repository> findAllRepositoryByEmail(string email)
        {
            return dao.findAllRepositoryByEmail(email);
        }
        public Repository findRepositoryById(string id)
        {
            return dao.findRepositoryById(id);
        }
        public int deleteRepositoryById(string id)
        {
            return dao.deleteRepositoryById(id);
        }
        public int save(Repository repository)
        {
            return dao.save(repository);
        }
        public int saveAndReturnId(Repository repository)
        {
            return dao.saveAndReturnId(repository);
        }
    }
}
