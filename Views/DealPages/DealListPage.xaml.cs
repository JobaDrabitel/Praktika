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

namespace Praktika.Views.DealPages
{
    /// <summary>
    /// Логика взаимодействия для DealListPage.xaml
    /// </summary>
    public partial class DealListPage : Page
    {
		NedvizhdbContext NedvizhdbContext = new NedvizhdbContext();
		public DealListPage()
		{
			InitializeComponent();
			DataContext = NedvizhdbContext.Deals.ToList();
		}

		private void DeleteUserButton_Click(object sender, RoutedEventArgs e)
		{
			Deal deal = DealsGrid.SelectedItem as Deal;
			if (deal == null) return;
			NedvizhdbContext.Deals.Remove(deal);
			NedvizhdbContext.SaveChanges();
			RefreshUserList();
		}
		private void RefreshUserList()
		{
			var demands = NedvizhdbContext.Deals.ToList();
			DealsGrid.ItemsSource = demands;
		}
		private void AddUserButton_Click(object sender, RoutedEventArgs e)
		{
			DealCreatePage demandCreatePage = new DealCreatePage();
			Window.GetWindow(this).Content = demandCreatePage;
		}

		private void EditUserButton_Click(object sender, RoutedEventArgs e)
		{
			Deal deal = DealsGrid.SelectedItem as Deal;
			if (deal == null)
			{
				MessageBox.Show("Выберите клиента для изменения");
				return;
			}
			DealCreatePage dealCreatePage = new DealCreatePage(deal);
			Window.GetWindow(this).Content = dealCreatePage;

		}
		private void BackButton_Click(object sender, RoutedEventArgs e)
		{
			MainWindow mainWindow = new MainWindow();
			mainWindow.Show();
			Window.GetWindow(this).Close();
		}
	}
}

