﻿using AdvancedTimer.Forms.Plugin.Abstractions;
using ElearningClient.Model;
using ElearningClient.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Xamarin.Forms;
using ElearningClient.Interface;
using SkiaSharp;

namespace ElearningClient.ViewModel
{
    public class MainViewDetailVM:ViewModelBase
    {
        static MainViewDetail _view;
        #region  PDF lecture variables
        static ClassRoom classRoom = null;
        static int currentStep = 0, timeLapped = 0;
        static IAdvancedTimer timer;
        #endregion
        #region Hand Writing variables
        static HandWritingData handWritingData = null;
        static int currentStrokeIndex = 0;
        static int handWritingTimeLapsed = 0;
        #endregion
        public MainViewDetailVM(MainViewDetail view)
        {
        }

        public MainViewDetail GetMainView()
        {
            return _view;
        }
        public void SetMainViewDetail(MainViewDetail view)
        {
            _view = view;
            string strPDFLecture = "", strHandWritingLecture = "";
            try
            {
                if (_view._lecture.lectureType == LECTURE_TYPE.DOCUMENT_VIEW)
                {
                    if(timer == null)
                    {
                        timer = DependencyService.Get<IAdvancedTimer>();
                        timer.initTimer(1000, timerElapsed, true);
                    }
                    #region Deserialize pdf lecture
                    READ_TEXT_ERRORCODE err = DependencyService.Get<ITextService>().LoadText(_view._lecture.documentPath, out strPDFLecture);
                    var serializer = new XmlSerializer(typeof(ClassRoom));
                    using (TextReader reader = new StringReader(strPDFLecture))
                        classRoom = (ClassRoom)serializer.Deserialize(reader);
                    #endregion
                }
                else if (_view._lecture.lectureType == LECTURE_TYPE.HAND_WRITING)
                {
                    if(timer == null)
                    {
                        timer = DependencyService.Get<IAdvancedTimer>();
                        timer.initTimer(10, timerElapsed, true);
                    }
                    #region Deserialize hand writing lecture
                    READ_TEXT_ERRORCODE errHandWriting = DependencyService.Get<ITextService>().LoadText(_view._lecture.documentPath, out strHandWritingLecture);

                    var serializerHand = new XmlSerializer(typeof(HandWritingData), new XmlRootAttribute("HandWritingData"));
                    using (TextReader reader = new StringReader(strHandWritingLecture))
                        handWritingData = (HandWritingData)serializerHand.Deserialize(reader);
                    #endregion
                }

            }
            catch (Exception ex)
            {
                string strError = ex.ToString();
            }
        }

        private RelayCommand _webAction;
        public RelayCommand WebAction
        {
            get
            {
                if (_webAction == null) _webAction = new RelayCommand(DoWebAction);
                return _webAction;
            }
            set
            {
                _webAction = value;
                OnPropertyChanged("WebAction");
            }
        }

        void DoWebAction()
        {
            _view.GetPdfWebView().Eval("goNext()");
        }
        public void DoNextAction()
        {
            _view.GetPdfWebView().Eval("goNext()");
        }

        public void DoTestAction()
        {
        }

        public void DoPrevAction()
        {
            _view.GetPdfWebView().Eval("goPrevious()");
        }

        public void DoPlayAction()
        {
            DependencyService.Get<IAudio>().PlayAudioFile(_view._lecture.audioPath);

            if(_view._lecture.lectureType == LECTURE_TYPE.DOCUMENT_VIEW)
            {
                timeLapped = 0;
                currentStep = 0;
                timer.setInterval(1000);
                timer.startTimer();
            }
            else if (_view._lecture.lectureType == LECTURE_TYPE.HAND_WRITING)
            {
                currentStrokeIndex = 0;
                handWritingTimeLapsed = 0;
                timer.setInterval(10);
                timer.startTimer();
            }
        }

