using Mikroszim.Entities;
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

namespace Mikroszim
{
    public partial class Form1 : Form
    {
        List<Person> Population = new List<Person>();
        List<BirthProbability> BirthProbabilities = new List<BirthProbability>();
        List<DeathProbability> DeathProbabilities = new List<DeathProbability>();
        Random rng = new Random(1234);
        public Form1()
        {
            InitializeComponent();
            Population = GetPopulation(@"C:\Temp\nép.csv");
            BirthProbabilities = GetBirthProbabilities(@"C:\Temp\születés.csv");
            DeathProbabilities = GetDeathProbabilities(@"C:\Temp\halál.csv");

           //szimuláció váza kezdete
            for (int year = 2005; year <= 2024; year++) //evek
            {
                
                for (int i = 0; i < Population.Count; i++)      //osszes személy
                {
                    private void SimStep(int year, Person person)
                    {
                        //Ha halott akkor kihagyjuk, ugrunk a ciklus következő lépésére
                        if (!person.IsAlive) return;

                        // Letároljuk az életkort, hogy ne kelljen mindenhol újraszámolni
                        byte age = (byte)(year - person.BirthYear);

                        // Halál kezelése
                        // Halálozási valószínűség kikeresése
                        double pDeath = (from x in DeathProbabilities
                                         where x.Gender == person.Gender && x.Age == age
                                         select x.P).FirstOrDefault();
                        // Meghal a személy?
                        if (rng.NextDouble() <= pDeath)
                            person.IsAlive = false;

                        //Születés kezelése - csak az élő nők szülnek
                        if (person.IsAlive && person.Gender == Gender.Female)
                        {
                            //Szülési valószínűség kikeresése
                            double pBirth = (from x in BirthProbabilities
                                             where x.Age == age
                                             select x.P).FirstOrDefault();
                            //Születik gyermek?
                            if (rng.NextDouble() <= pBirth)
                            {
                                Person újszülött = new Person();
                                újszülött.BirthYear = year;
                                újszülött.NbrOfChildren = 0;
                                újszülött.Gender = (Gender)(rng.Next(1, 3));
                                Population.Add(újszülött);
                            }
                        }
                    }
                }

                int nbrOfMales = (from x in Population
                                  where x.Gender == Gender.Male && x.IsAlive
                                  select x).Count();
                int nbrOfFemales = (from x in Population
                                    where x.Gender == Gender.Female && x.IsAlive
                                    select x).Count();
                Console.WriteLine(
                    string.Format("Év:{0} Fiúk:{1} Lányok:{2}", year, nbrOfMales, nbrOfFemales));
            }

        }

        public List<Person> GetPopulation(string csvpath)
        {
            List<Person> population = new List<Person>();

            using (StreamReader sr = new StreamReader(csvpath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    population.Add(new Person()
                    {
                        BirthYear = int.Parse(line[0]),
                        Gender = (Gender)Enum.Parse(typeof(Gender), line[1]),
                        NbrOfChildren = int.Parse(line[2])
                    });
                }
            }

            return population;
        }
        public List<BirthProbability> GetBirthProbabilities(string csvpath)
        {
            List<BirthProbability> BirthProbabilities = new List<BirthProbability>();
            using (StreamReader sr = new StreamReader(csvpath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {

                    var line = sr.ReadLine().Split(',');
                    BirthProbabilities.Add(new BirthProbability()
                    {
                        Age = int.Parse(line[0]),
                        NbrOfChildren = int.Parse(line[1]),
                        BProbability = int.Parse(line[2])   //csv-ben csak 0 szerepel

                    });
                }
                return BirthProbabilities;
            }
        }

        public List<DeathProbability> GetDeathProbabilities(string csvpath)
        {

            List<DeathProbability> DeathProbabilities = new List<DeathProbability>();
            using (StreamReader sr = new StreamReader(csvpath, Encoding.Default))
            {
                var line = sr.ReadLine().Split(',');
                DeathProbabilities.Add(new DeathProbability()
                {
                    Gender= (Gender)int.Parse(line[0]),     //cast??
                    Age = int.Parse(line[1]),
                    DProbability= int.Parse(line[2])        //2 db nem 0 van a csvben


                });
            }
            return DeathProbabilities;
         }


    }
}
