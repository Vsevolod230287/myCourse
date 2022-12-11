using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace myCourse.Models.Services.Infrastructure
{
   public interface IDatabaseAccessor
   {
      Task<DataSet> QueryAsync(FormattableString query);
   }
}