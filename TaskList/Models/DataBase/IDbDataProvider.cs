using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskList.Models.DataBase
{
    public interface IDbDataProvider
    {
        DataBaseResult GetData();

        DataBaseResult UpdateData(int targetId, IParameters param);

        DataBaseResult DeleteData(int targetId);

        DataBaseResult AddData(object targetObject);
    }
}
