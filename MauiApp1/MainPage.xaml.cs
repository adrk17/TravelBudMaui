using MauiApp1.ViewModel;
using Newtonsoft.Json;
using System.Diagnostics;


namespace MauiApp1;

public partial class MainPage : ContentPage
{
	
    int count = 0;
	

	public MainPage(LocationViewModel viewModel)
	{
        InitializeComponent();
		BindingContext = viewModel; 
	}

	private void OnCounterClicked(object sender, EventArgs e)
	{
		Android.Util.Log.Info("kurwa", "Siema");
        
	}
	
	
}






