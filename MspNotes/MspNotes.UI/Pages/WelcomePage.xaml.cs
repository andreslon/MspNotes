using MspNotes.UI.Services;
using System;
using Windows.UI.Xaml.Controls;

namespace MspNotes.UI.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class WelcomePage : Page
    {
        OneDriveService onedrive;
        public WelcomePage()
        {
            this.InitializeComponent();


            this.Loaded += (s, e) =>
            {
                onedrive = new OneDriveService();
                ddd();
            };
        }

        async private void ddd()
        {
           await onedrive.GetAllNotes();
        }

        private void btnlogin_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            onedrive.SaveNote(new Model.Note() { Id = Guid.NewGuid().ToString(), Title = "Holaa", Description = "esto es una description" });
        }
    }
}
