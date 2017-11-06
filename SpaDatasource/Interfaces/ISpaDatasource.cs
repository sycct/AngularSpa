using SpaDatasource.Entitites;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaDatasource.Interfaces
{
    public interface ISpaDatasource
    {
        #region API : Открытие и закрытие соединения

        void Open();
        void Close();

        #endregion

        #region API :  Работа с пользователями

        IEnumerable<User> Users();
        User FindUserById(int id);
        User FindUserByName(string name);
        void InsertUser(User user);
        void UpdateUser(User user);

        #endregion

        #region API :  Работа с заказами

        IEnumerable<Order> Orders();
        Order FindOrderById(int id);
        IEnumerable<Order> FindOrdersForUser(int userId);
        void InsertOrder(Order order);

        #endregion

    }
}
