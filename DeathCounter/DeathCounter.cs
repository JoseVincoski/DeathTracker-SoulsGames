using DeathCounter;
using DeathCounter.Properties;
using System.Xml.Linq;

namespace DeathCounterApp
{
    public partial class DeathCounter : ShortcutHandler
    {
        private FileHandler _deathLogFileHandler;
        private FileHandler _areaNameFileHandler;

        private string filePath = "DeathCounterLogFiles";
        private string deathLogFileName = "DeathLog.txt";
        private string areaNameFileName = "AreaNames.txt";

        private int totalDeathCount = 0;
        private int totalAreaDeathCount = 0;
        private string selectedArea = string.Empty;

        private readonly string REASON_ADDING_DEATH = "Adding Death";
        private readonly string REASON_REMOVING_DEATH = "Removing Death";
        private readonly string REASON_CHANGING_AREA_NAME = "Changing Area";

        private AutoCompleteStringCollection autoCompleteStringCollection = new AutoCompleteStringCollection();

        public DeathCounter()
        {
            InitializeComponent();
            _deathLogFileHandler = new FileHandler($"{Application.StartupPath}{filePath}/{deathLogFileName}");
            _deathLogFileHandler.Create();

            _areaNameFileHandler = new FileHandler($"{Application.StartupPath}{filePath}/{areaNameFileName}");
            if (!_areaNameFileHandler.Exists()) SeedAreaNameFile();

            this.Location = Screen.AllScreens[1].WorkingArea.Location;

            selectedArea = _deathLogFileHandler.ReadAllLines().LastOrDefault()?.Split('|')?[1]?.Trim();
            if (string.IsNullOrWhiteSpace(selectedArea)) selectedArea = "No area selected";

            int.TryParse(_deathLogFileHandler.ReadAllLines().LastOrDefault()?.Split('|')?[2]?.Trim(), out totalAreaDeathCount);
            int.TryParse(_deathLogFileHandler.ReadAllLines().LastOrDefault()?.Split('|')?[3]?.Trim(), out totalDeathCount);

            UpdateCbSource();
            UpdateDeaths();
            cb_areaName.Enabled = false;
        }

        private void setAreaName(object sender, EventArgs e)
        {
            if (cb_areaName.Enabled == false)
            {
                cb_areaName.Enabled = true;
                cb_areaName.Focus();
            }
            else
            {
                selectedArea = cb_areaName.Text;

                int.TryParse(_deathLogFileHandler.ReadAllLines()?.LastOrDefault(x => x.Split('|')[1].Trim() == selectedArea)?.Split('|')[2]?.Trim(), out totalAreaDeathCount);
                _deathLogFileHandler.WriteLine($"{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")} | {selectedArea} | {totalAreaDeathCount} | {totalDeathCount} | {REASON_CHANGING_AREA_NAME}");

                if (_areaNameFileHandler.ReadAllLines().FirstOrDefault(x => x == selectedArea) == null) { 
                    _areaNameFileHandler.WriteLine(selectedArea);
                    UpdateCbSource();
                }

                cb_areaName.Enabled = false;
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

        private void UpdateCbSource()
        {
            autoCompleteStringCollection = new AutoCompleteStringCollection();
            autoCompleteStringCollection.AddRange(_areaNameFileHandler.ReadAllLines().ToArray());
            cb_areaName.AutoCompleteCustomSource = autoCompleteStringCollection;
            cb_areaName.Text = selectedArea;
            cb_areaName.Update();
        }

        private void SeedAreaNameFile()
        {
            _areaNameFileHandler.WriteLine(Resources.AreaNames);
        }

        public override void OnAddDeathReleased() { AddDeath(); }

        public override void OnRemoveDeathReleased() { RemoveDeath(); }

        public override void OnFocusOnFormReleased() { this.Activate(); }
    }
}
