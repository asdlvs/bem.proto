using System.Collections.Generic;

namespace Dnevnik.Data
{
    using System;

    using Dnevnik.Data.Models;

    public class Context
    {
        public Context(string login, string avatar, List<MenuItem> menu)
        {
            if (string.IsNullOrWhiteSpace(login)) { throw new ArgumentException("login"); }
            if (string.IsNullOrWhiteSpace(avatar)) { throw new ArgumentException("avatar"); }

            this.Login = login;
            this.Avatar = avatar;
            this.Menu = menu;
        }

        public Context()
            : this(
                "asd_lvs",
                "http://cs413920.vk.me/v413920230/8cad/jOeTCq6Iw7g.jpg",
                new List<MenuItem>
                {
                    new MenuItem("Мой дневник", "/", new List<MenuItem>
                                                           {
                                                              new MenuItem( "Профиль", "/prof"),
                                                              new MenuItem( "Сообщения", "/mes"),
                                                              new MenuItem( "Почта", "/mail"),
                                                              new MenuItem( "Календарь", "/cln"),
                                                           }),

                    new MenuItem("Школа", "/", new List<MenuItem>
                                                           {
                                                              new MenuItem( "Моя школа", "/sch"),
                                                              new MenuItem( "Мои классы", "/gr"),
                                                              new MenuItem( "Расписание", "/sch"),
                                                              new MenuItem( "Журнал", "/jour"),
                                                           }),
                })
        { }

        public string Login
        {
            get;
            private set;
        }

        public string Avatar
        {
            get;
            private set;
        }

        public List<MenuItem> Menu
        {
            get;
            private set;
        }
    }
}