        public void StopLecture()
        {
            if (timer != null) timer.stopTimer();
            DependencyService.Get<IAudio>().Stop();
        }
        public static void timerElapsed(object o, EventArgs e)
        {
            #region Document View rendering
            if (_view._lecture.lectureType == LECTURE_TYPE.DOCUMENT_VIEW)
            {
                timeLapped++;
                ClassRoomDetail detail = classRoom.LectureDetail[currentStep];

                System.Diagnostics.Debug.WriteLine("Lecture (Document : {0} , Time : {1}, Page : {2}", detail.Document, detail.Time, detail.Page);
                System.Diagnostics.Debug.WriteLine("Timer lapped : {0}", timeLapped);

                if (detail.Time == (byte)timeLapped)
                {
                    int currentPage = detail.Page;
                    currentStep++;
                    Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                    {
                        string goToPage = String.Format("goToPage({0})", currentPage);
                        _view.GetPdfWebView().Eval(goToPage);
                    });
                }
            }
            #endregion
            #region Handwriting rendering
            if (currentStrokeIndex >= handWritingData.Items.Length) return;

            object item = handWritingData.Items[currentStrokeIndex];
            string strItem = item.ToString();

            if (strItem.Contains("DOWN"))//set from, no need to draw
            {
                HandWritingDataDOWN downPoint = (HandWritingDataDOWN)item;
                if (currentStrokeIndex == 0)//first drawing --> need to wait to time down then render
                {
                    int startDownTime = (int)downPoint.time;
                    if (handWritingTimeLapsed < startDownTime)
                    {
                        System.Diagnostics.Debug.WriteLine("Waiting for writing time : {0}/{1}", handWritingTimeLapsed, startDownTime);
                        handWritingTimeLapsed += timer.getInterval();
                        return;
                    }
                }

                SKPoint point = new SKPoint((float)downPoint.x, (float)downPoint.y);
                _view.GetHandWritingView().SetFromPoint(point);
                System.Diagnostics.Debug.WriteLine("Item : {0}, Point({1}, {2})", strItem, point.X, point.Y);

                //find number of stroke, time of drawing path to determine the time interval
                //number of stroke : from DOWN -> UP
                for (int i = currentStrokeIndex; i < handWritingData.Items.Length; i++)
                {
                    if (handWritingData.Items[i].ToString().Contains("UP"))
                    {
                        HandWritingDataUP upPoint = (HandWritingDataUP)handWritingData.Items[i];
                        int timeOfPath = (int)upPoint.time;
                        int numberOfStroke = i - currentStrokeIndex + 1;
                        System.Diagnostics.Debug.WriteLine("Start new path, Number of stroke : {0}, Time of path : {1}", numberOfStroke, timeOfPath);
                        if (numberOfStroke > timeOfPath)
                        {
                            timer.setInterval(100);
                            System.Diagnostics.Debug.WriteLine("Hand writing too fast, set timer interval 100ms");
                        }
                        else
                        {
                            int newTimeInterVal = timeOfPath / numberOfStroke;
                            timer.setInterval(newTimeInterVal);
                            System.Diagnostics.Debug.WriteLine("New handwriting timer interval {0} ms", newTimeInterVal);
                        }
                        break;
                    }
                }
            }
            else if (strItem.Contains("MOVE"))//set to, need to draw
            {
                HandWritingDataMOVE movePoint = (HandWritingDataMOVE)item;
                SKPoint point = new SKPoint((float)movePoint.x, (float)movePoint.y);
                Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                {
                    _view.GetHandWritingView().SetToPoint(new SKPoint((float)movePoint.x, (float)movePoint.y));
                });
                System.Diagnostics.Debug.WriteLine("Item : {0}, Point({1}, {2})", strItem, point.X, point.Y);
            }
            else if (strItem.Contains("UP"))
            {
                HandWritingDataUP upPoint = (HandWritingDataUP)item;
                SKPoint point = new SKPoint((float)upPoint.x, (float)upPoint.y);
                System.Diagnostics.Debug.WriteLine("Item : {0}, Point({1}, {2})", strItem, point.X, point.Y);

                //find number of stroke, time of drawing path to determine the time interval
                //number of stroke : from DOWN -> UP
                for (int i = currentStrokeIndex; i < handWritingData.Items.Length; i++)
                {
                    if (handWritingData.Items[i].ToString().Contains("DOWN"))
                    {
                        HandWritingDataDOWN nextDownPoint = (HandWritingDataDOWN)handWritingData.Items[i];
                        int timeOfRest = (int)nextDownPoint.time;
                        timer.setInterval(timeOfRest);
                        System.Diagnostics.Debug.WriteLine("Rest from UP to DOWN, New handwriting timer interval {0} ms", timeOfRest);
                        break;
                    }
                }

            }
            currentStrokeIndex++;
            #endregion
        }
    }
}
