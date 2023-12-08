using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Week6.Abstraction;
using Week6.Entities;

namespace Week6
{
    public partial class Form1 : Form
    {
        private List<toy> _toys = new List<toy>();

        private IToyFactory _factory;

        public IToyFactory Factory
        {
            get { return _factory; }
            set { _factory = value; }
        }



        public Form1()
        {
            InitializeComponent();
            Factory = new CarFactory();
        }

        private void CreateTimer_Tick(object sender, EventArgs e)
        {
            var Toy = Factory.CreateNew();
            _toys.Add(Toy);
            Toy.Left = -Toy.Width;
            MainPanel.Controls.Add(Toy);
        }

        private void ConveyorTimer_Tick(object sender, EventArgs e)
        {
            var maxPosition = 0;
            foreach (var Toy in _toys)
            {
                Toy.MoveToy();
                if (Toy.Left > maxPosition)
                    maxPosition = Toy.Left;
            }

            if (maxPosition > 1000)
            {
                var oldestToy = _toys[0];
                MainPanel.Controls.Remove(oldestToy);
                _toys.Remove(oldestToy);
            }

        }
    }
}
