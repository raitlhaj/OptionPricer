/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:OptionPricerWPF"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/



using CommonServiceLocator;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace OptionPricerWPF.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            Ioc.Default.ConfigureServices(
                           new ServiceCollection()

                           .AddSingleton<MainViewModel>(new MainViewModel())
                           .AddSingleton<OptionPricingViewModel>(new OptionPricingViewModel())
                           .AddSingleton<InformationViewModel>(new InformationViewModel())
                           .AddSingleton<OptionsGetterViewModel>(new OptionsGetterViewModel())

                           .BuildServiceProvider());

            //<remarks>
            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            //  SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////

        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }
        public OptionPricingViewModel OptionPricing
        {
            get
            {
                return ServiceLocator.Current.GetInstance<OptionPricingViewModel>();
            }
        }
        public InformationViewModel Information
        {
            get
            {
                return ServiceLocator.Current.GetInstance<InformationViewModel>();
            }
        }
        public OptionsGetterViewModel RegisteredOptions
        {
            get
            {
                return ServiceLocator.Current.GetInstance<OptionsGetterViewModel>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}