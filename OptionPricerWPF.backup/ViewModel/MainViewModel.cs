using GalaSoft.MvvmLight;
using OptionPricerWPF.Core;

namespace OptionPricerWPF.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : HamburgerMenuItemViewModelBase
    {

        public string About { get; set; }
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        
        public MainViewModel()
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}
            ///

            Title = "Option Pricer";
            AppVersion = "1.0.0.0";
            Author = "ChefAntoine";
            About = AppVersion + "\t" + Author;

            CreateMenuItems();
        }


        public void CreateMenuItems()
        {
            //to add a new menu :

            AddHamburgerMenuIconItem(label:"Option Pricing", toolTip: "PriceOption", icon:MahApps.Metro.IconPacks.PackIconMaterialKind.CurrencyEur,
                tag:CommonServiceLocator.ServiceLocator.Current.GetInstance<OptionPricingViewModel>());


            AddHamburgerMenuIconItem(label: "Registered Options", toolTip: "RegisteredOptions", icon: MahApps.Metro.IconPacks.PackIconMaterialKind.Database,
                tag: CommonServiceLocator.ServiceLocator.Current.GetInstance<OptionsGetterViewModel>());
            //one menu=one viewModel
            AddHamburgerMenuOptionIconItem(label:"Information",toolTip:"Practice Guide",icon: MahApps.Metro.IconPacks.PackIconMaterialKind.Information,
               tag:CommonServiceLocator.ServiceLocator.Current.GetInstance<InformationViewModel>() );
                
            //To do: Binding in C# MahApp.Metro & Xaml
            // To do 
        }
    }
}