using ElearningClient.Interface;
using ElearningClient.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace ElearningClient.View
{
    class QuickTestView : ContentPage
    {
        static MainViewDetail _view;
        QuickTest quickTest = null;
        public QuickTestView(MainViewDetail view)
        {
            _view = view;
            #region Deserialize quick test data
            string strQuickTest = "";
            READ_TEXT_ERRORCODE errQuicTestReading = DependencyService.Get<ITextService>().LoadText("/sdcard/Elearning/QuickTest.xml", out strQuickTest);

            var serializerHand = new XmlSerializer(typeof(QuickTest), new XmlRootAttribute("QuickTest"));
            using (TextReader reader = new StringReader(strQuickTest))
                quickTest = (QuickTest)serializerHand.Deserialize(reader);
            int nNumberQuestion = (int)(quickTest.Information.NumberQuestion);
            #endregion

            Button btnClose = new Button() { Text = "Close" };
            btnClose.Clicked += BtnCloseOnClicked;

            var mainStack = new StackLayout();
            mainStack.Orientation = StackOrientation.Vertical;
            var childStack = new StackLayout();
            childStack.Orientation = StackOrientation.Vertical;
            Label lbTitle = new Label { Text = "Quick Test with " + nNumberQuestion + " questions.", TextColor = Color.Black };
            childStack.Children.Add(lbTitle);

            for (int i = 0; i < nNumberQuestion; i++)
            {
                childStack.Padding = new Thickness(10, 5, 10, 5);
                QuickTestQuiz quiz = quickTest.Detail[i];
                Label lbQuizContent = new Label { Text = String.Format("{0}. {1}", quiz.ID, quiz.Content), TextColor = Color.Black };
                childStack.Children.Add(lbQuizContent);

                for (int ansIndex = 0; ansIndex < quiz.Ans.A.Length; ansIndex ++)
                {
                    CheckBox checkbox = new CheckBox();
                    var ansStack = new StackLayout();
                    ansStack.Orientation = StackOrientation.Horizontal;
                    ansStack.Children.Add(checkbox);
                    Label ansContent = new Label { Text = quiz.Ans.A[ansIndex].ToString(), TextColor = Color.Black };
                    ansStack.Children.Add(ansContent);

                    childStack.Children.Add(ansStack);
                }
            }

            var buttonStack = new StackLayout();
            buttonStack.Orientation = StackOrientation.Horizontal;
            buttonStack.Children.Add(new Button { Text = "Submit" });
            buttonStack.Children.Add(new Button { Text = "End Test" });
            childStack.Children.Add(buttonStack);

            mainStack.Children.Add(childStack);
            Content = new ScrollView { Content = mainStack };//

            // set the background to transparent color 
            // (actually darkened-transparency: notice the alpha value at the end)
            this.BackgroundColor = new Color(250, 250, 250, 1);
        }

        private void BtnCloseOnClicked(object sender, EventArgs e)
        {
            // Close the modal page
            Navigation.PopModalAsync();
        }
    }
}
