using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;
using System.IO;
using System.Windows.Forms;


namespace DataAccessLayer.Database
{
	public class InMemoryDatabase<T> : IDbWrapper<T> where T : DataEntity
	{
		private Dictionary<Tuple<string, string>, DataEntity> DatabaseInstance;

		public InMemoryDatabase()
		{
			DatabaseInstance = new Dictionary<Tuple<string, string>, DataEntity>();
            DatabaseInstance.Add(Tuple.Create("20", "1"), new Company { CompanyCode = "1", SiteId = "20", CompanyName = "testdelete" });
            DatabaseInstance.Add(Tuple.Create("20", "2"), new Company { CompanyCode = "2", SiteId = "20", CompanyName = "test123" });

            //DatabaseInstance.Add(Tuple.Create("20", "1"), new Employee { CompanyCode = "1", SiteId = "20", EmployeeCode = "1",EmployeeName = "testname" });
			//DatabaseInstance.Add(Tuple.Create("20", "2"), new Employee { CompanyCode = "2", SiteId = "20", EmployeeCode = "3", EmployeeName = "newemployee" });
		}

		public bool Insert(T data)
		{
			try
			{
				DatabaseInstance.Add(Tuple.Create(data.SiteId, data.CompanyCode), data);
				return true;
			}
			catch (Exception e)
			{
				LogErrorMessage(e);
				return false;
			}
		}

		public bool Update(T data)
		{
			try
			{
				if (DatabaseInstance.ContainsKey(Tuple.Create(data.SiteId, data.CompanyCode)))
				{
					DatabaseInstance.Remove(Tuple.Create(data.SiteId, data.CompanyCode));
					Insert(data);
					return true;
				}

				return false;
			}
			catch (Exception e)
			{
				LogErrorMessage(e);
				return false;
			}
		}

		public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
		{
			try
			{
				var entities = FindAll();
				return entities.Where(expression.Compile());
			}
			catch (Exception e)
			{
				LogErrorMessage(e);
				return Enumerable.Empty<T>();
			}
		}

		public IEnumerable<T> FindAll()
		{
			try
			{
				return DatabaseInstance.Values.OfType<T>();
			}
			catch (Exception e)
			{
				LogErrorMessage(e);
				return Enumerable.Empty<T>();
			}
		}

		public bool Delete(Expression<Func<T, bool>> expression)
		{
			try
			{
				var entities = FindAll();
				var entity = entities.Where(expression.Compile());
				foreach (var dataEntity in entity)
				{
					DatabaseInstance.Remove(Tuple.Create(dataEntity.SiteId, dataEntity.CompanyCode));
				}
				
				return true;
			}
			catch (Exception e)
			{
				LogErrorMessage(e);
				return false;
			}
		}

		public bool DeleteAll()
		{
			try
			{
				DatabaseInstance.Clear();
				return true;
			}
			catch(Exception e)
			{
				LogErrorMessage(e);
				return false;
			}
		}

		public bool UpdateAll(Expression<Func<T, bool>> filter, string fieldToUpdate, object newValue)
		{
			try
			{
				var entities = FindAll();
				var entity = entities.Where(filter.Compile());
				foreach (var dataEntity in entity)
				{
					var newEntity = UpdateProperty(dataEntity, fieldToUpdate, newValue);

					DatabaseInstance.Remove(Tuple.Create(dataEntity.SiteId, dataEntity.CompanyCode));
					Insert(newEntity);
				}

				return true;
			}
			catch(Exception e)
			{
				LogErrorMessage(e);
				return false;
			}
		}

		private T UpdateProperty(T dataEntity, string key, object value)
		{
			Type t = typeof(T);
			var prop = t.GetProperty(key, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

			if (prop == null)
			{
				throw new Exception("Property not found");
			}

			prop.SetValue(dataEntity, value, null);
			return dataEntity;
		}

		public Task<bool> InsertAsync(T data)
		{
			return Task.FromResult(Insert(data));
		}

		public Task<bool> UpdateAsync(T data)
		{
			return Task.FromResult(Update(data));
		}

		public Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression)
		{
			return Task.FromResult(Find(expression));
		}

		public Task<IEnumerable<T>> FindAllAsync()
		{
			return Task.FromResult(FindAll());
		}

		public Task<bool> DeleteAsync(Expression<Func<T, bool>> expression)
		{
			return Task.FromResult(Delete(expression));
		}

		public Task<bool> DeleteAllAsync()
		{
			return Task.FromResult(DeleteAll());
		}

		public Task<bool> UpdateAllAsync(Expression<Func<T, bool>> filter, string fieldToUpdate, object newValue)
		{
			return Task.FromResult(UpdateAll(filter, fieldToUpdate, newValue));
		}

		public void LogErrorMessage(Exception e)
        {
			if (!File.Exists("C:\\Temp\\exceptionlog.txt"))
				File.Create("C:\\Temp\\exceptionlog.txt");

			using (StreamWriter w = File.AppendText("C:\\Temp\\exceptionlog.txt"))
			{
				w.WriteLine("Exception: " + e.Message);
				w.WriteLine("Occurred at: " + DateTime.Now);
				w.WriteLine("Details:" + e.InnerException + Environment.NewLine);				
			}

			MessageBox.Show("Error Found. Please check log for details at C:\\Temp\\exceptionlog.txt.");
        }
	}
}
