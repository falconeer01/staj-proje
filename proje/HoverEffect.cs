using System;
using System.Drawing;
using System.Windows.Forms;

namespace proje
{
    public static class ButtonHoverHelper
    {
        public static void AttachHoverEffect(Button button)
        {
            button.MouseEnter += (sender, e) =>
            {
                button.BackColor = Color.FromArgb(255, 250, 101);
            };

            button.MouseLeave += (sender, e) =>
            {
                button.BackColor = Color.FromArgb(255, 218, 0);
            };
        }
    }
}
