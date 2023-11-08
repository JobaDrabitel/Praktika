using Praktika.Models;
using Praktika.Views.RealtyPages;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Praktika.Views.DemandPages
{
    /// <summary>
    /// Логика взаимодействия для DemandListPage.xaml
    /// </summary>
    public partial class DemandListPage : Page
    {
		NedvizhdbContext NedvizhdbContext = new NedvizhdbContext();
		public DemandListPage()
		{
			InitializeComponent();
			DataContext = NedvizhdbContext.Demands.ToList();
		}

		private void DeleteUserButton_Click(object sender, RoutedEventArgs e)
		{
			Demand demand = DemandsGrid.SelectedItem as Demand;
			if (demand == null) return;
			if (demand.Deals.Count > 0) { MessageBox.Show("Нельзя удалить потребность, участвующую в сделке"); return; }
			NedvizhdbContext.Demands.Remove(demand);
			NedvizhdbContext.SaveChanges();
			RefreshUserList();
		}
		private void RefreshUserList()
		{
			var demands = NedvizhdbContext.Demands.ToList();
			DemandsGrid.ItemsSource = demands;
		}
		private void AddUserButton_Click(object sender, RoutedEventArgs e)
		{
			DemandCreatePage demandCreatePage = new DemandCreatePage();
			Window.GetWindow(this).Content = demandCreatePage;
		}

		private void EditUserButton_Click(object sender, RoutedEventArgs e)
		{
			Demand demand = DemandsGrid.SelectedItem as Demand;
			if (demand == null)
			{
				MessageBox.Show("Выберите клиента для изменения");
				return;
			}
			DemandCreatePage demandCreatePage = new DemandCreatePage(demand);
			Window.GetWindow(this).Content = demandCreatePage;

		}
		private void BackButton_Click(object sender, RoutedEventArgs e)
		{
			MainWindow mainWindow = new MainWindow();
			mainWindow.Show();
			Window.GetWindow(this).Close();
		}
	}
}
