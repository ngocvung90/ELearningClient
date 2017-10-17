using ElearningClient.Interface;
using ElearningClient.Model;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace ElearningClient.View
{
	public class ListViewCode : ContentPage
	{
		private ObservableCollection<lectureBindingModel> listLectures { get; set; }
		public ListViewCode ()
		{
			listLectures = new ObservableCollection<lectureBindingModel> ();
			ListView lstLectureView = new ListView ();
			lstLectureView.ItemsSource = listLectures;
            lstLectureView.ItemSelected += LstLectureView_ItemSelected;
            //TODO - uncomment the region for the built-in cell type you'd like to see
            #region textCell
            //lstLectureView.ItemTemplate = new DataTemplate (typeof(TextCell));
            //lstLectureView.ItemTemplate.SetBinding (TextCell.TextProperty, "lectureName");
            //lstLectureView.ItemTemplate.SetBinding (TextCell.DetailProperty, "lectureComment");
            #endregion

            #region imageCell
            lstLectureView.ItemTemplate = new DataTemplate(typeof(ImageCell));
            lstLectureView.ItemTemplate.SetBinding(TextCell.TextProperty, "lectureName");
            lstLectureView.ItemTemplate.SetBinding(TextCell.DetailProperty, "lectureComment");
            lstLectureView.ItemTemplate.SetBinding(ImageCell.ImageSourceProperty, "lectureImagePath");
            #endregion

            //string[] listBaiGiang = DependencyService.Get<ITextService>().GetListFiles("/sdcard/Elearning");

            Content = lstLectureView; 

			listLectures.Add (new lectureBindingModel( new lectureModel() { lectureName = "Math", lectureComment = "Add with number less than 10", lectureType = LECTURE_TYPE.DOCUMENT_VIEW, audioPath = "/sdcard/Elearning/pdf/audio.mp3", documentPath = "/sdcard/Elearning/pdf/data.xml" }));
			listLectures.Add (new lectureBindingModel(new lectureModel() { lectureName = "English", lectureComment = "Simple present", lectureType = LECTURE_TYPE.HAND_WRITING, audioPath = "/sdcard/Elearning/xml/audio.mp3" , documentPath = "/sdcard/Elearning/xml/data.xml" }));
			listLectures.Add (new lectureBindingModel(new lectureModel() { lectureName = "Math", lectureComment = "Multiply with 2", lectureType = LECTURE_TYPE.DOCUMENT_VIEW, audioPath = "/sdcard/Elearning/pdf/audio.mp3", documentPath = "/sdcard/Elearning/pdf/data.xml" }));
			listLectures.Add (new lectureBindingModel(new lectureModel() { lectureName = "Sience", lectureComment = "Temperature introduction", lectureType = LECTURE_TYPE.HAND_WRITING, audioPath = "/sdcard/Elearning/xml/audio.mp3" , documentPath = "/sdcard/Elearning/xml/data.xml" }));
		}

        private void LstLectureView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e != null)
            {
                lectureBindingModel lecture = (lectureBindingModel)(e.SelectedItem);
                MainViewDetail viewDetail = new MainViewDetail();
                if (lecture.lecture.lectureType == LECTURE_TYPE.HAND_WRITING)
                    viewDetail.SetLecture(lecture.lecture);
                else
                    viewDetail.SetLecture(lecture.lecture);
                this.Navigation.PushAsync(viewDetail);
            }
        }
    }
}


