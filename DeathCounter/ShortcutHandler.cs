using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DeathCounter
{
    public class ShortcutHandler : Form
    {
        private System.Windows.Forms.Timer shortcutPoolingTimer = new System.Windows.Forms.Timer() { Interval = 16 };

        public ShortcutHandler()
        {
            shortcutPoolingTimer.Tick += new EventHandler(ShortcutPoolingTimerTick);

            shortcutPoolingTimer.Start();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            shortcutPoolingTimer.Stop();
            shortcutPoolingTimer.Dispose();
        }


        [DllImport("User32.dll")]
        private static extern short GetAsyncKeyState(int vKey);
        private Func<int, bool> isPressed = delegate (int key) { return ((GetAsyncKeyState(key) >> 15) & 0x0001) == 0x0001; };

        private static readonly int VK_PRIOR = 0x21; //This is the page-up key.
        private static readonly int VK_NEXT = 0x22; //This is the page-down key.
        private static readonly int VK_HOME = 0x24; //This is the home key.

        bool pgUpWasPressed = false;
        bool pgDnWasPressed = false;
        bool homeWasPressed = false;

        private void ShortcutPoolingTimerTick(object sender = null, EventArgs e = null)
        {
            var isPgUpPressed = isPressed(VK_PRIOR);
            if (pgUpWasPressed == true && isPgUpPressed == false)
            {
                OnAddDeathReleased();
            }
            pgUpWasPressed = isPgUpPressed;

            var isPgDnPressed = isPressed(VK_NEXT);
            if (pgDnWasPressed == true && isPgDnPressed == false)
            {
                OnRemoveDeathReleased();
            }
            pgDnWasPressed = isPgDnPressed;

            var isHomePressed = isPressed(VK_HOME);
            if (homeWasPressed == true && isHomePressed == false)
            {
                OnFocusOnFormReleased();
            }
            homeWasPressed = isHomePressed;
        }

        public virtual void OnAddDeathReleased()
        {
            throw new NotImplementedException();
        }

        public virtual void OnRemoveDeathReleased()
        {
            throw new NotImplementedException();
        }

        public virtual void OnFocusOnFormReleased()
        {
            throw new NotImplementedException();
        }
    }
}
