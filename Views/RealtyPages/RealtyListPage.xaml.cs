using Praktika.Models;
using Praktika.Views.ClientPages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Praktika.Views.RealtyPages
{
	/// <summary>
	/// Логика взаимодействия для RealtyListPage.xaml
	/// </summary>
	public partial class RealtyListPage : Page
	{
		NedvizhdbContext NedvizhdbContext = new NedvizhdbContext();
		public RealtyListPage()
		{
			InitializeComponent();
			DataContext = NedvizhdbContext.Realties.ToList();
		}

		private void DeleteUserButton_Click(object sender, RoutedEventArgs e)
		{
			Realty realty = RealtiesGrid.SelectedItem as Realty;
			if (realty == null) return;
			if (realty.Supplies.Count > 0 ) { MessageBox.Show("Нельзя удалить клиента с предложением"); return; }
			NedvizhdbContext.Realties.Remove(realty);
			NedvizhdbContext.SaveChanges();
			RefreshUserList();
		}
		private void RefreshUserList()
		{
			var clients = NedvizhdbContext.Realties.ToList();
			RealtiesGrid.ItemsSource = clients;
		}
		private void AddUserButton_Click(object sender, RoutedEventArgs e)
		{
			RealtyCreatePage realtiesCreatePage = new RealtyCreatePage();
			Window.GetWindow(this).Content = realtiesCreatePage;
		}

		private void EditUserButton_Click(object sender, RoutedEventArgs e)
		{
			Realty realty = RealtiesGrid.SelectedItem as Realty;
			if (realty == null)
			{
				MessageBox.Show("Выберите клиента для изменения");
				return;
			}
			RealtyCreatePage realtyCreatePage = new RealtyCreatePage(realty);
			Window.GetWindow(this).Content = realtyCreatePage;

		}
		private void AddressFilterTextBox_KeyUp(object sender, RoutedEventArgs e)
		{
			string filterText = AddressFilterTextBox.Text.ToLower();
			ICollectionView view = CollectionViewSource.GetDefaultView(RealtiesGrid.ItemsSource);

			if (view != null)
			{
				if (string.IsNullOrWhiteSpace(filterText))
				{
					view.Filter = null;
				}
				else
				{
					view.Filter = item =>
					{
						Realty realty = item as Realty;
						return realty != null &&
							   (realty.AddressCity?.ToLower().Contains(filterText) == true ||
								realty.AddressStreet?.ToLower().Contains(filterText) == true);
					};
				}
			}
		}

		private void BackButton_Click(object sender, RoutedEventArgs e)
		{
			MainWindow mainWindow = new MainWindow();
			mainWindow.Show();
			Window.GetWindow(this).Close();
		}
	}
}

