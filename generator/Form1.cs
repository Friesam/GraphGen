using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace generator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBox1.SelectedText = null;
            panel1.Paint += new PaintEventHandler(panel1_Paint);
        }

        int n, x, y, x1, y1, x2, y2;

        List<Vertex> G;
        List<Color> allColors;

        private List<Color> GetAllColors()
        {
            List<Color> allColors = new List<Color>();

            foreach (PropertyInfo property in typeof(Color).GetProperties())
            {
                if (property.PropertyType == typeof(Color))
                {
                    allColors.Add((Color)property.GetValue(null));
                }
            }

            return allColors;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            draw(e.Graphics);
        }

    
        private void calculateXY(int id)
        {
            int Width = panel1.Width;
            int Height = panel1.Height;

            x = Width / 2 + (int)(Width * Math.Cos(2 * id * Math.PI / n) / 4.0);
            y = Height / 2 + (int)(Width * Math.Sin(2 * id * Math.PI / n) / 4.0);
        }

        private void draw(Graphics g)
        {
            if (G != null)
            {
                int Width = panel1.Width;
                int Height = panel1.Height;
                Pen pen = new Pen(Color.Black);

                n = G.Count;

                for (int i = 0; i < n; i++)
                {
                    Vertex u = G[i];

                    calculateXY(u.Id);
                    x1 = x + (Width / 2) / n / 2;
                    y1 = y + (Width / 2) / n / 2;

                    if (u.Edge != null)
                    {
                        for (int j = 0; j < u.Edge.Count; j++)
                        {
                            Vertex v = u.Edge[j];

                            calculateXY(v.Id);
                            x2 = x + (Width / 2) / n / 2;
                            y2 = y + (Width / 2) / n / 2;
                            g.DrawLine(pen, x1, y1, x2, y2);
                        }
                    }
                }

                for (int i = 0; i < n; i++)
                {
                    Vertex u = G[i];
                    SolidBrush brush = new SolidBrush(allColors[u.Color]);

                    calculateXY(u.Id);
                    g.FillEllipse(brush, x, y, (Width / 2) / n, (Width / 2) / n);
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int numVers = int.Parse((string)textBox1.SelectedText);
            //int numCols = int.Parse((string)comboBox2.SelectedItem);
            //int seed = int.Parse((string)comboBox3.SelectedItem);
            int seed = 1;
            Random random = new Random(seed);

            G = new List<Vertex>();

            for (int i = 0; i < numVers; i++)
            {
                Vertex v = new Vertex(random.Next(allColors), i);

                G.Add(v);
            }

            int n = 0;
            for (int i = 0; i < numVers; i++)
            {
               
                int numEdges = n * (n - 1) / 2;
                numEdges = random.Next(numVers - 1);
                Vertex v = G[i];

                v.Edge = new List<Vertex>();

                for (int j = 0; j < numEdges; j++)
                {
                    int id = random.Next(numVers);

                    while (id == v.Id)
                        id = random.Next(numVers);

                    v.Edge.Add(G[id]);
                }
            }

            double probability = 0.25;
            int n = G.Count;
            int generations = 100 * n;
            int population = n;
            int restart = 0;
            eurle hc = new eurle();
            List<Vertex> solution = null;
            bool found = hc.EHC(
                probability,
                generations,
                population,
                G,
                ref restart,
                ref solution,
                ref random);

            if (!found)
                MessageBox.Show("Solution not found! Try a new # Colors",
                    "Warning Message", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

            else
            {
                for (int i = 0; i < solution.Count; i++)
                    G[i] = solution[i];

                panel1.Invalidate();
            }
        }
    }


        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
