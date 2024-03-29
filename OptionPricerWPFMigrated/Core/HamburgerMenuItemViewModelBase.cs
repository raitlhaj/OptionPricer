﻿using MahApps.Metro.Controls;
using MahApps.Metro.IconPacks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptionPricerWPF.Core
{
    public class HamburgerMenuItemViewModelBase
    {
        public HamburgerMenuItemCollection MenuItems { get; set; } //Onglets
        public HamburgerMenuItemCollection MenuOptionItems { get; set; } //Informations : ReadMe
        //We can add settings 
        public string Title { get; set; }
        public string AppVersion { get; set; } // Versioning : 1.0.0.1 => F: MajorRelease, S:New feature, T:Patch,...
        public string Author { get; set; }

        public HamburgerMenuItemViewModelBase()
        {
            MenuItems = new HamburgerMenuItemCollection();
            MenuOptionItems = new HamburgerMenuItemCollection();
        }
        //<summary>
        protected void AddHamburgerMenuIconItem(string label, string toolTip,PackIconMaterialKind icon, object tag ) //toolType:info mouse
        {
            MenuItems.Add(new HamburgerMenuIconItem { 
                                                        Icon= new PackIconMaterial
                                                        {
                                                           Kind=icon
                                                        },
                                                        Label = label,
                                                        Tag = tag,
                                                        ToolTip = toolTip,
                                                 }
                         );     
        }

        protected void AddHamburgerMenuOptionIconItem(string label, string toolTip, PackIconMaterialKind icon, object tag) //toolType:info mouse
        {
            MenuOptionItems.Add(new HamburgerMenuIconItem
                                                        {
                                                            Icon= new PackIconMaterial
                                                            {
                                                                Kind=icon
                                                            },
                                                            Label = label,
                                                            Tag = tag,
                                                            ToolTip = toolTip,
                                                         }
                               );
        }


    }
}
