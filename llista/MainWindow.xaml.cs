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


        private void consultarDades(object sender, RoutedEventArgs e)
        {
            Window1 finestra = new Window1();
            bool? result = finestra.ShowDialog();

            if (result.HasValue && result.Value)
            {
                Persona persona = finestra.Persona;
                Afegir_Cua(persona);

                MessageBox.Show("DADA GUARDADA.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                MessageBox.Show("DADA CANCELADA.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void editarDades(object sender, RoutedEventArgs e)
        {
            if (colalist.SelectedItem != null)
            {
                // Obtén el elemento seleccionado y elimínalo de la lista
                Persona personaSeleccionada = (Persona)colalist.SelectedItem;
                cola_list.Remove(personaSeleccionada);

                Window1 finestra = new Window1();
                finestra.Persona = personaSeleccionada;
                bool? result = finestra.ShowDialog();

                if (result.HasValue && result.Value)
                {
                    Afegir_Cua(finestra.Persona);
                    MessageBox.Show("DADA GUARDADA.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    MessageBox.Show("DADA CANCELADA.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);

                }

                // Limpia la selección en colalist
                colalist.SelectedItem = null;

                // Actualiza la vista de colalist
                this.colalist.Items.Refresh();
            }


        }

        
        private void Afegir_Cua(Persona persona)
        {

            // Verifica si la persona ya existe en cola_list o local_list
            bool existeEnCola = cola_list.Any(p => p.nom == persona.nom && p.edat == persona.edat);
            bool existeEnLocal = local_list.Any(p => p.nom == persona.nom && p.edat == persona.edat);

            if (existeEnCola || existeEnLocal)
            {
                // Muestra un mensaje de aviso si la persona ya existe
                MessageBox.Show("Esta persona ya existe en la lista.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                // Si no existe, crea una nueva persona y agrégala a cola_list
                cola_list.Add(persona);
                this.colalist.Items.Refresh();
            }
        }
        

        private void Eliminar_Focus(object sender, RoutedEventArgs e)
        {
            // Verifica si hay elementos seleccionados en colalist
            if (colalist.SelectedItems.Count > 0)
            {
                // Obtén una lista de elementos seleccionados
                List<Persona> personasSeleccionadas = new List<Persona>();
                foreach (var item in colalist.SelectedItems)
                {
                    personasSeleccionadas.Add((Persona)item);
                }

                // Remueve los elementos seleccionados de la lista
                foreach (var persona in personasSeleccionadas)
                {
                    cola_list.Remove(persona);
                }

                // Limpia la selección en colalist
                colalist.SelectedItems.Clear();

                // Actualiza la vista de colalist
                this.colalist.Items.Refresh();
            }
            else if (locallist.SelectedItems.Count > 0)
            {
                // Verifica si hay elementos seleccionados en locallist
                // Obtén una lista de elementos seleccionados
                List<Persona> personasSeleccionadas = new List<Persona>();
                foreach (var item in locallist.SelectedItems)
                {
                    personasSeleccionadas.Add((Persona)item);
                }

                // Remueve los elementos seleccionados de la lista
                foreach (var persona in personasSeleccionadas)
                {
                    local_list.Remove(persona);
                }

                // Limpia la selección en locallist
                locallist.SelectedItems.Clear();

                // Actualiza la vista de locallist
                this.locallist.Items.Refresh();
            }
        }


        private void Moure_Focus(object sender, RoutedEventArgs e)
        {
            // Verifica si hay elementos seleccionados en colalist
            if (colalist.SelectedItems.Count > 0)
            {
                // Obtén una lista de elementos seleccionados
                List<Persona> personasSeleccionadas = new List<Persona>();
                foreach (var item in colalist.SelectedItems)
                {
                    personasSeleccionadas.Add((Persona)item);
                }

                // Remueve los elementos de la lista actual y agrégalo a la otra lista
                foreach (var persona in personasSeleccionadas)
                {
                    cola_list.Remove(persona);
                    local_list.Add(persona);
                }

                // Limpia la selección en colalist
                colalist.SelectedItems.Clear();

                // Actualiza las vistas
                this.colalist.Items.Refresh();
                this.locallist.Items.Refresh();
            }
            else if (locallist.SelectedItems.Count > 0)
            {
                // Verifica si hay elementos seleccionados en locallist
                // Obtén una lista de elementos seleccionados
                List<Persona> personasSeleccionadas = new List<Persona>();
                foreach (var item in locallist.SelectedItems)
                {
                    personasSeleccionadas.Add((Persona)item);
                }

                // Remueve los elementos de la lista actual y agrégalo a la otra lista
                foreach (var persona in personasSeleccionadas)
                {
                    local_list.Remove(persona);
                    cola_list.Add(persona);
                }

                // Limpia la selección en locallist
                locallist.SelectedItems.Clear();

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
