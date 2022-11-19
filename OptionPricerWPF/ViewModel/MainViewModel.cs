using CommunityToolkit.Mvvm.DependencyInjection;
using OptionPricerWPF.Core;

namespace OptionPricerWPF.ViewModel
{
    public interface IMainViewModel
    {
        void CreateMenuItems();
    }
    public class MainViewModel : HamburgerMenuItemViewModelBase, IMainViewModel
    {
        public string About { get; set; }
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            MyTitle = "Option Pricer";
            AppVersion = "1.0.0.0";
            Author = "Rachid, Antoine";
            About = AppVersion + "\t" + Author;           

           CreateMenuItems();
        }


        public void CreateMenuItems()
        {
            //to add a new menu :
            //CommonServiceLocator.ServiceLocator.Current.GetInstance<OptionsGetterViewModel>();
            AddHamburgerMenuIconItem(label: "Option Pricing", toolTip: "PriceOption",
                tag: Ioc.Default.GetService<IOptionPricingViewModel>(), icon: MahApps.Metro.IconPacks.PackIconMaterialKind.Package);

            //CommonServiceLocator.ServiceLocator.Current.GetInstance<OptionsGetterViewModel>()
            // icon:MahApps.Metro.IconPacks.PackIconMaterialKind.None
            AddHamburgerMenuIconItem(label: "Registered Options", toolTip: "RegisteredOptions",
                tag: Ioc.Default.GetService<IOptionsGetterViewModel>(), icon: MahApps.Metro.IconPacks.PackIconMaterialKind.Database);

            //one menu=one viewModel//, icon: MahApps.Metro.IconPacks.PackIconMaterialKind.Database
            //CommonServiceLocator.ServiceLocator.Current.GetInstance<InformationViewModel>()
            AddHamburgerMenuOptionIconItem(label:"Information",toolTip:"Practice Guide",
               tag: Ioc.Default.GetService<IInformationViewModel>(), icon: MahApps.Metro.IconPacks.PackIconMaterialKind.Information);
            //,icon: MahApps.Metro.IconPacks.PackIconMaterialKind.Information
            //To do: Binding in C# MahApp.Metro & Xaml
            // To do 
        }
    }
}