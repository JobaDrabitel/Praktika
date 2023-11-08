using Microsoft.EntityFrameworkCore;
using Praktika.Models;
using Praktika.Views.RealtyPages;
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

namespace Praktika.Views.DemandPages
{
    /// <summary>
    /// Логика взаимодействия для DemandCreatePage.xaml
    /// </summary>
    public partial class DemandCreatePage : Page
    {
		NedvizhdbContext _context = new();
		Demand Demand { get; set; }
		public DemandCreatePage()
		{
			InitializeComponent();
			Demand = new Demand();
			DataContext = Demand;
			AgentComboBox.ItemsSource = _context.Agents.ToList();
			ClientComboBox.ItemsSource = _context.Clients.ToList();
			RealtyComboBox.ItemsSource = _context.Realties.ToList();
		}
		public DemandCreatePage(Demand demand)
		{
			InitializeComponent();
			Demand = demand;
			DataContext = Demand;
			AgentComboBox.ItemsSource = _context.Agents.ToList();
			ClientComboBox.ItemsSource = _context.Clients.ToList();
			RealtyComboBox.ItemsSource = _context.Realties.ToList();
		}
		private async void SaveChangesButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				Demand currentDemand = (Demand)((Button)sender).Tag;
			
				if (Convert.ToInt32(MinPriceTB.Text) < 0 || Convert.ToInt32(MaxPriceTB.Text) < 0) 
				{
					MessageBox.Show("Минимальная цена и максимальная цена - целые положительные числа.");
					return;
				}
					Demand.AddressCity = _context.Realties.Where(r => r.AddressStreet == Demand.AddressStreet).Select(r => r.AddressCity).FirstOrDefault();
				if (Demand.Id == 0)
				{
					var lastDemand = await _context.Demands.OrderBy(c => c.Id).LastAsync();
					Demand.Id = lastDemand.Id + 1;
					_context.Demands.Add(Demand);
				}
				else
				{
					_context.Demands.Update(Demand);
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
			DemandListPage demandListPage = new();
			Window.GetWindow(this).Content = demandListPage;
		}
	}
}

