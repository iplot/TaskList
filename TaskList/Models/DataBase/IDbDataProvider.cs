using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskList.Models.DataBase
{
    public interface IDbDataProvider
    {
        Task<DataBaseResult> GetData();

        Task<DataBaseResult> UpdateData(int targetId, IParameters param);

        Task<DataBaseResult> DeleteData(int targetId);

        Task<DataBaseResult> AddData(object targetObject);
    }
}
