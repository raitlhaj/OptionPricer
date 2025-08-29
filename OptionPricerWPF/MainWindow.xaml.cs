using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace OptionPricerWPF
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow:MahApps.Metro.Controls.MetroWindow
    {
        public MainWindow()
        {
             InitializeComponent();

            Loaded += async (s, e) => await InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            // Afficher un indicateur de chargement (optionnel)
            // Par exemple : myProgressBar.Visibility = Visibility.Visible;

            try
            {
                // Exécuter une tâche longue sur un thread de fond
                var data = await Task.Run(() =>
                {
                    // Simuler une opération longue (ex. : appel API, chargement de données)
                    System.Threading.Thread.Sleep(2000); // Simulation
                    return "Données chargées !";
                });

                // Mettre à jour l'UI sur le thread principal
                // (pas besoin de Dispatcher ici, car await garantit le retour sur le thread UI)
                //myTextBlock.Text = data;
            }
            catch (Exception ex)
            {
                // Gérer les erreurs et mettre à jour l'UI si nécessaire
                MessageBox.Show($"Erreur : {ex.Message}");
            }
            finally
            {
                // Masquer l'indicateur de chargement
                // myProgressBar.Visibility = Visibility.Collapsed;
            }
        }

    }
}
