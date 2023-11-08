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
	/// Логика взаимодействия для AgentListPage.xaml
	/// </summary>
	public partial class AgentListPage : Page
	{
		NedvizhdbContext NedvizhdbContext = new NedvizhdbContext();
		public AgentListPage()
		{
			InitializeComponent();
			DataContext = NedvizhdbContext.Agents.ToList();
		}
		private void DeleteAgentButton_Click(object sender, RoutedEventArgs e)
		{
			Agent agent = AgentsGrid.SelectedItem as Agent;
			if (agent == null) return;
			if (agent.Supplies.Count > 0 || agent.Demands.Count > 0) { MessageBox.Show("Нельзя удалить риелтора с предложением или потребностью"); return; }
			NedvizhdbContext.Agents.Remove(agent);
			NedvizhdbContext.SaveChanges();
			RefreshAgentList();
		}
		private void RefreshAgentList()
		{
			var agents = NedvizhdbContext.Agents.ToList();
			AgentsGrid.ItemsSource = agents;
		}
		private void AddAgentButton_Click(object sender, RoutedEventArgs e)
		{
			AgentCreatePage agentCreatePage = new AgentCreatePage();
			Window.GetWindow(this).Content = agentCreatePage;
		}

		private void EditAgentButton_Click(object sender, RoutedEventArgs e)
		{
			Agent agent = AgentsGrid.SelectedItem as Agent;
			if (agent == null)
			{
				MessageBox.Show("Выберите риелтора для изменения");
				return;
			}
			AgentCreatePage clientCreatePage = new AgentCreatePage(agent);
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
