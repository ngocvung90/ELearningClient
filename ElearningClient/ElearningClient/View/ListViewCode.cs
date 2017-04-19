using ElearningClient.Model;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace ElearningClient.View
{
	public class ListViewCode : ContentPage
	{
		private ObservableCollection<lectureModel> veggies { get; set; }
		public ListViewCode ()
		{
			veggies = new ObservableCollection<lectureModel> ();
			ListView lstLectureView = new ListView ();
			lstLectureView.ItemsSource = veggies;
            lstLectureView.ItemSelected += LstLectureView_ItemSelected;
			//TODO - uncomment the region for the built-in cell type you'd like to see
			#region textCell
			lstLectureView.ItemTemplate = new DataTemplate (typeof(TextCell));
			lstLectureView.ItemTemplate.SetBinding (TextCell.TextProperty, "name");
			lstLectureView.ItemTemplate.SetBinding (TextCell.DetailProperty, "comment");
			#endregion

			/*#region imageCell
			lstView.ItemTemplate = new DataTemplate (typeof(ImageCell));
			lstView.ItemTemplate.SetBinding (ImageCell.TextProperty, "name");
			lstView.ItemTemplate.SetBinding (ImageCell.DetailProperty, "comment");
			lstView.ItemTemplate.SetBinding(ImageCell.ImageSourceProperty, "image");
			#endregion*/

			/*#region switchCell
			lstView.ItemTemplate = new DataTemplate (typeof(SwitchCell));
			lstView.ItemTemplate.SetBinding (SwitchCell.TextProperty, "name");
			lstView.ItemTemplate.SetBinding (SwitchCell.OnProperty, "isReallyAVeggie");
			#endregion*/

			/*#region entryCell
			lstView.ItemTemplate = new DataTemplate(typeof(EntryCell));
			lstView.ItemTemplate.SetBinding(EntryCell.LabelProperty, "name");
			lstView.ItemTemplate.SetBinding(EntryCell.TextProperty, "comment");
			#endregion*/
			Content = lstLectureView;
			veggies.Add (new lectureModel() { name = "Math", comment = "Add with number less than 10", isReallyAVeggie = false, image="tomato.png" });
			veggies.Add (new lectureModel() { name = "English", comment = "Simple present", isReallyAVeggie = false, image="pizza.png" });
			veggies.Add (new lectureModel() { name = "Math", comment = "Multiply with 2", isReallyAVeggie = true, image="lettuce.png" });
			veggies.Add (new lectureModel() { name = "Sience", comment = "Temperature introduction", isReallyAVeggie = true, image="zucchini.png" });
		}

        private void LstLectureView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e != null)
            {
                lectureModel lecture = (lectureModel)(e.SelectedItem);
                MainViewDetail viewDetail = new MainViewDetail();
                if (lecture.name == "Math")
                    viewDetail.SetLecture(LECTURE_TYPE.HAND_WRITING);
                else
                    viewDetail.SetLecture(LECTURE_TYPE.DOCUMENT_VIEW);
                this.Navigation.PushAsync(viewDetail);
            }
        }
    }
}


