using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace llista
{
    /// <summary>
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        public Persona Persona
        {
            get {
                Persona p = new();
                p.nom = this.nom.Text;
                p.edat = Convert.ToInt32(this.edat.Text);
                p.tempsEspera = Convert.ToInt32(this.inpTempsCua.Text);
                p.tempsInterior = Convert.ToInt32(this.inpTempsInterior.Text);

                return p; 
            }

            set
            {
                this.nom.Text = value.nom;
                this.edat.Text = value.edat.ToString();
                this.nom.Focus();
            }
            
        }

        private void cancelar(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void guardar(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
}
