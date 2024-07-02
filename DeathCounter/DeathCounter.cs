using System.Diagnostics;
using System.Runtime.InteropServices;

namespace WinFormsApp1
{
    public partial class DeathCounter : Form
    {
        private FileHandler _deathLogFileHandler;
        private string filePath = "DeathLog.txt";

        private int totalDeathCount = 0;
        private int totalAreaDeathCount = 0;

        private readonly string REASON_ADDING_DEATH = "Adding Death";
        private readonly string REASON_REMOVING_DEATH = "Removing Death";
        private readonly string REASON_CHANGING_AREA_NAME = "Changing Area";

        private string selectedArea = string.Empty;

        public DeathCounter()
        {
            InitializeComponent();
            _deathLogFileHandler = new FileHandler($"{Application.StartupPath}{filePath}");
            _deathLogFileHandler.Create();
            this.Location = Screen.AllScreens[1].WorkingArea.Location;

            selectedArea = _deathLogFileHandler.ReadAllLines().LastOrDefault()?.Split('|')?[1]?.Trim();
            if (string.IsNullOrWhiteSpace(selectedArea)) selectedArea = "No area selected";

            tb_AreaName.Text = selectedArea;
            int.TryParse(_deathLogFileHandler.ReadAllLines().LastOrDefault()?.Split('|')?[2]?.Trim(), out totalAreaDeathCount);
            int.TryParse(_deathLogFileHandler.ReadAllLines().LastOrDefault()?.Split('|')?[3]?.Trim(), out totalDeathCount);

            UpdateDeaths();
        }

        private void setAreaName(object sender, EventArgs e)
        {
            if (tb_AreaName.Enabled == false)
            {
                tb_AreaName.Enabled = true;
                tb_AreaName.Focus();
            }
            else
            {
                selectedArea = tb_AreaName.Text;

                int.TryParse(_deathLogFileHandler.ReadAllLines()?.LastOrDefault(x => x.Split('|')[1].Trim() == selectedArea)?.Split('|')[2]?.Trim(), out totalAreaDeathCount);
                _deathLogFileHandler.WriteLine($"{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")} | {selectedArea} | {totalAreaDeathCount} | {totalDeathCount} | {REASON_CHANGING_AREA_NAME}");

                tb_AreaName.Enabled = false;
                btn_cgAreaName.Focus();
                UpdateDeaths();
            }
        }

        private void onTextBoxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) setAreaName(sender, e);
        }

        private void AddDeath(object sender = null, EventArgs e = null)
        {
            totalAreaDeathCount++;
            totalDeathCount++;
            _deathLogFileHandler.WriteLine($"{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")} | {selectedArea} | {totalAreaDeathCount} | {totalDeathCount} | {REASON_ADDING_DEATH}");
            UpdateDeaths();
        }

        private void RemoveDeath(object sender = null, EventArgs e = null)
        {
            if (totalAreaDeathCount > 0) totalAreaDeathCount--;
            if (totalDeathCount > 0) totalDeathCount--;
            _deathLogFileHandler.WriteLine($"{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")} | {selectedArea} | {totalAreaDeathCount} | {totalDeathCount} | {REASON_REMOVING_DEATH}");
            UpdateDeaths();
        }

        private void UpdateDeaths()
        {
            lbl_AreaDeaths.Text = totalAreaDeathCount.ToString();
            lbl_totalDeaths.Text = totalDeathCount.ToString();
        }

        private void FocusOnForm()
        {
            this.Activate();
        }

        #region ShortcutHelper
        [DllImport("User32.dll")]
        private static extern short GetAsyncKeyState(int vKey);
        private Func<int, bool> isPressed = delegate (int key) { return ((GetAsyncKeyState(key) >> 15) & 0x0001) == 0x0001; };

        private static readonly int VK_PRIOR = 0x21; //This is the page-up key.
        private static readonly int VK_NEXT = 0x22; //This is the page-down key.
        private static readonly int VK_HOME = 0x24; //This is the home key.

        bool pgUpWasPressed = false;
        bool pgDnWasPressed = false;
        bool homeWasPressed = false;

        private void ShortcutPoolingTimerTick(object sender, EventArgs e)
        {
            var isPgUpPressed = isPressed(VK_PRIOR);
            if (pgUpWasPressed == true && isPgUpPressed == false)
            {
                AddDeath();
            }
            pgUpWasPressed = isPgUpPressed;

            var isPgDnPressed = isPressed(VK_NEXT);
            if (pgDnWasPressed == true && isPgDnPressed == false)
            {
                RemoveDeath();
            }
            pgDnWasPressed = isPgDnPressed;

            var isHomePressed = isPressed(VK_HOME);
            if (homeWasPressed == true && isHomePressed == false)
            {
                FocusOnForm();
            }
            homeWasPressed = isHomePressed;
        }

#endregion
    }
}
