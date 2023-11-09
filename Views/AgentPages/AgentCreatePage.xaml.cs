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

namespace Praktika.Views.AgentPages
{
	/// <summary>
	/// Логика взаимодействия для AgentCreatePage.xaml
	/// </summary>
	public partial class AgentCreatePage : Page
	{
		NedvizhdbContext _context = new();
		Agent Agent { get; set; }
		public AgentCreatePage()
		{
			InitializeComponent();
			Agent = new Agent();
			DataContext = Agent;
			ClientItemControl.DataContext = Agent;
		}
		public AgentCreatePage(Agent agent)
		{
			InitializeComponent();
			Agent = agent;
			DataContext = Agent; 
			ClientItemControl.DataContext = Agent;
		}
		private async void SaveChangesButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				Agent currentAgent = (Agent)((Button)sender).Tag;
				if (currentAgent.FirstName.IsNullOrEmpty() || currentAgent.LastName.IsNullOrEmpty() || currentAgent.MiddleName.IsNullOrEmpty()) { MessageBox.Show("Необходимо заполнить ФИО"); return; }
				if (Convert.ToInt32(currentAgent.DealShare) < 0 || Convert.ToInt32(currentAgent.DealShare) > 100) { MessageBox.Show("Доля может быть от 0 до 100"); return; }
				if (Agent.Id == 0)
				{
					var lastClient = await _context.Agents.OrderBy(c => c.Id).LastAsync();
					Agent.Id = lastClient.Id + 1;
					_context.Agents.Add(Agent);
				}
				else
				{
					_context.Agents.Update(Agent);
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
			AgentListPage agentListPage = new();
			Window.GetWindow(this).Content = agentListPage;
		}
	}
}

