using CGit.Dao;
using CGit.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGit.Src.Dao
{
    public class UserDao:BaseDao<User>
    {
        public static String dataBase = "CGit";
        /// <summary>
        /// 将数据库记录封装成User对象的方法
        /// </summary>
        /// <param name="reader">SqlDataReader类</param>
        /// <returns>封装好的一个User对象</returns>
        public User userHandler(SqlDataReader reader)
        {
            User user = new User();
            string email = reader.GetString(0);
            user.email = email;
            user.name = reader.GetString(1);
            user.pwd = reader.GetString(2);
            user.area = reader.GetString(3);
            user.resume = reader.GetString(4);
            return user;
        }
        /// <summary>
        /// 查询所有用户
        /// </summary>
        /// <returns>封装了所有用户的list，如果未找到Count为0</returns>
        public List<User> findAllUser()
        {
            String sql = "SELECT * from CGit.users";
            return this.query(dataBase, sql, null, userHandler);
        }
        /// <summary>
        /// 根据E-mail查找用户
        /// </summary>
        /// <param name="emial"></param>
        /// <returns>找到的用户如果为找到则返回null</returns>
        public User findUserByEmail(String email)
        {
            User user = new User();
            String sql = "SELECT * from CGit.users where email= @email";
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            sqlParameters.Add(new SqlParameter("@email", email));
            List<User> list = query(dataBase, sql, sqlParameters, userHandler);
            if (list.Count>0){
                return list[0];
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 判断是否存在email和pwd对应的用户
        /// </summary>
        /// <param name="emial">email</param>
        /// <param name="pwd">pwd</param>
        /// <returns>找到的用户，如果未找到返回null</returns>
        public User login(string email,string pwd)
        {
            User user = new User();
            String sql = "SELECT * from CGit.users where email= @email and pwd= @pwd";
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            sqlParameters.Add(new SqlParameter("@email", email));
            sqlParameters.Add(new SqlParameter("@pwd", pwd));
            List<User> list = query(dataBase, sql, sqlParameters, userHandler);
            if (list.Count > 0)
            {
                return list[0];
            }
            else
            {
                return null;
            }

        }
        public int save(User user)
        {
            String sql = "INSERT INTO CGit.users (email, name, pwd, area, resume) VALUES(@email, @name, @pwd, @area, @resume)";
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            sqlParameters.Add(new SqlParameter("@email", user.email));
            sqlParameters.Add(new SqlParameter("@name", user.name));
            sqlParameters.Add(new SqlParameter("@pwd", user.pwd));
            sqlParameters.Add(new SqlParameter("@area", user.area));
            sqlParameters.Add(new SqlParameter("@resume", user.resume));
            return update(dataBase, sql, sqlParameters);
        }
    }
}
