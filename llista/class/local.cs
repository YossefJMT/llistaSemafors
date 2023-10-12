using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace llista
{
    public class Local
    {
        private int aforamentMaxim;
        private Semaphore semaphore;
        // private Semaphore cuaSemaphore = new Semaphore(1, 1); // Semàfor per controlar l'accés a la cua.
        ObservableCollection<Persona> cola_list;
        ObservableCollection<Persona> local_list;
        ObservableCollection<Persona> historial_list;

        public Local(int aforamentMaxim, ObservableCollection<Persona> prm_cola_list, ObservableCollection<Persona> prm_local_list, ObservableCollection<Persona> prm_historial_list)
        {
            this.aforamentMaxim = aforamentMaxim;
            this.cola_list = prm_cola_list;
            this.local_list = prm_local_list;
            this.semaphore = new Semaphore(aforamentMaxim, aforamentMaxim); // Inicialitzar el semàfor amb el mateix valor que l'aforament màxim.
            this.historial_list = prm_historial_list;
        }

        public async Task PosarCua(Persona persona)
        {
            cola_list.Add(persona); // Afegir la persona a la llista de persones a la cua.
            
            await esperantCua(persona);
            
        }

        public async Task esperantCua(Persona persona)
        {
            try
            {
                bool haEntrat = semaphore.WaitOne(persona.tempsEspera * 1000);

                if (haEntrat)
                {
                    await entraLocal(persona);
                }
                else
                {
                    cola_list.Remove(persona); // Treure la persona de la llista de persones a la cua.
                    MessageBox.Show("No ha entrat a la discoteca.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            finally
            {
                semaphore.Release();
            }
        }

        public async Task entraLocal(Persona persona)
        {
            local_list.Add(persona); // Afegir la persona a la llista de persones dins de la discoteca.
            cola_list.Remove(persona); // Treure la persona de la llista de persones a la cua.
            await Task.Delay(persona.tempsInterior * 1000);
            local_list.Remove(persona); // Treure la persona de la llista de persones dins de la discoteca.
            historial_list.Add(persona); // Afegir la persona a la llista de persones que han entrat a la discoteca.
        }

    }
}
