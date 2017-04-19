using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;
using Windows.UI.Popups;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace App1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NewPage : Page
    {
        public NewPage()
        {
            this.InitializeComponent();
            BL_PageContent.CreatedBy = "Created by: Steve Boniface";
            txtBoxFooter.Text = BL_PageContent.CreatedBy;
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

        public class TodoItem
        {
            public string Id { get; set; }
            public string Text { get; set; }
            public bool Complete { get; set; }
        }

        IMobileServiceTable<TodoItem> todoTable = App.MobileService.GetTable<TodoItem>();
        MobileServiceCollection<TodoItem, TodoItem> items;

        async private void Submit_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                TodoItem item = new TodoItem
                {
                    Text = txtBoxItem.Text,
                    Complete = false
                };
                await App.MobileService.GetTable<TodoItem>().InsertAsync(item);
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
                items = await todoTable
                    .Where(todoItem => todoItem.Complete == false)
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
            TodoItem item = cb.DataContext as TodoItem;
            await UpdateCheckedTodoItem(item);
        }

        private async Task UpdateCheckedTodoItem(TodoItem item)
        {
            // This code takes a freshly completed TodoItem and updates the database. When the MobileService 
            // responds, the item is removed from the list 2
            await todoTable.UpdateAsync(item);
            items.Remove(item);
            ListItems.Focus(Windows.UI.Xaml.FocusState.Unfocused);

            //await SyncAsync(); // offline sync
        }

    }
}
