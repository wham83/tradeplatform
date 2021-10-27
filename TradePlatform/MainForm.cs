using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using BaseLibrary;
using BaseLibrary.Strategy.Loader;

namespace App
{
    public partial class MainForm : Form
    {
        IStrategy currentStrategy;

        StrategyLoader loader = new StrategyLoader();

        TradeSystem tradeSystem = new TradeSystem();

        public MainForm()
        {
            InitializeComponent();
            FindAllStrategy();
        }

        private void FindAllStrategy()
        {
            var strategies = loader.GetAllStrategy();
            foreach (var current in strategies)
            {
                listBox1.Items.Add(current);
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            var selectedStrategy = listBox1.SelectedItem;
            if (null == selectedStrategy)
                return;


            tradeSystem.Start(selectedStrategy as IStrategy);
        }
    }


}
