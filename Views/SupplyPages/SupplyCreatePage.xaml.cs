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

namespace Praktika.Views.SupplyPages
{
    /// <summary>
    /// Логика взаимодействия для SupplyCreatePage.xaml
    /// </summary>
    public partial class SupplyCreatePage : Page
    {
		NedvizhdbContext _context = new();
		Supply Supply { get; set; }
		public SupplyCreatePage()
		{
			InitializeComponent();
			Supply = new Supply();
			DataContext = Supply;
			AgentComboBox.ItemsSource = _context.Agents.ToList();
			ClientComboBox.ItemsSource = _context.Clients.ToList();
			RealtyComboBox.ItemsSource = _context.Realties.ToList();
		}
		public SupplyCreatePage(Supply supply)
		{
			InitializeComponent();
			Supply = supply;
			DataContext = Supply;
			AgentComboBox.ItemsSource = _context.Agents.ToList();
			ClientComboBox.ItemsSource = _context.Clients.ToList();
			RealtyComboBox.ItemsSource = _context.Realties.ToList();
		}
		private async void SaveChangesButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				Supply currentSupply = (Supply)((Button)sender).Tag;

				if (Convert.ToInt32(currentSupply.Price) < 0 )
				{
					MessageBox.Show("Минимальная цена и максимальная цена - целые положительные числа.");
					return;
				}
				if (Supply.Id == 0)
				{
					var lastDemand = await _context.Supplies.OrderBy(c => c.Id).LastAsync();
					Supply.Id = lastDemand.Id + 1;
					_context.Supplies.Add(Supply);
				}
				else
				{
					_context.Supplies.Update(Supply);
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
			SupplyListPage supplyListPage = new();
			Window.GetWindow(this).Content = supplyListPage;
		}
	}
}

