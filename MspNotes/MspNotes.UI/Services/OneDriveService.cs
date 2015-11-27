using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using Microsoft.OneDrive.Sdk;
using MspNotes.UI.Model;

namespace MspNotes.UI.Services
{
    public class OneDriveService
    {
        public IOneDriveClient oneDriveClient { get; set; }
        public readonly string[] scopes = new string[] { "onedrive.readwrite", "wl.offline_access", "wl.signin" };

        public OneDriveService()
        {
            Initialize();
        }
        async private void Initialize()
        {
            try
            {
                if (oneDriveClient == null)
                {
                    oneDriveClient = OneDriveClientExtensions.GetClientUsingOnlineIdAuthenticator(scopes);
                    await oneDriveClient.AuthenticateAsync();
                }
            }
            catch (Exception ex)
            {
            }
        }

        async public Task<List<Note>> GetAllNotes()
        {
            var lst = new List<Note>();
            try
            {
                var lstItems = await oneDriveClient
                                 .Drive
                                 .Special
                                 .AppRoot
                                 .Children
                                 .Request()
                                 .GetAsync();

                foreach (var item in lstItems)
                {
                    var contentStream = await oneDriveClient
                                .Drive
                                .Items[item.Id]
                                .Content
                                .Request()
                                .GetAsync();

                    lst.Add(new Serializer().DeserializeObject<Note>(contentStream));
                }
            }
            catch (Exception)
            {
            }
            return lst;
        }

        async public Task<Note> GetNote(string noteId)
        {
            var contentStream = await oneDriveClient
                               .Drive
                               .Special
                               .AppRoot
                               .ItemWithPath(string.Format("{0}.txt", noteId))
                               .Content
                               .Request()
                               .GetAsync();

            return new Serializer().DeserializeObject<Note>(contentStream);
        }

        async public Task DeleteNote(string noteId)
        {
            await oneDriveClient
                                .Drive
                                .Special
                                .AppRoot
                                .ItemWithPath(string.Format("{0}.txt", noteId))
                                .Request()
                                .DeleteAsync();
        }

        async public Task UpdateNote(Note note)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(new Serializer().SerializeObject(note));
            MemoryStream stream = new MemoryStream(byteArray);
            using (stream)
            {
                var uploadedItem = await oneDriveClient
                                             .Drive
                                             .Root
                                             .ItemWithPath(string.Format("{0}.txt", note.Id))
                                             .Content
                                             .Request()
                                             .PutAsync<Item>(stream);
            }
        }

        async public Task SaveNote(Note note)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(new Serializer().SerializeObject(note));
            MemoryStream stream = new MemoryStream(byteArray);
            var itemSave = await oneDriveClient
                    .Drive
                    .Special
                    .AppRoot
                    .Children[string.Format("{0}.txt", note.Id)]
                    .Content
                    .Request()
                    .PutAsync<Item>(stream);
        }


    }
}
