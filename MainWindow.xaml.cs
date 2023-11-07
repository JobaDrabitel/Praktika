using Praktika.Models;
using Praktika.Views.AgentPages;
using Praktika.Views.ClientPages;
using Praktika.Views.DealPages;
using Praktika.Views.DemandPages;
using Praktika.Views.RealtyPages;
using Praktika.Views.SupplyPages;
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

namespace Praktika
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		NedvizhdbContext NedvizhdbContext = new NedvizhdbContext();
		public MainWindow()
		{
			InitializeComponent();
		}

		private void ClientListButton_Click(object sender, RoutedEventArgs e)
		{
			ClientListPage clientListPage = new ClientListPage();
			this.Content = clientListPage;
		}

		private void AgentListButton_Click(object sender, RoutedEventArgs e)
		{
			AgentListPage clientListPage = new AgentListPage();
			this.Content = clientListPage;
		}

		private void RealtyListButton_Click(object sender, RoutedEventArgs e)
		{
			RealtyListPage realtyListPage = new RealtyListPage();
			this.Content = realtyListPage;
		}

		private void DemandListButton_Click(object sender, RoutedEventArgs e)
		{
			DemandListPage demandListPage = new DemandListPage();
			this.Content = demandListPage;
		}

		private void SupplyListButton_Click(object sender, RoutedEventArgs e)
		{
			SupplyListPage supplyListPage = new SupplyListPage();
			this.Content = supplyListPage;
		}

		private void DealListButton_Click(object sender, RoutedEventArgs e)
		{
			DealListPage dealListPage = new DealListPage();
			this.Content = dealListPage;
		}
	}
}
