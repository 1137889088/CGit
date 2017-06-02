using CGit.Src.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGit.Src.bll
{
    public class FollowUserBll
    {
        private FollowUserDao dao = new FollowUserDao();
        public List<string> findAllFollowUserByUserEmail(string userEmail)
        {
            return dao.findAllFollowUserByUserEmail(userEmail);
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
        public List<string> findUserAndCount()
        {
            return dao.findUserAndCount();
        }
    }

}
