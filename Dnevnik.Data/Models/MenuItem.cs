namespace Dnevnik.Data.Models
{
    using System.Collections.Generic;

    public class MenuItem
    {
        public MenuItem(string title, string url, List<MenuItem> submenu = null)
        {
            this.Title = title;
            this.Url = url;
            this.Submenu = submenu;
        }

        public string Title { get; set; }

        public string Url { get; set; }

        public List<MenuItem> Submenu { get; set; } 
    }
}
