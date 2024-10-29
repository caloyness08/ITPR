using Dapper;
using Dapper.Contrib.Extensions;
using eITPR.Models;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;
using KeyAttribute = System.ComponentModel.DataAnnotations.KeyAttribute;

namespace eITPR.Data {
    public class DapperRepository<TEntity> : IRepository<TEntity> where TEntity : class {
        private string _sqlConnectionString;
        private string entityName;
        private Type entityType;

        private string primaryKeyName;
        private string primaryKeyType;
        private bool PKNotIdentity = false;

        public DapperRepository(string sqlConnectionString) {
            _sqlConnectionString = sqlConnectionString;
            entityType = typeof(TEntity);
            entityName = entityType.Name;

            var props = entityType.GetProperties().Where(
                prop => Attribute.IsDefined(prop,
                typeof(KeyAttribute)));
            if (props.Count() > 0) {
                primaryKeyName = props.First().Name;
                primaryKeyType = props.First().PropertyType.Name;
            }
            else {
                // Default
                primaryKeyName = "Id";
                primaryKeyType = "Int32";
            }

            // look for [ExplicitKey]
            props = entityType.GetProperties().Where(
                prop => Attribute.IsDefined(prop,
                typeof(ExplicitKeyAttribute)));
            if (props.Count() > 0) {
                PKNotIdentity = true;
                primaryKeyName = props.First().Name;
                primaryKeyType = props.First().PropertyType.Name;
            }
        }


        //GENERAL USE ENTITY

        public async Task<bool> DeleteAsync(TEntity entityToDelete) {
            using (IDbConnection db = new SqlConnection(_sqlConnectionString)) {
                //string sql = $"delete from {entityName} where {primaryKeyName}" +
                //    $" = @{primaryKeyName}";
                try {
                    //await db.ExecuteAsync(sql, entityToDelete);
                    await db.DeleteAsync<TEntity>(entityToDelete);
                    return true;
                }
                catch (Exception ex) {
                    return false;
                }
            }

        }

        public async Task<IEnumerable<TEntity>> GetAsync(string Query) {
            using (IDbConnection db = new SqlConnection(_sqlConnectionString)) {
                try {
                    return await db.QueryAsync<TEntity>(Query);
                }
                catch (Exception ex) {
                    return (IEnumerable<TEntity>)new List<TEntity>();
                }
            }
        }
    
        public async Task<IEnumerable<TEntity>> GetAsyncSP(string procedureName, string[] parameters) {
            using (IDbConnection db = new SqlConnection(_sqlConnectionString)) {
                try {

                    var _parameters = new DynamicParameters();
                    foreach (var val in parameters) {
                        _parameters.Add(val, val);
                    }
                    return await db.QueryAsync<TEntity>(procedureName, _parameters,commandType:CommandType.StoredProcedure);
                }
                catch (Exception ex) {
                    return (IEnumerable<TEntity>)new List<TEntity>();
                }
            }
        }

        public async Task<IEnumerable<TEntity>> GetAsyncSP(string procedureName,string fieldName, string parameters) {
            using (IDbConnection db = new SqlConnection(_sqlConnectionString)) {
                try {

                    var _parameters = new DynamicParameters();
                    _parameters.Add(fieldName, parameters);
                    return await db.QueryAsync<TEntity>(procedureName, _parameters, commandType: CommandType.StoredProcedure);
                }
                catch (Exception ex) {
                    return (IEnumerable<TEntity>)new List<TEntity>();
                }
            }
        }

