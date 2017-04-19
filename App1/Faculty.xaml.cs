using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;

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

using SQLite;
using SQLite.Net;
using SQLite.Net.Async;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace App1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Faculty : Page
    {
        public Faculty()
        {
            this.InitializeComponent();
            BL_PageContent.CreatedBy = "Created by: Steve Boniface";
            courseFaculty.Text = "";
            txtBoxFooter.Text = BL_PageContent.CreatedBy;

            txtCourse.Text = "Click your class button below to display faculty information.";
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

        private void button_Click(object sender, RoutedEventArgs e)
        {
            // executes Course1 method from BL_PageContent. This loads all relevant data to course 1.
            // this data is then used to fill the txtCourse, courseFaculty, aboutCourse, and coursePrereq text blocks
            BL_PageContent.Course1();

            txtCourse.Text = BL_PageContent.CourseID + "\n" + BL_PageContent.CourseCode + "\n" + BL_PageContent.CourseTitle;
            courseFaculty.Text = BL_PageContent.FacultyMember;
            aboutCourse.Text = BL_PageContent.AboutCourse;
            coursePrereq.Text = BL_PageContent.CreditsPrereq;

            // sending the correct image file path to the courseFacultyPic "placeholder" in the XAML file
            courseFacultyPic.Source = new BitmapImage(new Uri(courseFacultyPic.BaseUri, "syedNabeel.png"));
        }

        private void course2Btn_Click(object sender, RoutedEventArgs e)
        {
            // executes Course2 method from BL_PageContent. This loads all relevant data to course 2.
            // this data is then used to fill the txtCourse, courseFaculty, aboutCourse, and coursePrereq text blocks
            BL_PageContent.Course2();

            txtCourse.Text = BL_PageContent.CourseID + "\n" + BL_PageContent.CourseCode + "\n" + BL_PageContent.CourseTitle;
            courseFaculty.Text = BL_PageContent.FacultyMember;
            aboutCourse.Text = BL_PageContent.AboutCourse;
            coursePrereq.Text = BL_PageContent.CreditsPrereq;

            // sending the correct image file path to the courseFacultyPic "placeholder" in the XAML file
            courseFacultyPic.Source = new BitmapImage(new Uri(courseFacultyPic.BaseUri, "cynthiaMoonhowler.png"));
        }

        private void course3Btn_Click(object sender, RoutedEventArgs e)
        {
            // executes Course3 method from BL_PageContent. This loads all relevant data to course 3.
            // this data is then used to fill the txtCourse, courseFaculty, aboutCourse, and coursePrereq text blocks.
            BL_PageContent.Course3();

            txtCourse.Text = BL_PageContent.CourseID + "\n" + BL_PageContent.CourseCode + "\n" + BL_PageContent.CourseTitle;
            courseFaculty.Text = BL_PageContent.FacultyMember;
            aboutCourse.Text = BL_PageContent.AboutCourse;
            coursePrereq.Text = BL_PageContent.CreditsPrereq;

            // sending the correct image file path to the courseFacultyPic "placeholder" in the XAML file
            courseFacultyPic.Source = new BitmapImage(new Uri(courseFacultyPic.BaseUri, "shadMcdillek.jpeg"));
        }

        public class CourseComment
        {
            public string id { get; set; }
            public string comment { get; set; }
            public bool isComment { get; set; }
        }

        IMobileServiceSyncTable<CourseComment> courseCommentsTable = App.MobileService.GetSyncTable<CourseComment>();
        MobileServiceCollection<CourseComment, CourseComment> items;

        //private async Task InitLocalStoreAsync()
        //{
        //    if (!App.MobileService.SyncContext.IsInitialized)
        //    {
        //        var store = new MobileServiceSQLiteStore("CourseCommentsLocal.db");
        //        store.DefineTable<CourseComment>();
        //        await App.MobileService.SyncContext.InitializeAsync(store);
        //    }
        //    await SyncAsync();
        //}

        //private async Task SyncAsync()
        //{
        //    await App.MobileService.SyncContext.PushAsync();
        //    await courseCommentsTable.PullAsync("CourseComments", courseCommentsTable.CreateQuery());
        //}

        async private void submitComment_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CourseComment item = new CourseComment
                {
                    comment = txtBoxComment.Text,
                    isComment = false
                };
                await App.MobileService.GetTable<CourseComment>().InsertAsync(item);
                var dialog = new MessageDialog("Your comment has been recorded.");
                await dialog.ShowAsync();
            }
            catch (Exception em)
            {
                var dialog = new MessageDialog("An error occurred: " + em.Message);
                await dialog.ShowAsync();
            }
        }

        async private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            //RefreshComments();
            await RefreshComments();
        }

        private async Task RefreshComments()
        {
            MobileServiceInvalidOperationException exception = null;
            try
            {
                items = await courseCommentsTable
                    .Where(CourseComment => CourseComment.isComment == false)
                    .ToCollectionAsync();
            }
            catch (MobileServiceInvalidOperationException e)
            {
                exception = e;
            }
            if(exception != null)
            {
                await new MessageDialog(exception.Message, "Error loading items").ShowAsync();
            }
            else
            {
                comments.ItemsSource = items;
                this.btnRefresh.IsEnabled = true;
            }
        }

        private async void CheckBoxComplete_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            CourseComment item = cb.DataContext as CourseComment;
            await UpdateCheckedTodoItem(item);
        }

        private async Task UpdateCheckedTodoItem(CourseComment item)
        {
            // This code takes a freshly completed TodoItem and updates the database. When the MobileService 
            // responds, the item is removed from the list 
            await courseCommentsTable.UpdateAsync(item);
            items.Remove(item);
            comments.Focus(Windows.UI.Xaml.FocusState.Unfocused);

            //await SyncAsync(); // offline sync
        }
    }
}


