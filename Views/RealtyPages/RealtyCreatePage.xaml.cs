using Microsoft.EntityFrameworkCore;
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

namespace Praktika.Views.RealtyPages
{
	/// <summary>
	/// Логика взаимодействия для RealtyCreatePage.xaml
	/// </summary>
	public partial class RealtyCreatePage : Page
	{
		NedvizhdbContext _context = new();
		Realty Realty { get; set; }
		public RealtyCreatePage()
		{
			InitializeComponent();
			Realty = new Realty();
			DataContext = new List<Realty> { Realty };
		}
		public RealtyCreatePage(Realty realty)
		{
			InitializeComponent();
			Realty = realty;
			DataContext = new List<Realty> { Realty };
		}
		private async void SaveChangesButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (!(Realty.AddressHouse is int) || !(Realty.AddressNumber is int))
				{
					MessageBox.Show("Номер дома должен быть целым числом");
					return;
				}
				Realty currentRealty = (Realty)((Button)sender).Tag;
				if (Realty.Id == 0)
				{
					var lastRealty = await _context.Realties.OrderBy(c => c.Id).LastAsync();
					Realty.Id = lastRealty.Id + 1;
					_context.Realties.Add(Realty);
				}
				else
				{
					_context.Realties.Update(Realty);
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

		private void RealtyBackButton_Click(object sender, RoutedEventArgs e)
		{
			RealtyListPage realtyListPage = new();
			Window.GetWindow(this).Content = realtyListPage;
		}
	}
}