        public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>>
            filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "") {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync() {
            using (IDbConnection db = new SqlConnection(_sqlConnectionString)) {
                db.Open();
                //string sql = $"select * from {entityName}";
                //IEnumerable<TEntity> result = await db.QueryAsync<TEntity>(sql);
                //return result;
                return await db.GetAllAsync<TEntity>();
            }
        }

        public async Task<TEntity> InsertAsync(TEntity entity) {
            using (IDbConnection db = new SqlConnection(_sqlConnectionString)) {
                db.Open();
                // start a transaction in case something goes wrong
                await db.ExecuteAsync("begin transaction");
                try {
                    // Get the primary key property
                    var prop = entityType.GetProperty(primaryKeyName);

                    // int key?
                    if (primaryKeyType == "Int32") {
                        // not an identity?
                        if (PKNotIdentity == true) {
                            // get the highest value
                            var sql = $"select max({primaryKeyName}) from {entityName}";
                            // and add 1 to it
                            var Id = Convert.ToInt32(db.ExecuteScalar(sql)) + 1;
                            // update the entity
                            prop.SetValue(entity, Id);
                            // do the insert
                            db.Insert<TEntity>(entity);
                        }
                        else {
                            // key will be created by the database
                            var Id = (int)db.Insert<TEntity>(entity);
                            // set the value
                            prop.SetValue(entity, Id);
                        }
                    }
                    else if (primaryKeyType == "String") {
                        // string primary key. Use my helper
                        string sql = DapperSqlHelper.GetDapperInsertStatement(entity, entityName);
                        await db.ExecuteAsync(sql, entity);
                    }
                    // if we got here, we're good!
                    await db.ExecuteAsync("commit transaction");
                    return entity;
                }
                catch (Exception ex) {
                    var msg = ex.Message;
                    await db.ExecuteAsync("rollback transaction");
                    return null;
                }
            }
        }

        public async Task<TEntity> UpdateAsync(TEntity entity) {
            using (IDbConnection db = new SqlConnection(_sqlConnectionString)) {
                db.Open();
                try {
                    //string sql = DapperSqlHelper.GetDapperUpdateStatement(entity, entityName, primaryKeyName);
                    //await db.ExecuteAsync(sql, entity);
                    await db.UpdateAsync<TEntity>(entity);
                    return entity;
                }
                catch (Exception ex) {
                    return null;
                }
            }
        }

        //ItprMain MAIN
        public async Task<ItprMain> InsertAsync_Main(ItprMain main, string tableName) {
            using (IDbConnection db = new SqlConnection(_sqlConnectionString)) {
                db.Open();
                // start a transaction in case something goes wrong
                await db.ExecuteAsync("begin transaction");
                try {
                    // Get the primary key property
                    var id = 0;
                    var prop = entityType.GetProperty(primaryKeyName);

                    // int key?
                    if (primaryKeyType == "Int32") {
                        // not an identity?
                        if (PKNotIdentity == false) {
                            // get the highest value
                            var valSql = $"select COALESCE(max({primaryKeyName}),0) from {tableName}";
                            // and add 1 to it
                            id = Convert.ToInt32(db.ExecuteScalar(valSql)) + 1;
                            // update the entity
                            main.id = id;
                            // update the itpr ID
                            main.itprId = "ITPR" + main.budgetYear + "-" + id;
                        }
                    }

                    // key will be created by the database
                    string sql = DapperSqlHelper.GetDapperInsertStatement(main, tableName);

                    if (await db.ExecuteAsync(sql, main) > 0) {
                        // if we got here, we're good!
                        await db.ExecuteAsync("commit transaction");
                    }

                    return main;
                }
                catch (Exception ex) {
                    var msg = ex.Message;
                    await db.ExecuteAsync("rollback transaction");
                    return null;
                }
            }
        }
        public async Task<ItprMain> UpdateAsync_Main_Step1(ItprMain entity) {
            using (IDbConnection db = new SqlConnection(_sqlConnectionString)) {
                db.Open();
                try {
                   
                    var _parameters = new DynamicParameters();

                    _parameters.Add("id", entity.id);
                    _parameters.Add("budgetId", entity.budgetId);
                    _parameters.Add("warrantId", entity.warrantId);
                    _parameters.Add("projectName", entity.projectName);
                    _parameters.Add("entity", entity.entity);
                    _parameters.Add("capexAmount", entity.capexAmount);
                    _parameters.Add("opexAmount", entity.opexAmount);
                    _parameters.Add("totalAmount", entity.totalAmount);
                    _parameters.Add("statusId", entity.statusId);
                    _parameters.Add("updatedBy", "");
                    _parameters.Add("updatedDate", DateTime.Now);

                    int rows = await db.ExecuteAsync("[itpr].[update_main_step1]", _parameters, commandType: CommandType.StoredProcedure);

                    if (rows > 0)
                        return entity;
                    else
                        return null;
                    

                }
                catch (Exception ex) {
                    return null;
                }
            }
        }
        public async Task<ItprMain> UpdateAsync_Main_Step2(ItprMain main) {
            using (IDbConnection db = new SqlConnection(_sqlConnectionString)) {
                db.Open();
                try {

                    var _parameters = new DynamicParameters();

                    _parameters.Add("id", main.id);
                    int rows = await db.ExecuteAsync("[itpr].[update_main_step2]", _parameters, commandType: CommandType.StoredProcedure);

                    if (rows > 0)
                        return main;
                    else
                        return null;


                }
                catch (Exception ex) {
                    return null;
                }
            }
        }

        //VendorMain
        public async Task<vendorMain> InsertAsync_VendorMain(vendorMain main, string tableName) {
            using (IDbConnection db = new SqlConnection(_sqlConnectionString)) {
                db.Open();
                // start a transaction in case something goes wrong
                await db.ExecuteAsync("begin transaction");
                try {
                    // Get the primary key property
                    var id = 0;
                    var prop = entityType.GetProperty(primaryKeyName);

                    // int key?
                    if (primaryKeyType == "Int32") {
                        // not an identity?
                        if (PKNotIdentity == false) {
                            // get the highest value
                            var valSql = $"select COALESCE(max({primaryKeyName}),0) from {tableName}";
                            // and add 1 to it
                            id = Convert.ToInt32(db.ExecuteScalar(valSql)) + 1;
                            // update the entity
                            main.id = id;
                        }
                    }

                    // key will be created by the database
                    string sql = DapperSqlHelper.GetDapperInsertStatement(main, tableName);

                    if (await db.ExecuteAsync(sql, main) > 0) {
                        // if we got here, we're good!
                        await db.ExecuteAsync("commit transaction");
                    }

                    return main;
                }
                catch (Exception ex) {
                    var msg = ex.Message;
                    await db.ExecuteAsync("rollback transaction");
                    return null;
                }
            }
        }

        //get distinct by row
        public  IEnumerable<TSource> DistinctByImpl<TSource, TKey>(IEnumerable<TSource> source,Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer) {
            HashSet<TKey> knownKeys = new HashSet<TKey>(comparer);
            foreach (TSource element in source) {
                if (knownKeys.Add(keySelector(element))) {
                    yield return element;
                }
            }
        }


    }
}