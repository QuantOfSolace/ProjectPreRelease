using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfApp5
{
    public static class ClassChangePage
    {
        public static Frame frame1 = new Frame();
    }
    public class GlobalVar
    {
        public static string PanelLogin;
        public static bool ThemeNegr = false;
        public static bool FlagToAction = true;
        public static List<DateTime> selectionChangedInitTimes = new List<DateTime>();
        public static List<string> selectionChangedInitData = new List<string>();
        public static List<string> selectionChangedInitFirstName = new List<string>();
        public static List<string> selectionChangedInitSurrName = new List<string>();
        public static List<string> selectionChangedInitLastName = new List<string>();
        public static List<string> selectionChangedInitRootPass = new List<string>();
        public static bool StatusAuth;

        internal static string GetUserNameByLogin(string panelLogin)
        {
            throw new NotImplementedException();
        }
    }
    public class GlobalMethods
    {
        public static string GetUserNameAdmin(string login)
        {
            TESTEntities dataBase = new TESTEntities();
            // Получить объект пользователя из БД по login:
            admins user = dataBase.admins.FirstOrDefault(u => u.login == login);

            // Если у пользователя есть имя, то вернуть его:
            return user != null && !string.IsNullOrEmpty(user.root) ? user.root : string.Empty;
        }
        public static string GetUserNameUser(string login)
        {
            TESTEntities dataBase = new TESTEntities();
            // Получить объект пользователя из БД по login:
            users user = dataBase.users.FirstOrDefault(u => u.login == login);

            // Если у пользователя есть имя, то вернуть его:
            return user != null && !string.IsNullOrEmpty(user.root) ? user.root : string.Empty;
        }
        public static string GetName(string name)
        {
            TESTEntities dataBase = new TESTEntities();
            // Получить объект пользователя из БД по login:
            admins user = dataBase.admins.FirstOrDefault(u => u.login == name);

            // Если у пользователя есть имя, то вернуть его:
            return user != null && !string.IsNullOrEmpty(user.name) ? user.name : string.Empty;
        }
    }
}
