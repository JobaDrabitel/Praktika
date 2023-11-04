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
			ClientListPage clientListPage = new ClientListPage();
			this.Content = clientListPage;
		}

		private void CreateUserButton_Click(object sender, RoutedEventArgs e)
		{
			ClientCreatePage clientCreatePage = new ClientCreatePage();
			this.Content = clientCreatePage;
		}
	}
}
