using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading;


namespace TaskManagement
{
    public partial class Form1 : Form
    {
        List<Process> listaProcesos = new List<Process>();
        bool bandera;

        public Form1()
        {
            InitializeComponent();
            button1.Enabled = false;}

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ms = 0;
             s = 0;
             m = 0;
            toolStripStatusLabel2.Text = "Tiempo ejecucion: ";
            bandera = true;
            actualizarListView();
            timer1.Start();
            if (bandera == true)
            {
                //toolStripStatusLabel2 = timer1.Interval.ToString();
            }


        }


        private void actualizarListView()
        {
            int contador=0;
            listView1.Items.Clear();
            
            foreach (Process proceso in Process.GetProcesses())
            {
                string[] arregloDatos = new string[4];
                arregloDatos[0] = proceso.ProcessName;
                arregloDatos[1] = darFormato((proceso.PrivateMemorySize64/1024).ToString());
                arregloDatos[2] = proceso.Responding.ToString();
                //arregloDatos[3] =  num.Next().ToString();
                arregloDatos[3]= proceso.Id.ToString();
                ListViewItem item = new ListViewItem(arregloDatos);
                listView1.Items.Add(item);
                toolStripStatusLabel1.Text = "Procesos Actuales: " + listView1.Items.Count;
                contador += 1;
            }
        }
        private string darFormato(string mb)
        {
            string mbFinal = "";
            if(mb.Length <= 4)
            {
                int x=0;
                foreach(char a in mb)
                {
                    if (x==1)
                    {
                        mbFinal = mbFinal + "." + a;
                    }
                    else
                    {
                        if(x== (mb.Length - 1)) {
                            mbFinal = mbFinal + a;
                            Console.WriteLine(mbFinal);
                            return mbFinal + " MB";
                        }
                        else
                        {
                            mbFinal = mbFinal + a;
                        }
                        
                    }
                    x++;
                }         
            }

            if (mb.Length > 4)
            {
                int x = 0;
                foreach (char a in mb)
                {
                    if (x == 2)
                    {
                        mbFinal = mbFinal + "." + a;
                    }
                    else
                    {
                        if (x == (mb.Length - 1))
                        {
                            mbFinal = mbFinal + a;
                            Console.WriteLine(mbFinal);
                            return mbFinal + " MB";
                        }
                        else
                        {
                            mbFinal = mbFinal + a;
                        }
                    }
                    x++;
                }
            }
            return "00.00 MB";
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;
            //Console.WriteLine(listView1.FocusedItem.SubItems[4].ToString());
            //Console.WriteLine(obtenID(listView1.FocusedItem.SubItems[4].ToString()));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int idProceso = obtenID(listView1.FocusedItem.SubItems[3].ToString());
            try
            {
                System.Diagnostics.Process.GetProcessById(idProceso).Kill();
            }
            catch
            {
                actualizarListView();
            }
            
            actualizarListView();
        }

        private void agregaDatosLista()
        {
            foreach (Process p in Process.GetProcesses())
            {
                listaProcesos.Add(p);
            }
        }

        private void agregarListaAlaListview()
        {
            listaProcesos.Sort();
        }

        private int obtenID(string palabra)
        {
            string num="";
            foreach(char x in palabra)
            {
                if(char.IsDigit(x))
                {
                    num=num+x;
                }
            }
            return Convert.ToInt32(num);
        }
        int ms=0;
        int s = 0;
        int m = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Interval = 10;
            ms++;
            if (ms == 100)
            {
                if ((s % 5) == 0 && s!=0) { actualizarListView(); }           
                s++;
                ms = 0;
            }
            else if (s == 60)
            {
                m++;
                s = 0;
            }
            toolStripStatusLabel2.Text = "Tiempo ejecucion: " + m.ToString().PadLeft(2, '0') + ":" + s.ToString().PadLeft(2, '0') + ":" + ms.ToString().PadLeft(2, '0');
          
        }

        private void toolStripStatusLabel2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }
    }
}

