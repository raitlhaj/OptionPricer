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
            // CommonServiceLocator.ServiceLocator.SetLocatorProvider(() => Ioc.Default);
            //<remarks>
            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}
            ///
           

            Ioc.Default.ConfigureServices(
                  new ServiceCollection()

                  .AddSingleton<IMainViewModel,MainViewModel>()
                  .AddSingleton<IOptionPricingViewModel,OptionPricingViewModel>()
                  .AddSingleton<IInformationViewModel,InformationViewModel>()
                  .AddSingleton<IOptionsGetterViewModel,OptionsGetterViewModel>()
                  .BuildServiceProvider());
   


            //Ioc.Default.Register<MainViewModel>();
            //Ioc.Default.Register<OptionPricingViewModel>();
            // Ioc.Default.Register<InformationViewModel>();
            // Ioc.Default.Register<OptionsGetterViewModel>();

        }

        public static IMainViewModel Main
        {
            get
            {
                return   Ioc.Default.GetService<IMainViewModel>();
            }
        }
        public static IOptionPricingViewModel OptionPricing
        {
            get
            {
                return Ioc.Default.GetService<IOptionPricingViewModel>();
            }
        }
        public static IInformationViewModel Information
        {
            get
            {
                return Ioc.Default.GetService<IInformationViewModel>();
            }
        }
        public static IOptionsGetterViewModel RegisteredOptions
        {
            get
            {
                return Ioc.Default.GetService<IOptionsGetterViewModel>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}