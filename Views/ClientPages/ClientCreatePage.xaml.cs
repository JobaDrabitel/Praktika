using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Praktika.Models;
using Praktika.Views.ClientPages;
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

namespace Praktika.Views.ClientPages
{
	/// <summary>
	/// Логика взаимодействия для ClientCreatePage.xaml
	/// </summary>
	public partial class ClientCreatePage : Page
	{
		NedvizhdbContext _context = new();
		Client Client { get; set; }
		public ClientCreatePage()
		{
			InitializeComponent();
			Client = new Client();
			DataContext = Client;
			ClientItemControl.ItemsSource = new List<Client>() { Client };
		}
		public ClientCreatePage(Client client)
		{
			InitializeComponent();
			DataContext = client;
			ClientItemControl.ItemsSource = new List<Client>() { client };
		}
		private async void SaveChangesButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				Client currentClient = (Client)((Button)sender).Tag;
				if (currentClient.Email.IsNullOrEmpty() || currentClient.Phone.IsNullOrEmpty()) { MessageBox.Show("Необходимо заполнить поля Email и Phone!"); return; }
				if (!Validator.IsValidEmail(currentClient.Email)) { MessageBox.Show("Некорректные данные почты. Пожалуйста, проверьте введенные значения."); return; }
				if (Client.Id == 0)
				{
					var lastClient = await _context.Clients.OrderBy(c => c.Id).LastAsync();
					Client.Id = lastClient.Id + 1;
					_context.Clients.Add(Client);
				}
				await _context.SaveChangesAsync();
				MessageBox.Show("Изменения сохранены успешно.");
			}
			catch (Exception ex)
			{
				MessageBox.Show("Некорректные данные. Пожалуйста, проверьте введенные значения.");
				return;
			}
		}

		private void ClientBackButton_Click(object sender, RoutedEventArgs e)
		{
			ClientListPage clientListPage = new();
			Window.GetWindow(this).Content = clientListPage;
		}
	}
}
