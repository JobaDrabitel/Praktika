using Praktika.Models;
using Praktika.Views.DemandPages;
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

namespace Praktika.Views.SupplyPages
{
    /// <summary>
    /// Логика взаимодействия для SupplyListPage.xaml
    /// </summary>
    public partial class SupplyListPage : Page
    {
		NedvizhdbContext NedvizhdbContext = new NedvizhdbContext();
		public SupplyListPage()
		{
			InitializeComponent();
			DataContext = NedvizhdbContext.Supplies.ToList();
		}

		private void DeleteUserButton_Click(object sender, RoutedEventArgs e)
		{
			Supply supply = SuppliesGrid.SelectedItem as Supply;
			if (supply == null) return;
			if (supply.Deals.Count > 0) { MessageBox.Show("Нельзя удалить потребность, участвующую в сделке"); return; }
			NedvizhdbContext.Supplies.Remove(supply);
			NedvizhdbContext.SaveChanges();
			RefreshUserList();
		}
		private void RefreshUserList()
		{
			var supply = NedvizhdbContext.Supplies.ToList();
			SuppliesGrid.ItemsSource = supply;
		}
		private void AddUserButton_Click(object sender, RoutedEventArgs e)
		{
			SupplyCreatePage supplyCreatePage = new SupplyCreatePage();
			Window.GetWindow(this).Content = supplyCreatePage;
		}

		private void EditUserButton_Click(object sender, RoutedEventArgs e)
		{
			Supply supply = SuppliesGrid.SelectedItem as Supply;
			if (supply == null)
			{
				MessageBox.Show("Выберите клиента для изменения");
				return;
			}
			SupplyCreatePage supplyCreatePage = new SupplyCreatePage(supply);
			Window.GetWindow(this).Content = supplyCreatePage;

		}
		private void BackButton_Click(object sender, RoutedEventArgs e)
		{
			MainWindow mainWindow = new MainWindow();
			Window.GetWindow(this).Content = mainWindow.Content;
		}
	}
}
