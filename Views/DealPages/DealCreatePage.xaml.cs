using Microsoft.EntityFrameworkCore;
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
    /// Логика взаимодействия для DealCreatePage.xaml
    /// </summary>
    public partial class DealCreatePage : Page
    {
		NedvizhdbContext _context = new();
		Deal Deal { get; set; }
		public DealCreatePage()
		{
			InitializeComponent();
			Deal = new Deal();
			DataContext = Deal;
			DemandComboBox.ItemsSource = _context.Demands.Where(d => d.Deals.Count() == 0).ToList();
			SupplyComboBox.ItemsSource = _context.Supplies.Where(s => s.Deals.Count() == 0).ToList();
			CalculateComission();
		}
		public DealCreatePage(Deal deal)
		{
			InitializeComponent();
			Deal = deal;
			DataContext = Deal;
			DemandComboBox.ItemsSource = _context.Demands.ToList();
			SupplyComboBox.ItemsSource = _context.Supplies.ToList();
			CalculateComission();

		}
		private async void SaveChangesButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				Deal currentDemand = (Deal)((Button)sender).Tag;
				if (Deal.Id == 0)
				{
					var lastDemand = await _context.Demands.OrderBy(c => c.Id).LastAsync();
					Deal.Id = lastDemand.Id + 1;
					_context.Deals.Add(Deal);
				}
				else
				{
					_context.Deals.Update(Deal);
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

		private void DemandBackButton_Click(object sender, RoutedEventArgs e)
		{
			DealListPage dealListPage = new();
			Window.GetWindow(this).Content = dealListPage;
		}
		private void CalculateComission()
		{
			var clientSellerPrice = (36000 + (double)Deal.Supply.Price * 0.01);
			var clientBuyerPrice = (double)Deal.Supply.Price * 0.03;
			double agentBuyerComission;
			double agentSellerComission;
			if (Deal.Supply.Agent.DealShare != 0 && Deal.Supply.Agent.DealShare != null)
				agentBuyerComission = (clientBuyerPrice+ clientSellerPrice) * ((double)Deal.Supply.Agent.DealShare * 0.01);
			else
				agentBuyerComission = (clientBuyerPrice + clientSellerPrice) * 0.45;
			if (Deal.Demand.Agent.DealShare != 0 && Deal.Demand.Agent.DealShare != null)
				agentSellerComission = (clientBuyerPrice + clientSellerPrice) * ((double)Deal.Demand.Agent.DealShare * 0.01);
			else
				agentSellerComission = (clientBuyerPrice + clientSellerPrice) * 0.45;
			if (clientBuyerPrice + clientSellerPrice > agentBuyerComission + agentSellerComission)
				CompanyCommission.Text += (clientBuyerPrice + clientSellerPrice - agentBuyerComission + agentSellerComission).ToString();
			else 
				CompanyCommission.Text += 0.ToString();
			ClientBuyerPrice.Text += clientBuyerPrice.ToString();
			ClientSellerPrice.Text += clientSellerPrice.ToString();
			AgentBuyerCommission.Text += agentBuyerComission.ToString();
			AgentSellerCommission.Text += agentSellerComission.ToString();
		}
	}
}

