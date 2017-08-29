using SQLite;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using ChatDemo.Models;

namespace ChatDemo.Data
{
    public static class Repository
    {
        const string DatabaseName = "Chat-List.db1";
        public static readonly string DBPath;

        static SQLiteConnection _connection;
        static SQLiteConnection DBConnection
        {
            get
            {
                if (_connection == null)
                    _connection = new SQLiteConnection(DBPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache);
                return _connection;
            }
        }

        static Repository()
        {
            DBPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), DatabaseName);
            InitializeDatabase();
        }

        #region Private Methods

        static void InitializeDatabase()
        {
           DBConnection.CreateTable<User>();
        DBConnection.CreateTable<UserMessage>();

     

        }

        private static void SetDefaultValues<T>(T item) where T : IEntity
        {
            if (item.Id == 0)
                item.CreatedOn = DateTime.UtcNow;
            item.UpdatedOn = DateTime.UtcNow;
        }

        #endregion

        public static void ClearDatabasse()
        {
            lock (DBConnection)
            {
                DBConnection.DeleteAll<User>();
            }
        }

        #region Generic Methods

        public static TableQuery<T> Table<T>() where T : new()
        {
            return DBConnection.Table<T>();
        }

      

        /// <summary>
        /// Save or update item by Id
        /// </summary>
        /// <typeparam name="T">item type</typeparam>
        /// <param name="item">Item to sav eor update</param>
        public static void SaveOrUpdate<T>(T item) where T : IEntity
        {
            lock (_connection)
                SaveOrUpdate(new List<T> { item });
        }

        /// <summary>
        /// Save or update items by Id
        /// </summary>
        /// <typeparam name="T">item type</typeparam>
        /// <param name="items">Items to save or update</param>
        public static void SaveOrUpdate<T>(List<T> items) where T : IEntity
        {
            lock (_connection)
            {
                items.ForEach(x => SetDefaultValues(x));
                DBConnection.InsertAll(items.Where(x => x.Id == 0));
                DBConnection.UpdateAll(items.Where(x => x.Id > 0));
            }
        }

        public static void InsertOrReplace<T>(List<T> items) where T : IEntity
        {
            lock (_connection)
            {
                items.ForEach(item =>
            {
                SetDefaultValues(item);
                DBConnection.InsertOrReplace(item, typeof(T));
            });
            }
        }

        public static void Delete<T>(T item) where T : IEntity
        {
            lock (_connection)
            {
                DBConnection.Delete(item);
            }
        }

        public static IQueryable<T> AsQuerable<T>(T item) where T : IEntity, new()
        {
            lock (_connection)
            {
                return DBConnection.Table<T>().AsQueryable();
            }
        }

        /// <summary>
        /// Execute query in database and return result
        /// </summary>
        /// <typeparam name="T">type of record</typeparam>
        /// <returns></returns>
        public static List<T> Find<T>() where T : new()
        {
            lock (_connection)
            {
                return Find<T>(null);
            }
        }

        /// <summary>
        /// Execute query in database and return result
        /// </summary>
        /// <typeparam name="T">type of record</typeparam>
        /// <param name="query">query to filter the records</param>
        /// <returns></returns>
        public static List<T> Find<T>(Func<T, bool> query) where T : new()
        {
            lock (_connection)
            {
                if (query == null)
                    return DBConnection.Table<T>().ToList();
                return DBConnection.Table<T>().Where(query).ToList();
            }
        }

        /// <summary>
        /// Execute query in database and return result
        /// </summary>
        /// <typeparam name="T">type of record</typeparam>
        /// <param name="query">query to filter the records</param>
        /// <returns></returns>
        public static List<T> Find<T>(Func<T, bool> query, int pageNumber, int limit) where T : new()
        {
            pageNumber = pageNumber < 1 ? 0 : pageNumber - 1;
            lock (_connection)
            {
                if (query == null)
                    return DBConnection.Table<T>().Skip(pageNumber * limit).Take(limit).ToList();
                return DBConnection.Table<T>().Where(query).Skip(pageNumber * limit).Take(limit).ToList();
            }
        }


        /// <summary>
        /// Execute query in database and return first result
        /// </summary>
        /// <typeparam name="T">type of record</typeparam>
        /// <param name="query">query to filter the record</param>
        /// <returns></returns>
        public static T FindOne<T>(Func<T, bool> query) where T : new()
        {
            lock (_connection)
            {
                if (query == null)
                    return DBConnection.Table<T>().FirstOrDefault();
                return DBConnection.Table<T>().FirstOrDefault(query);
            }
        }

        ///// <summary>
        ///// check for given primary key return result
        ///// </summary>
        ///// <typeparam name="T">type of record</typeparam>
        ///// <param name="id">primary key</param>
        ///// <returns></returns>
        //public static T FindOne<T>(int id) where T : new()
        //{
        //    return DBConnection.Find<T>(id);
        //}

        public static bool Exists<T>(Func<T, bool> query) where T : new()
        {
            lock (_connection)
            {
                return DBConnection.Table<T>().Any(query);
            }
        }

        public static int Execute(string query, params object[] args)
        {
            lock (_connection)
            {
                return DBConnection.Execute(query, args);
            }
        }


        public static int Count<T>(Func<T, bool> query) where T : new()
        {
            lock (_connection)
            {
                return DBConnection.Table<T>().Count(query);
            }
        }

        public static void Dispose()
        {
            if (_connection != null)
            {
                _connection.Dispose();
                _connection = null;
            }
        }

        #endregion


        public static bool IsEqual<T>(IEntity item1, IEntity item2)
        {
            foreach (var prop in typeof(T).GetProperties())
            {
                if (prop.Name == "DbId" || prop.Name == "CreatedOn")
                    continue;
                if (prop.GetValue(item1) != prop.GetValue(item2))
                    return true;
            }
            return false;
        }

        public static bool CopyValues<T>(T source, T target, params string[] properties) where T : IEntity
        {
            var props = typeof(T).GetProperties();
            if (properties != null && properties.Length > 0)
            {
                props = props.Where(x => properties.Contains(x.Name)).ToArray();
            }
            foreach (var prop in props)
            {
                if (prop.PropertyType.IsArray || prop.PropertyType.IsGenericType)
                    continue;
                if (prop.CanWrite)
                {
                    try
                    {
                        prop.SetValue(target, prop.GetValue(source));
                    }
                    catch { }
                }
            }
            return false;
        }


    }
}
