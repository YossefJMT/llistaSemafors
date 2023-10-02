using llista;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace llista
{
    public partial class MainWindow : Window
    {
        // Utiliza ObservableCollection para que los cambios se reflejen en la interfaz de usuario automáticamente
        List<Persona> cola_list = new List<Persona>();
        List<Persona> local_list = new List<Persona>();

        public MainWindow()
        {
            InitializeComponent();
            this.colalist.ItemsSource = cola_list;
            this.locallist.ItemsSource = local_list;

        }

        private void Afegir_Cua(object sender, RoutedEventArgs e)
        {
            string nombre = this.nom.Text;
            int edad = Convert.ToInt32(this.edat.Text);

            // Verifica si la persona ya existe en cola_list o local_list
            bool existeEnCola = cola_list.Any(p => p.nom == nombre && p.edat == edad);
            bool existeEnLocal = local_list.Any(p => p.nom == nombre && p.edat == edad);

            if (existeEnCola || existeEnLocal)
            {
                // Muestra un mensaje de aviso si la persona ya existe
                MessageBox.Show("Esta persona ya existe en la lista.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                // Si no existe, crea una nueva persona y agrégala a cola_list
                Persona p = new();
                p.nom = nombre;
                p.edat = edad;

                cola_list.Add(p);
                this.colalist.Items.Refresh();
            }
        }


        private void Eliminar_Focus(object sender, RoutedEventArgs e)
        {
            // Verifica si hay un elemento seleccionado en colalist
            if (colalist.SelectedItem != null)
            {
                // Obtén el elemento seleccionado y elimínalo de la lista
                Persona personaSeleccionada = (Persona)colalist.SelectedItem;
                cola_list.Remove(personaSeleccionada);

                // Limpia la selección en colalist
                colalist.SelectedItem = null;

                // Actualiza la vista de colalist
                this.colalist.Items.Refresh();
            }
            else if (locallist.SelectedItem != null)
            {
                // Si no hay un elemento seleccionado en colalist, verifica locallist
                Persona personaSeleccionada = (Persona)locallist.SelectedItem;
                local_list.Remove(personaSeleccionada);

                // Limpia la selección en locallist
                locallist.SelectedItem = null;

                // Actualiza la vista de locallist
                this.locallist.Items.Refresh();
            }
        }


        private void Moure_Focus(object sender, RoutedEventArgs e)
        {
            // Verifica si hay un elemento seleccionado en colalist
            if (colalist.SelectedItem != null)
            {
                // Obtén el elemento seleccionado
                Persona personaSeleccionada = (Persona)colalist.SelectedItem;

                // Remueve el elemento de la lista actual y agrégalo a la otra lista
                cola_list.Remove(personaSeleccionada);
                local_list.Add(personaSeleccionada);

                // Limpia la selección en ambos ListBox
                colalist.SelectedItem = null;
                locallist.SelectedItem = null;

                // Actualiza las vistas
                this.colalist.Items.Refresh();
                this.locallist.Items.Refresh();
            }
            else if (locallist.SelectedItem != null)
            {
                // Si no hay un elemento seleccionado en colalist, realiza el movimiento inverso
                Persona personaSeleccionada = (Persona)locallist.SelectedItem;

                // Remueve el elemento de la lista actual y agrégalo a la otra lista
                local_list.Remove(personaSeleccionada);
                cola_list.Add(personaSeleccionada);

                // Limpia la selección en ambos ListBox
                colalist.SelectedItem = null;
                locallist.SelectedItem = null;

                // Actualiza las vistas
                this.colalist.Items.Refresh();
                this.locallist.Items.Refresh();
            }
        }

        private void mover_cola(object sender, RoutedEventArgs e)
        {
            // Mueve todos los elementos de cola_list a local_list
            foreach (var persona in cola_list)
            {
                local_list.Add(persona);
            }

            // Limpia la cola_list después de la transferencia
            cola_list.Clear();

            // Actualiza las vistas de ambos ListBox
            this.colalist.Items.Refresh();
            this.locallist.Items.Refresh();
        }

        private void mover_local(object sender, RoutedEventArgs e)
        {
            // Mueve todos los elementos de local_list a cola_list
            foreach (var persona in local_list)
            {
                cola_list.Add(persona);
            }

            // Limpia local_list después de la transferencia
            local_list.Clear();

            // Actualiza las vistas de ambos ListBox
            this.colalist.Items.Refresh();
            this.locallist.Items.Refresh();
        }


        private void vaciar_cola(object sender, RoutedEventArgs e)
        {
            // Limpia local_list
            cola_list.Clear();

            // Actualiza las vistas
            this.colalist.Items.Refresh();
        }

        private void vaciar_local(object sender, RoutedEventArgs e)
        {
            // Limpia local_list
            local_list.Clear();

            // Actualiza las vistas
            this.locallist.Items.Refresh();
        }
    }
}
