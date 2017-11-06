using Npgsql;
using NpgsqlTypes;
using SpaDatasource.Entitites;
using SpaDatasource.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaDatasource.Implementations
{
    public class SpaDatasource : ISpaDatasource, IDisposable
    {
        #region Атрибуты

        private bool _IsAlreadyDisposed = false;

        private string _ConnectionString;
        private NpgsqlConnection _Conn;

        #endregion

        #region Инициализация и освобождение ресурсов

        public SpaDatasource(string connString)
        {
            _ConnectionString = connString;
        }

        public void Dispose()
        {
            Dispose(true);  
            GC.SuppressFinalize(this);  
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if (_IsAlreadyDisposed)
                return;

            if (isDisposing)
            {
                // освобождаем управляемые ресурсы тут
                Close();
            } 

            // если есть неуправляемые ресурсы - то их нужно совободить тут

            _IsAlreadyDisposed = true;
        }

        #endregion

        #region Открытие и закрытие соединения с БД

        public void Open()
        {
            if (_IsAlreadyDisposed)
                throw new ObjectDisposedException("MyDbContext", "Called [Open] method on disposed object.");

            if(_Conn != null)
            {
                if(_Conn.State != System.Data.ConnectionState.Closed && _Conn.State != System.Data.ConnectionState.Broken)
                    throw new InvalidOperationException("Called [Open] on already opened connection.");
            }

            _Conn = new NpgsqlConnection(_ConnectionString);
            _Conn.Open();
        }

        public void Close()
        {
            if(_Conn == null)
                return;

            if(_Conn.State == System.Data.ConnectionState.Closed || _Conn.State == System.Data.ConnectionState.Broken)
                return;

            _Conn.Close();
        }

        #endregion

        #region Работа с пользователями

        public User FindUserById(int id)
        {
            CheckConnValidity("FindUserById");

            string sql = "SELECT id, login, password_hash, full_name FROM users WHERE id = :id";
            NpgsqlCommand command = CreateCommandWithTimeout(sql, _Conn);
            command.Parameters.Add(new NpgsqlParameter("id", NpgsqlDbType.Integer));
            command.Parameters[0].Value = id;

            return GetUserFromCommand(command);
        }

        public User FindUserByName(string name)
        {
            CheckConnValidity("FindUserByName");

            string sql = "SELECT id, login, password_hash, full_name FROM users WHERE login = :login";
            NpgsqlCommand command = CreateCommandWithTimeout(sql, _Conn);
            command.Parameters.Add(new NpgsqlParameter("login", NpgsqlDbType.Text));
            command.Parameters[0].Value = name;

            return GetUserFromCommand(command);
        }

        public void InsertUser(User user)
        {
            CheckConnValidity("InsertUser");

            string sql = "INSERT INTO users (login, password_hash, full_name) VALUES (:login, :password_hash, :full_name) RETURNING id";
            NpgsqlCommand command = CreateCommandWithTimeout(sql, _Conn);

            command.Parameters.Add(new NpgsqlParameter("login", NpgsqlDbType.Text));
            command.Parameters[0].Value = user.Login;

            command.Parameters.Add(new NpgsqlParameter("password_hash", NpgsqlDbType.Text));
            command.Parameters[1].Value = user.PasswordHash;

            command.Parameters.Add(new NpgsqlParameter("full_name", NpgsqlDbType.Text));
            command.Parameters[2].Value = user.FullName;

            long id = (long)command.ExecuteScalar();

            user.Id = id;
        }

        public void UpdateUser(User user)
        {
            CheckConnValidity("UpdateUser");

            string sql = "UPDATE users SET login = :login, password_hash = :password_hash, full_name = :full_name";
            NpgsqlCommand command = CreateCommandWithTimeout(sql, _Conn);

            command.Parameters.Add(new NpgsqlParameter("id", NpgsqlDbType.Integer));
            command.Parameters[0].Value = user.Id;

            command.Parameters.Add(new NpgsqlParameter("login", NpgsqlDbType.Text));
            command.Parameters[0].Value = user.Login;

            command.Parameters.Add(new NpgsqlParameter("password_hash", NpgsqlDbType.Text));
            command.Parameters[1].Value = user.PasswordHash;

            command.Parameters.Add(new NpgsqlParameter("full_name", NpgsqlDbType.Text));
            command.Parameters[2].Value = user.FullName;

            command.ExecuteNonQuery();
        }

        public IEnumerable<User> Users()
        {
            CheckConnValidity("Users");

            List<User> list = new List<User>();

            string sql = "SELECT id, login, password_hash, full_name FROM users";
            NpgsqlCommand command = CreateCommandWithTimeout(sql, _Conn);

            NpgsqlDataReader dr = command.ExecuteReader();
            while(dr.Read())
            {
                User u = GetUserFromDataReader(dr);
                list.Add(u);
            }

            dr.Close();

            return list;
        }

        #endregion

        #region Работа с заказми

        public IEnumerable<Order> Orders()
        {
            CheckConnValidity("Orders");

            List<Order> list = new List<Order>();

            string sql = "SELECT id, \"time\", user_id, email, currency, description FROM orders";
            NpgsqlCommand command = CreateCommandWithTimeout(sql, _Conn);

            NpgsqlDataReader dr = command.ExecuteReader();
            while(dr.Read())
            {
                Order u = GetOrderFromDataReader(dr);
                list.Add(u);
            }

            dr.Close();

            return list;
        }

        public IEnumerable<Order> FindOrdersForUser(int userId)
        {
            CheckConnValidity("FindOrdersForUser");

            List<Order> list = new List<Order>();

            string sql = "SELECT id, \"time\", user_id, email, currency, description FROM orders WHERE user_id = :user_id";
            NpgsqlCommand command = CreateCommandWithTimeout(sql, _Conn);

            command.Parameters.Add(new NpgsqlParameter("user_id", NpgsqlDbType.Integer));
            command.Parameters[0].Value = userId;

            NpgsqlDataReader dr = command.ExecuteReader();
            while(dr.Read())
            {
                Order u = GetOrderFromDataReader(dr);
                list.Add(u);
            }

            dr.Close();

            return list;
        }

        public Order FindOrderById(int id)
        {
            CheckConnValidity("FindOrderById");

            string sql = "SELECT id, \"time\", user_id, email, currency, description FROM orders WHERE id = :id";
            NpgsqlCommand command = CreateCommandWithTimeout(sql, _Conn);

            command.Parameters.Add(new NpgsqlParameter("id", NpgsqlDbType.Integer));
            command.Parameters[0].Value = id;

            return GetOrderFromCommand(command);
        }

        public void InsertOrder(Order order)
        {
            CheckConnValidity("InsertOrder");

            string sql = "INSERT INTO orders (\"time\", user_id, email, currency, description) VALUES (:time, :user_id, :email, :currency, :description) RETURNING id";
            NpgsqlCommand command = CreateCommandWithTimeout(sql, _Conn);

            command.Parameters.Add(new NpgsqlParameter("time", NpgsqlDbType.TimeTZ));
            command.Parameters[0].Value = order.Time;

            command.Parameters.Add(new NpgsqlParameter("user_id", NpgsqlDbType.Integer));
            command.Parameters[1].Value = order.UserId;

            command.Parameters.Add(new NpgsqlParameter("email", NpgsqlDbType.Text));
            command.Parameters[2].Value = order.EmailAddress;

            command.Parameters.Add(new NpgsqlParameter("currency", NpgsqlDbType.Numeric));
            command.Parameters[3].Value = order.Currency;

            command.Parameters.Add(new NpgsqlParameter("description", NpgsqlDbType.Text));
            command.Parameters[4].Value = order.OrderDescription;

            long id = (long)command.ExecuteScalar();

            order.Id = id;
        }

        #endregion

        #region Вспомогательные классы

        private void CheckConnValidity(string methodName)
        { 
            if (_IsAlreadyDisposed)
                throw new ObjectDisposedException("MyDbContext", "Called [" + methodName + "] method on disposed object.");

            if(_Conn == null)
                throw new InvalidOperationException("Called [" + methodName + "] method on null connection object.");

            if(_Conn.State == System.Data.ConnectionState.Closed) 
                throw new InvalidOperationException("Called [" + methodName + "] method on closed connection object.");

            if(_Conn.State == System.Data.ConnectionState.Broken)
                throw new InvalidOperationException("Called [" + methodName + "] method on broken connection object.");
        }

        private NpgsqlCommand CreateCommandWithTimeout(string cmdText, NpgsqlConnection conn, int Timeout = 30)
        {
            NpgsqlCommand command = new NpgsqlCommand(cmdText, conn)
            {
                CommandTimeout = Timeout
            };

            return command;
        }

        private User GetUserFromCommand(NpgsqlCommand command)
        {
            User user = null;

            NpgsqlDataReader dr = command.ExecuteReader();
            if (dr.Read())
            {
                user = GetUserFromDataReader(dr);
            }

            dr.Close();

            return user;
        }

        private User GetUserFromDataReader(NpgsqlDataReader dr)
        {
            return new User
                {
                    Id             = dr.GetInt64(0),
                    Login          = dr.GetString(1), 
                    PasswordHash   = dr.GetString(2),
                    FullName       = dr.IsDBNull(3) ? null : dr.GetString(3)
                };
        }

        private Order GetOrderFromCommand(NpgsqlCommand command)
        {
            Order order = null;

            NpgsqlDataReader dr = command.ExecuteReader();
            if (dr.Read())
            {
                order = GetOrderFromDataReader(dr);
            }

            dr.Close();

            return order;
        }

        private Order GetOrderFromDataReader(NpgsqlDataReader dr)
        {
            return new Order
                {
                    Id                  = dr.GetInt64(0),
                    Time                = dr.GetDateTime(1), 
                    UserId              = dr.GetInt32(2),
                    EmailAddress        = dr.GetString(3),
                    Currency            = dr.GetDecimal(4),
                    OrderDescription    = dr.IsDBNull(5) ? null : dr.GetString(5)
                };
        }

        #endregion

    }
}
