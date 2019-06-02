using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LawPortal.Types.Common
{
    public class OrdinalDbContext<T> : DbContext where T : Entity, new()
    {
        public DbSet<T> Instance { get; set; }
        public string Schema { get; set; }

        public OrdinalDbContext(string schema = Constants.DefaultSchema) : base()
        {
            // todo
            // Constants.DefaultContextName
            Schema = schema;
        }

        // todo
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.HasDefaultSchema(Schema);
        //    modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        //}

        public GenericResponse<long> Insert(T contract)
        {
            var returnObject = new GenericResponse<long>();

            try
            {
                var list = Instance.Add(contract);
                this.SaveChanges();
            }
            catch (Exception ex)
            {
                LogManager.Log(typeof(T).Name, "Insert", ex);
                returnObject.Messages.Add(new Message() { Code = ex.HResult.ToString(), Text = ex.Message });
            }

            returnObject.Value = GetIdValue(contract);
            return returnObject;
        }

        public GenericResponse<List<T>> Select()
        {
            var returnObject = new GenericResponse<List<T>>();

            try
            {
                returnObject.Value = Instance.ToList();
            }
            catch (Exception ex)
            {
                LogManager.Log(typeof(T).Name, "Select", ex);
                returnObject.Messages.Add(new Message() { Code = ex.HResult.ToString(), Text = ex.Message });
            }

            return returnObject;
        }

        public GenericResponse<T> SelectByKey(long id)
        {
            var returnObject = new GenericResponse<T>();

            try
            {
                returnObject.Value = Instance.FirstOrDefault(x => x.Id == id);
            }
            catch (Exception ex)
            {
                LogManager.Log(typeof(T).Name, "SelectByKey", ex);
                returnObject.Messages.Add(new Message() { Code = ex.HResult.ToString(), Text = ex.Message });
            }

            return returnObject;
        }

        public GenericResponse<List<T>> SelectByFilter(Expression<Func<T, bool>> selector)
        {
            var returnObject = new GenericResponse<List<T>>();

            try
            {
                returnObject.Value = Instance.Where(selector).ToList();
            }
            catch (Exception ex)
            {
                LogManager.Log(typeof(T).Name, "SelectByFilter", ex);
                returnObject.Messages.Add(new Message() { Code = ex.HResult.ToString(), Text = ex.Message });
            }

            return returnObject;
        }

        // todo
        //public GenericBaseResponse<long> Update(DbPropertyValues values, long id)
        //{
        //    var returnObject = new GenericBaseResponse<long>();
        //    var original = Instance.FirstOrDefault(x => x.Id == id);
        //    if (original != null)
        //    {
        //        Entry(original).CurrentValues.SetValues(values);
        //        SaveChanges();
        //    }
        //    return returnObject;
        //}

        public GenericResponse<long> Update(T contract, long id)
        {
            var returnObject = new GenericResponse<long>();

            contract.Id = id;
            var original = Instance.FirstOrDefault(x => x.Id == id);
            if (original != null)
            {
                Entry(original).CurrentValues.SetValues(contract);
                SaveChanges();
            }

            return returnObject;
        }

        public GenericResponse<T> Delete(long id)
        {
            var returnObject = new GenericResponse<T>();

            try
            {
                returnObject.Value = Instance.FirstOrDefault(x => x.Id == id);
                Instance.Remove(returnObject.Value);
                SaveChanges();
            }
            catch (Exception ex)
            {
                LogManager.Log(typeof(T).Name, "Delete", ex);
                returnObject.Messages.Add(new Message() { Code = ex.HResult.ToString(), Text = ex.Message });
            }

            return returnObject;
        }

        public GenericResponse<List<T>> SelectTop<TKey>(Expression<Func<T, bool>> selector, int count, Expression<Func<T, TKey>> keySelector)
        {
            var returnObject = new GenericResponse<List<T>>();

            try
            {
                if (selector == null)
                {
                    returnObject.Value = Instance.OrderBy(keySelector).Take(count).ToList();
                }
                else
                {
                    returnObject.Value = Instance.Where(selector).OrderBy(keySelector).Take(count).ToList();
                }
            }
            catch (Exception ex)
            {
                LogManager.Log(typeof(T).Name, "SelectTop", ex);
                returnObject.Messages.Add(new Message() { Code = ex.HResult.ToString(), Text = ex.Message });
            }

            return returnObject;
        }

        public GenericResponse<List<T>> SelectTopDesc<TKey>(Expression<Func<T, bool>> selector, int count, Expression<Func<T, TKey>> keySelector)
        {
            var returnObject = new GenericResponse<List<T>>();

            try
            {
                if (selector == null)
                {
                    returnObject.Value = Instance.OrderByDescending(keySelector).Take(count).ToList();
                }
                else
                {
                    returnObject.Value = Instance.Where(selector).OrderByDescending(keySelector).Take(count).ToList();
                }
            }
            catch (Exception ex)
            {
                LogManager.Log(typeof(T).Name, "SelectTop", ex);
                returnObject.Messages.Add(new Message() { Code = ex.HResult.ToString(), Text = ex.Message });
            }

            return returnObject;
        }

        public GenericResponse<long> Count(Expression<Func<T, bool>> selector)
        {
            var returnObject = new GenericResponse<long>();

            try
            {
                if (selector == null)
                {
                    returnObject.Value = Instance.Count();
                }
                else
                {
                    returnObject.Value = Instance.Count(selector);
                }
            }
            catch (Exception ex)
            {
                LogManager.Log(typeof(T).Name, "SelectTop", ex);
                returnObject.Messages.Add(new Message() { Code = ex.HResult.ToString(), Text = ex.Message });
            }

            return returnObject;
        }

        private long GetIdValue(T contract)
        {
            return contract.Id;
        }

        private void SetIdValue(T contract, long value)
        {
            contract.Id = value;
        }
    }
}
