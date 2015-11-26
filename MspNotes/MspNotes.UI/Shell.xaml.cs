using System;
using System.Linq;
using Windows.UI.Xaml.Controls;
using MspNotes.UI.Pages;
using MspNotes.UI.Presentation;
using MspNotes.UI.Services;

namespace MspNotes.UI
{
    public sealed partial class Shell : UserControl
    {
        public Shell()
        {
            this.InitializeComponent();

            var vm = new ShellViewModel();
            vm.MenuItems.Add(new MenuItem { Icon = "", Title = "Welcome", PageType = typeof(WelcomePage) });
            //vm.MenuItems.Add(new MenuItem { Icon = "", Title = "Page 1", PageType = typeof(Page1) });
            //vm.MenuItems.Add(new MenuItem { Icon = "", Title = "Page 2", PageType = typeof(Page2) });
            //vm.MenuItems.Add(new MenuItem { Icon = "", Title = "Page 3", PageType = typeof(Page3) });
            vm.MenuItems.Add(new MenuItem { Icon = "", Title = "Listar Notas", PageType = typeof(NotesPage) });
            vm.MenuItems.Add(new MenuItem { Icon = "", Title = "Agregar Nota", PageType = typeof(NotePage) });

            // select the first menu item
            vm.SelectedMenuItem = vm.MenuItems.First();

            this.ViewModel = vm;

            this.Loaded += (s, e) => {
                App.MainViewModel.onedrive = new OneDriveService();
            };
        }

        public ShellViewModel ViewModel { get; private set; }

        public Frame RootFrame
        {
            get
            {
                return this.Frame;
            }
        }
    }
}
