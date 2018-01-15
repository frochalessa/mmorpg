using GeonBit.UI.Entities;

namespace Client
{
    class MenuManager
    {
        public static Menu menu;

        public enum Menu
        {
            Login,
            Register,
            InGame
        }

        public static void ChangeMenu(Menu menu)
        {
            foreach (Panel window in Gui.Windows)
            {
                window.Visible = false;
            }

            Gui.Windows[(int)menu].Visible = true;
        }
    }
}
