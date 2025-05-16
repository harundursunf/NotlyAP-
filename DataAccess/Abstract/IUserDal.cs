using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IUserDal:IGenericDal<User>
    {
        User GetByFilter(Expression<Func<User, bool>> filter);
    }
}
