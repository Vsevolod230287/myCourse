using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using myCourse.Models.Options;

namespace myCourse.Models.Services.Infrastructure
{
   public class SQLiteDatabaseAccessor : IDatabaseAccessor
   {
      public IOptionsMonitor<ConnectionStringsOptions> connectionStringsOptions { get; }
      private readonly ILogger<SQLiteDatabaseAccessor> logger;

      public SQLiteDatabaseAccessor(IOptionsMonitor<ConnectionStringsOptions> connectionStringsOptions, ILogger<SQLiteDatabaseAccessor> logger)
      {
         this.logger = logger;
         this.connectionStringsOptions = connectionStringsOptions;

      }

      public async Task<DataSet> QueryAsync(FormattableString formattableQuery)
      {
         logger.LogInformation(formattableQuery.Format, formattableQuery.GetArguments());

         var queryArguments = formattableQuery.GetArguments();

         var sqliteParameters = new List<SqliteParameter>();

         for (int i = 0; i < queryArguments.Length; i++)
         {
            var parameter = new SqliteParameter(i.ToString(), queryArguments[i]);
            sqliteParameters.Add(parameter);
            queryArguments[i] = "@" + i;

         }
         string query = formattableQuery.ToString();

         string connectionString = connectionStringsOptions.CurrentValue.Default;

         using (var conn = new SqliteConnection(connectionString))
         {
            await conn.OpenAsync();

            using (var cmd = new SqliteCommand(query, conn))
            {
               cmd.Parameters.AddRange(sqliteParameters);
               using (var reader = await cmd.ExecuteReaderAsync())
               {
                  DataSet dataSet = new DataSet();
                  dataSet.EnforceConstraints = false;
                  do
                  {
                     DataTable dataTable = new DataTable();
                     dataSet.Tables.Add(dataTable);
                     dataTable.Load(reader);
                  } while (!reader.IsClosed);


                  return dataSet;
               }
            }
         }
      }
   }
}