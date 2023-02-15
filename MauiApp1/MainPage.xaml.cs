using MauiApp1.ViewModel;
using Newtonsoft.Json;
using System.Diagnostics;


namespace MauiApp1;

public partial class MainPage : ContentPage
{
	

	public MainPage(LocationViewModel viewModel)
	{
        InitializeComponent();
		BindingContext = viewModel; 
	}
	
	
}






