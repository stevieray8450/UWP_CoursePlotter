using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.Storage;
using System.Net.Http;
using Newtonsoft.Json;
using Amazon.APIGateway;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace App1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class InvPage : Page
    {
        public InvPage()
        {
            this.InitializeComponent();
        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(NewPage));
        }

        private void invPage_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(InvPage));
        }

        private void todo2_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(UserAuth));
        }

        public void hypLinkPage2_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Faculty));
        }

        public class InventoryItems
        {
            public string Id { get; set; }
            public string Text { get; set; }
            public string Course { get; set; }
            public bool Done { get; set; }
        }

        IMobileServiceTable<InventoryItems> invItemsTable = App.MobileService.GetTable<InventoryItems>();
        MobileServiceCollection<InventoryItems, InventoryItems> items;

        async private void Submit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                InventoryItems item = new InventoryItems
                {
                    Text = txtBoxInvItem.Text,
                    Course = txtBoxCourse.Text,
                    Done = false
                };
                await App.MobileService.GetTable<InventoryItems>().InsertAsync(item);
                var dialog = new MessageDialog("Successful!");
                await dialog.ShowAsync();
            }
            catch (Exception em)
            {
                var dialog = new MessageDialog("An Error Occured: " + em.Message);
                await dialog.ShowAsync();
            }
        }

        private async void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            await RefreshTodoItems();
        }

        private async Task RefreshTodoItems()
        {
            MobileServiceInvalidOperationException exception = null;
            try
            {
                // This code refreshes the entries in the list view by querying the TodoItems table.
                // The query excludes completed TodoItems
                //items = await invItemsTable
                //    .Where(invItem => invItem.Done == false)
                //    .ToCollectionAsync();

                items = await invItemsTable
                   .Where(invItem => invItem.Course == txtBoxCourse.Text)
                   .ToCollectionAsync();
            }
            catch (MobileServiceInvalidOperationException e)
            {
                exception = e;
            }

            if (exception != null)
            {
                await new MessageDialog(exception.Message, "Error loading items").ShowAsync();
            }
            else
            {
                ListItems.ItemsSource = items;
                this.btnRefresh.IsEnabled = true;
            }
        }

        private async void CheckBoxComplete_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            InventoryItems item = cb.DataContext as InventoryItems;
            await UpdateCheckedInventoryItems(item);
        }

        private async Task UpdateCheckedInventoryItems(InventoryItems item)
        {
            // This code takes a freshly completed TodoItem and updates the database. When the MobileService 
            // responds, the item is removed from the list 
            await invItemsTable.UpdateAsync(item);
            // items.Remove(item);
            // ListItems.Focus(Windows.UI.Xaml.FocusState.Unfocused);

            //await SyncAsync(); // offline sync
        }

    }

}
