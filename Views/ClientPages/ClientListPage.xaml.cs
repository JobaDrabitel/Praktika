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
using Praktika.Models;
using Praktika.Views.ClientPages;


namespace Praktika.Views.ClientPages
{
	/// <summary>
	/// Логика взаимодействия для ClientListPage.xaml
	/// </summary>
	public partial class ClientListPage : Page
	{
		NedvizhdbContext NedvizhdbContext = new NedvizhdbContext();
		public ClientListPage()
		{
			InitializeComponent();
			DataContext = NedvizhdbContext.Clients.ToList();
		}

		private void DeleteUserButton_Click(object sender, RoutedEventArgs e)
		{
			Client client = ClientsGrid.SelectedItem as Client;
			if (client == null) return;
			if (client.Supplies.Count > 0 || client.Demands.Count>0) { MessageBox.Show("Нельзя удалить клиента с предложением или потребностью"); return; }
			NedvizhdbContext.Clients.Remove(client);
			NedvizhdbContext.SaveChanges();
			RefreshUserList();
		}
		private void RefreshUserList()
		{
			var clients = NedvizhdbContext.Clients.ToList();
			ClientsGrid.ItemsSource = clients;
		}
		private void AddUserButton_Click(object sender, RoutedEventArgs e)
		{
			ClientCreatePage clientCreatePage = new ClientCreatePage();
			Window.GetWindow(this).Content = clientCreatePage;
		}

		private void EditUserButton_Click(object sender, RoutedEventArgs e)
		{
			Client client = ClientsGrid.SelectedItem as Client;
			if (client == null)
			{
				MessageBox.Show("Выберите клиента для изменения");
				return;
			}
			ClientCreatePage clientCreatePage = new ClientCreatePage(client);
			Window.GetWindow(this).Content = clientCreatePage;

		}
		private void BackButton_Click(object sender, RoutedEventArgs e)
		{
			MainWindow mainWindow = new MainWindow();
			mainWindow.Show();
			Window.GetWindow(this).Close();
		}
	}
}
