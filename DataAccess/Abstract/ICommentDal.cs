using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface ICommentDal : IGenericDal<Comment>
    {
        Comment GetByIdWithIncludes(int id);
        List<Comment> GetAllWithIncludes();
    }
}
