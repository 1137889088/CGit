using CGit.Src.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGit.Src.bll
{
    public class FollowRepositoryBll
    {
        private FollowRepositoryDao dao = new FollowRepositoryDao();
        public List<string> findAllFollowRepositoryByRepositoryEmail(string userEmail)
        {
            return dao.findAllFollowRepositoryByRepositoryEmail(userEmail);
        }

        public int save(string userEmail, string followEmail)
        {
            return dao.save(userEmail, followEmail);
        }
        public int delete(string userEmail, string followEmail)
        {
            return dao.delete(userEmail, followEmail);
        }
        public bool isFollow(string userEmail, string followEmail)
        {
            return dao.isFollow(userEmail, followEmail);
        }
        public List<string> findRepositoryAndCount()
        {
            return dao.findRepositoryAndCount();
        }
    }
}
