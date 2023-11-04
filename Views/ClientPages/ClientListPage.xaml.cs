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
			ClientsGrid.ItemsSource = NedvizhdbContext.Clients.ToList();
		}

		private void DeleteUserButton_Click(object sender, RoutedEventArgs e)
		{

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
	}
}
