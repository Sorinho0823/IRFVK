using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorldsHardestGame;

namespace week10
{
    public partial class Form1 : Form
    {
        GameController gc = new GameController();
        GameArea ga;

        int populationSize = 100;
        int nbrOfSteps = 10;
        int nbrOfStepsIncrement = 10;
        int generation = 1;
        public Form1()
        {
            InitializeComponent();
            ga = gc.ActivateDisplay();  //a pálya betoltese
            this.Controls.Add(ga);      //megjelenitese
            //gc.AddPlayer();
            //gc.Start(true);
            gc.GameOver += Gc_GameOver;

            for (int i = 0; i < populationSize; i++)            //populáció létrehozása
            {
                gc.AddPlayer(nbrOfSteps);
            }
            gc.Start(); //start             
            //ide kellenek a fittek?
        }

        private void Gc_GameOver(object sender)
        {
            generation++;
            label1.Text = string.Format(
                "{0}. generáció",
                generation);
            //
            var playerList = from p in gc.GetCurrentPlayers()
                             orderby p.GetFitness() descending
                             select p;
            var topPerformers = playerList.Take(populationSize / 2).ToList();


        }
    }
}
