using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.OneDrive.Sdk;

namespace MspNotes.UI.Services
{
    class OneDriveService
    {
        public IOneDriveClient oneDriveClient { get; set; }
        public readonly string[] scopes = new string[] { "onedrive.readwrite", "wl.offline_access", "wl.signin" };

        public OneDriveService()
        {
            Initialize();
        }
        async private void Initialize()
        {
            if (oneDriveClient == null)
            {
                oneDriveClient = OneDriveClientExtensions.GetUniversalClient(this.scopes);
                await oneDriveClient.AuthenticateAsync();
            }


            var childrenPage = await this.oneDriveClient.Drive.Items[""].Children.Request().Expand("thumbnails").GetAsync();
            var items = childrenPage == null
              ? new List<Item>()
              : childrenPage.CurrentPage.Where(item => item.Folder != null || item.Image != null);
        }



    }
}
